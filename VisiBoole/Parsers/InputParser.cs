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
        public InputParser(SubDesign sub)//string[] codeText, string fileName)
        {
            //SubDesign s = new SubDesign("h");
            //currentTab = fileName;
            currentTab = sub.FileSourceName;
            if(!Globals.variables.ContainsKey(currentTab))
            {
                Globals.variables.Add(currentTab, new Dictionary<string, int>());
            }

            using (StreamReader reader = sub.FileSource.OpenText())
            {
                string text = "";
                int lineNumber = 1;
                while ((text = reader.ReadLine()) != null)
                {
                    if(!text.Contains(';'))
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
                        }
                    }
                    else
                    {
                        if (!Globals.variables[currentTab].ContainsKey(s))
                        {
                            Globals.variables[currentTab].Add(s, 0);
                        }
                    }
                }
            }
            else
            {
                string dependent = lineOfCode.Substring(0, lineOfCode.IndexOf('='));
                string expression = lineOfCode.Substring(lineOfCode.IndexOf('=') + 1).Trim();
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
                        }
                    }
                    else if (Globals.variables[currentTab].ContainsKey(s))
                    {
                        if (expFinal == -1)
                        {
                            expFinal = Globals.variables[currentTab][s];
                        }
                        else
                        {
                            if (operation == null)
                            {
                                operation = "*";
                            }
                            if (operation.Equals("*"))
                            {
                                expFinal = expFinal * Globals.variables[currentTab][s];
                            }
                            else if (operation.Equals("+"))
                            {
                                expFinal = expFinal + Globals.variables[currentTab][s];
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
