using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VisiBoole
{
    class InputParser
    {
        public string currentTab;
        public string currentDependent;
        public InputParser(SubDesign sub, string variableClicked)//string[] codeText, string fileName)
        {
            currentTab = sub.FileSourceName;
            if (!Globals.dependencies.ContainsKey(currentTab))
            {
                Globals.dependencies.Add(currentTab, new Dictionary<string, List<string>>());
                Globals.expressions.Add(currentTab, new Dictionary<string, string>());
            }
            if (String.IsNullOrEmpty(variableClicked))
            {
                //currentTab = sub.FileSourceName;
                if (!Globals.variables.ContainsKey(currentTab))
                {
                    Globals.variables.Add(currentTab, new Dictionary<string, int>());
                }

                using (StreamReader reader = sub.FileSource.OpenText())
                {
                    string text = "";
                    int lineNumber = 1;
                    while ((text = reader.ReadLine()) != null)
                    {
                        if (!text.Contains(';'))
                        {

                        }
                        else
                        {
                            ContainsVariable(text.Substring(0, text.Length - 1), lineNumber);
                        }
                        lineNumber++;
                    }
                }
            }
            else
            {
                //currentTab = sub.FileSourceName;
                int newValue = Negate(Globals.variables[currentTab][variableClicked]);
                Globals.variables[currentTab][variableClicked] = newValue;

                //build list of all dependent variables based on user click
                List<string> totalVariables = new List<string>();
                foreach (string dependentVariable in Globals.dependencies[currentTab][variableClicked])
                {
                    totalVariables.Add(dependentVariable);
                }
                int count = 0;
                int end = totalVariables.Count;
                while (count != end)
                {
                    for(int i = count; i < end; i++)
                    {
                        foreach(string dependentVariable in Globals.dependencies[currentTab][totalVariables[i]])
                        {
                            totalVariables.Add(dependentVariable);
                        }
                    }
                    count = end;
                    end = totalVariables.Count;
                }
                foreach(string dependentVariable in totalVariables)
                {
                    //currentDependent is used in SolveExpression()
                    currentDependent = dependentVariable;
                    int updatedVariable = SolveExpression(Globals.expressions[currentTab][dependentVariable], -1);
                    Globals.variables[currentTab][dependentVariable] = updatedVariable;
                }
                //all dependent variable list(loop through with foreach)
                /*foreach(string dependentVariable in Globals.dependencies[currentTab][variableClicked])
                {                  
                    currentDependent = dependentVariable;
                    int updatedVariable = SolveExpression(Globals.expressions[currentTab][dependentVariable], -1);
                    Globals.variables[currentTab][dependentVariable] = updatedVariable;
                }*/
            }
        }

        public string ContainsVariable(string lineOfCode, int lineNumber)
        {
            if (!lineOfCode.Contains('='))
            {
                string[] independent = lineOfCode.Split(' ');
                foreach (string s in independent)
                {
                    if (s.Contains('*'))
                    {
                        if (!Globals.variables[currentTab].ContainsKey(s.Substring(1)))
                        {
                            Globals.variables[currentTab].Add(s.Substring(1), 1);
                            Globals.dependencies[currentTab][s.Substring(1)] = new List<string>();
                        }
                    }
                    else
                    {
                        if (!Globals.variables[currentTab].ContainsKey(s))
                        {
                            Globals.variables[currentTab].Add(s, 0);
                            Globals.dependencies[currentTab][s] = new List<string>();
                        }
                    }
                }
            }
            else
            {
                string dependent = lineOfCode.Substring(0, lineOfCode.IndexOf('='));
                currentDependent = dependent.Trim();
                Globals.dependencies[currentTab].Add(currentDependent, new List<string>());
                //Globals.dependencies[currentTab][dependent.Trim()] = new List<string>();
                string expression = lineOfCode.Substring(lineOfCode.IndexOf('=') + 1).Trim();
                Globals.expressions[currentTab].Add(currentDependent, expression);
                //Globals.expressions[currentTab][dependent.Trim()] = expression;
                int x = SolveExpression(expression, lineNumber);
                if (!Globals.variables[currentTab].ContainsKey(dependent.Trim()))
                {
                    Globals.variables[currentTab].Add(dependent.Trim(), x);
                }
                return expression;
            }
            return lineOfCode;
        }

        public int SolveExpression(string expression, int lineNumber)
        {
            int expFinal = -1;
            string operation = "";
            string[] tokens = expression.Split(' ');
            foreach (string s in tokens)
            {
                if (!s.Equals(string.Empty))
                {
                    if (s[0].Equals('~'))
                    {
                        if (Globals.variables[currentTab].ContainsKey(s.Substring(1)))
                        {
                            expFinal = Negate(Globals.variables[currentTab][s.Substring(1)]);
                            if (!Globals.dependencies[currentTab][s.Substring(1)].Contains(currentDependent))
                            {
                                Globals.dependencies[currentTab][s.Substring(1)].Add(currentDependent);
                            }
                        }
                    }
                    else if (Globals.variables[currentTab].ContainsKey(s))
                    {
                        if (expFinal == -1)
                        {
                            expFinal = Globals.variables[currentTab][s];
                            if (!Globals.dependencies[currentTab][s].Contains(currentDependent))
                            {
                                Globals.dependencies[currentTab][s].Add(currentDependent);
                            }
                        }
                        else
                        {
                            //if (String.IsNullOrEmpty(operation))
                            //{
                            //    operation = "*";
                            //}
                            if (String.IsNullOrEmpty(operation))
                            {
                                expFinal = expFinal * Globals.variables[currentTab][s];
                                if (!Globals.dependencies[currentTab][s].Contains(currentDependent))
                                {
                                    Globals.dependencies[currentTab][s].Add(currentDependent);
                                }
                            }
                            else if (operation.Equals("+"))
                            {
                                expFinal = expFinal + Globals.variables[currentTab][s];
                                if (!Globals.dependencies[currentTab][s].Contains(currentDependent))
                                {
                                    Globals.dependencies[currentTab][s].Add(currentDependent);
                                }
                                if (expFinal == 2)
                                {
                                    expFinal = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (s.Equals("+"))
                        {
                            operation = s;
                        }
                        else
                        {
                            //Error
                            //Console.WriteLine("Error {Line " + lineNumber + " | " + s + " is undefined }");
                        }
                    }
                }
            }
            return expFinal;
        }

        public int Negate(int value)
        {
            if (value == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int BinaryToDecimal(string binary)
        {
            int dec = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[binary.Length - i - 1] == '0') continue;
                dec += (int)Math.Pow(2, i);
            }
            return dec;
        }
    }
}
