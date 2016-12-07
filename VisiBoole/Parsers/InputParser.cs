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
       
        public string currentDependent;
        public InputParser(SubDesign sub, string variableClicked)//string[] codeText, string fileName)
        {
            Globals.CurrentTab = sub.FileSourceName;
            if (!Globals.dependencies.ContainsKey(Globals.CurrentTab))
            {
                Globals.dependencies.Add(Globals.CurrentTab, new Dictionary<string, List<string>>());
                Globals.expressions.Add(Globals.CurrentTab, new Dictionary<string, string>());
            }
            if (String.IsNullOrEmpty(variableClicked))
            {
                //Globals.CurrentTab = sub.FileSourceName;
                if (!Globals.variables.ContainsKey(Globals.CurrentTab))
                {
                    Globals.variables.Add(Globals.CurrentTab, new Dictionary<string, int>());
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
                //Globals.CurrentTab = sub.FileSourceName;
                int newValue = Negate(Globals.variables[Globals.CurrentTab][variableClicked]);
                Globals.variables[Globals.CurrentTab][variableClicked] = newValue;

                //build list of all dependent variables based on user click
                List<string> totalVariables = new List<string>();
                foreach (string dependentVariable in Globals.dependencies[Globals.CurrentTab][variableClicked])
                {
                    totalVariables.Add(dependentVariable);
                }
                int count = 0;
                int end = totalVariables.Count;
                while (count != end)
                {
                    for(int i = count; i < end; i++)
                    {
                        foreach(string dependentVariable in Globals.dependencies[Globals.CurrentTab][totalVariables[i]])
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
                    int updatedVariable = SolveExpression(Globals.expressions[Globals.CurrentTab][dependentVariable], -1);
                    Globals.variables[Globals.CurrentTab][dependentVariable] = updatedVariable;
                }
                //all dependent variable list(loop through with foreach)
                /*foreach(string dependentVariable in Globals.dependencies[Globals.CurrentTab][variableClicked])
                {                  
                    currentDependent = dependentVariable;
                    int updatedVariable = SolveExpression(Globals.expressions[Globals.CurrentTab][dependentVariable], -1);
                    Globals.variables[Globals.CurrentTab][dependentVariable] = updatedVariable;
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
                        if (!Globals.variables[Globals.CurrentTab].ContainsKey(s.Substring(1)))
                        {
                            Globals.variables[Globals.CurrentTab].Add(s.Substring(1), 1);
                            Globals.dependencies[Globals.CurrentTab][s.Substring(1)] = new List<string>();
                        }
                    }
                    else
                    {
                        if (!Globals.variables[Globals.CurrentTab].ContainsKey(s))
                        {
                            Globals.variables[Globals.CurrentTab].Add(s, 0);
                            Globals.dependencies[Globals.CurrentTab][s] = new List<string>();
                        }
                    }
                }
            }
            else
            {
                string dependent = lineOfCode.Substring(0, lineOfCode.IndexOf('='));
                currentDependent = dependent.Trim();
                Globals.dependencies[Globals.CurrentTab].Add(currentDependent, new List<string>());
                //Globals.dependencies[Globals.CurrentTab][dependent.Trim()] = new List<string>();
                string expression = lineOfCode.Substring(lineOfCode.IndexOf('=') + 1).Trim();
                Globals.expressions[Globals.CurrentTab].Add(currentDependent, expression);
                //Globals.expressions[Globals.CurrentTab][dependent.Trim()] = expression;
                int x = SolveExpression(expression, lineNumber);
                if (!Globals.variables[Globals.CurrentTab].ContainsKey(dependent.Trim()))
                {
                    Globals.variables[Globals.CurrentTab].Add(dependent.Trim(), x);
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
                        if (Globals.variables[Globals.CurrentTab].ContainsKey(s.Substring(1)))
                        {
                            expFinal = Negate(Globals.variables[Globals.CurrentTab][s.Substring(1)]);
                            if (!Globals.dependencies[Globals.CurrentTab][s.Substring(1)].Contains(currentDependent))
                            {
                                Globals.dependencies[Globals.CurrentTab][s.Substring(1)].Add(currentDependent);
                            }
                        }
                    }
                    else if (Globals.variables[Globals.CurrentTab].ContainsKey(s))
                    {
                        if (expFinal == -1)
                        {
                            expFinal = Globals.variables[Globals.CurrentTab][s];
                            if (!Globals.dependencies[Globals.CurrentTab][s].Contains(currentDependent))
                            {
                                Globals.dependencies[Globals.CurrentTab][s].Add(currentDependent);
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
                                expFinal = expFinal * Globals.variables[Globals.CurrentTab][s];
                                if (!Globals.dependencies[Globals.CurrentTab][s].Contains(currentDependent))
                                {
                                    Globals.dependencies[Globals.CurrentTab][s].Add(currentDependent);
                                }
                            }
                            else if (operation.Equals("+"))
                            {
                                expFinal = expFinal + Globals.variables[Globals.CurrentTab][s];
                                if (!Globals.dependencies[Globals.CurrentTab][s].Contains(currentDependent))
                                {
                                    Globals.dependencies[Globals.CurrentTab][s].Add(currentDependent);
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
