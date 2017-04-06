using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VisiBoole.ErrorHandling;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class FormatSpecifierStmt : Statement
	{
		public static Regex Pattern1 { get; } = new Regex(@"^%[ubhd]{[a-zA-z0-9_]{1,20}\[\d\.\.\d\]};$");
		public static Regex Pattern2 { get; } = new Regex(@"^%[ubhd]{([a-zA-Z0-9_]{1,20} ?)+};$");

		public FormatSpecifierStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

		public override void Parse()
		{
		    try
		    {
		        // obtain the format specifier token
		        Regex regex = new Regex(@"([ubhd])", RegexOptions.None);
		        string format = regex.Match(Text).Value;

                // used to pass into our functions for calculation
                List<int> valueList = new List<int>();

                // strip the surrounding specifier and brackets to get the content
                string content = regex.Replace(Text, string.Empty, 1);
		        regex = new Regex(@"[%{};]", RegexOptions.None);
		        content = regex.Replace(content, string.Empty);

		        // obtain the variables within the content. First search for pattern A[N..n]
		        regex = new Regex(@"[a-zA-Z0-9_]+\[\d+\.\.\d\]", RegexOptions.None);
		        string match = regex.Match(content).Value;
		        if (!string.IsNullOrEmpty(match))
		        {
		            // first pattern found. Expand the expression to extract the variables
		            regex = new Regex(@"[a-zA-Z0-9_]+", RegexOptions.None);
		            string var = regex.Match(match).Value;
		            regex = new Regex(@"\d");
		            MatchCollection matches = regex.Matches(match);
		            int beg = Convert.ToInt32(matches[0].Value);
		            int end = Convert.ToInt32(matches[1].Value);

                    List<int> order = new List<int>();

                    if (beg < end)
                    {
                        for(int i=beg; i <= end; i++)
                        {
                            order.Add(i);
                        }
                    }
                    else // beg > end
                    {
                        for(int i=beg; i >= end; i--)
                        {
                            order.Add(i);
                        }
                    }

		            // add each variable to our output list of object code
                    foreach(int i in order)
		            {
		                string key = string.Concat(var, i);
		                IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
                        DependentVariable depVar = Database.TryGetVariable<DependentVariable>(key) as DependentVariable;
		                if (indVar != null)
		                {
                            if(indVar.Value)
                            {
                                valueList.Add(1);
                            }
                            else
                            {
                                valueList.Add(0);
                            }
		                    //Output.Add(indVar);
		                }
                        else if (depVar != null)
                        {
                            if (depVar.Value)
                            {
                                valueList.Add(1);
                            }
                            else
                            {
                                valueList.Add(0);
                            }
                            //Output.Add(depVar);
                        }
		                else
		                {
                            IndependentVariable newVar = new IndependentVariable(key, false);
                            Database.AddVariable<IndependentVariable>(newVar);
                            valueList.Add(0);
                        }
		            }
                    string final = Calculate(format, valueList);
                    Operator val = new Operator(final);
                    Output.Add(val);
                }
		        else
		        {
		            // first pattern was not found. Search the content for the second pattern: A1 A2 An
		            regex = new Regex(@"[a-zA-Z0-9_]{1,20}", RegexOptions.None);
		            MatchCollection matches = regex.Matches(content);
		            foreach (Match m in matches)
		            {
		                // add each variable to our output list of object code
		                string key = m.Value;
		                IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
                        DependentVariable depVar = Database.TryGetVariable<DependentVariable>(key) as DependentVariable;
                        var x = Database.AllVars;
		                if (indVar != null)
		                {
                            if(indVar.Value)
                            {
                                valueList.Add(1);
                            }
                            else
                            {
                                valueList.Add(0);
                            }
                            //add value to list of ints
		                    //Output.Add(indVar);
		                }
                        else if (depVar != null)
                        {
                            if (depVar.Value)
                            {
                                valueList.Add(1);
                            }
                            else
                            {
                                valueList.Add(0);
                            }
                            //add value to list of ints
                            //Output.Add(depVar);
                        }
		                else
		                {
                            IndependentVariable newVar = new IndependentVariable(key, false);
                            Database.AddVariable<IndependentVariable>(newVar);
                            valueList.Add(0);
		                }
		            }
                    string final = Calculate(format, valueList);
                    Operator val = new Operator(final);
                    Output.Add(val);
		        }

		        // if no values have been gathered, then there was a user syntax error
		        if (Output.Count == 0)
		        {
		            throw new FormatSpecifierSyntaxException("Syntax error. Variables in Variable list statement not recognized.", this);
		        }
		    }
		    catch (Exception ex)
		    {
		        // TODO: proper exception handling
		        Globals.DisplayException(ex);
		    }

        }

        public string Calculate(string specifier, List<int> values)
        {
            switch (specifier.ToUpper())
            {
                case "B":
                    return ToBinary(values);
                case "H":
                    return ToHex(ToBinary(values));
                case "D":
                    return ToSigned(ToBinary(values));
                case "U":
                    return ToUnsigned(ToBinary(values));
                default:
                    return string.Empty;
            }
        }

        private string ToBinary(List<int> _vals)
        {
            string binary = "";
            foreach (var variable in _vals)
            {
                binary += variable.ToString();
            }
            return binary;
        }

        public string ToUnsigned(string binary) // decimal
        {
            int dec = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[binary.Length - i - 1] == '0') continue;
                dec += (int)Math.Pow(2, i);
            }
            return dec.ToString();
        }

        private string ToHex(string binary)
        {
            //string binary = ToBinary(new List<int>());
            return Convert.ToInt32(binary, 2).ToString("X");
        }

        private string ToSigned(string binary)
        {
            int index = binary.IndexOf("1", StringComparison.Ordinal);
            if (index == 0)
            {
                int num = -1 * Convert.ToInt32(ToUnsigned(binary.Substring(1)));
                return num.ToString();
            }
            return ToUnsigned(binary);
        }
    }
}
