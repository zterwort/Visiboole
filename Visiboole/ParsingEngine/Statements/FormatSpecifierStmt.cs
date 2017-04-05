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

		            // arrange beg and end from smallest to largest
		            if (end < beg)
		            {
		                int temp = beg;
		                beg = end;
		                end = temp;
		            }

		            // add each variable to our output list of object code
		            for (int i = beg; i < end; i++)
		            {
		                string key = string.Concat(var, i);
		                IndependentVariable v = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
		                if (v != null)
		                {
		                    Output.Add(v);
		                }
		                else
		                {
		                    // if a variable wasn't found then the given data is erroneous
		                    // TODO: throw a proper error with metadata
		                    throw new Exception();
		                }
		            }
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
		                    Output.Add(indVar);
		                }
                        else if (depVar != null)
                        {
                            Output.Add(depVar);
                        }
		                else
		                {
		                    // if a variable wasn't found then the given data is erroneous
		                    // TODO: throw a proper error with metadata
		                    throw new Exception();
		                }
		            }
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

        private void ParseFormatSpecifier()
	    {
	        try
	        {
	            // obtain the format specifier token
	            Regex regex = new Regex(@"([ubhd])", RegexOptions.None);
	            string format = regex.Match(Text).Value;

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

	                // arrange beg and end from smallest to largest
	                if (end < beg)
	                {
	                    int temp = beg;
	                    beg = end;
	                    end = temp;
	                }

	                // add each variable to our output list of object code
                    for (int i = beg; i < end; i++)
	                {
	                    string key = string.Concat(var, i);
	                    IndependentVariable v = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
	                    if (v == null)
	                    {
	                        Output.Add(v);	                        
	                    }
                        else
                        {
                            // if a variable wasn't found then the given data is erroneous
                            // TODO: throw a proper error with metadata
                            throw new Exception();
                        }
                    }
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
                        IndependentVariable v = Database.TryGetVariable<IndependentVariable>(key) as IndependentVariable;
	                    if (v == null)
	                    {
	                        Output.Add(v);	                        
	                    }
	                    else
	                    {
	                        // if a variable wasn't found then the given data is erroneous
	                        // TODO: throw a proper error with metadata
	                        throw new Exception();
	                    }
	                }
	            }            

	            // if no values have been gathered, then there was a user syntax error
	            if (Output.Count == 0)
	            {
	                // TODO: throw a proper error with metadata
	                throw new Exception();
	            }
	        }
	        catch (Exception ex)
	        {
	            // TODO: proper exception handling
	            Globals.DisplayException(ex);
	        }
	    }
    }
}
