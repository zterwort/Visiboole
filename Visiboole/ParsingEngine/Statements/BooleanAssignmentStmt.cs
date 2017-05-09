using System.Linq;
using System.Text.RegularExpressions;
using VisiBoole.ParsingEngine.ObjectCode;
using VisiBoole.ParsingEngine.Boolean;
using System;

namespace VisiBoole.ParsingEngine.Statements
{
    /// <summary>
    /// The Boolean assignment statement is the primary type of statement used to
    /// create digital designs. Assignment statements specify the value of a Boolean variable as a
    /// (digital logic) function of other Boolean variables. Its format is a variable name followed by
    /// either an equal sign or a less-than equal pair followed by a Boolean logic expression.Each such
    /// statement represents a network of logic gates and wires.
    /// </summary>
	public class BooleanAssignmentStmt : Statement
	{
        /// <summary>
        /// The identifying pattern that can be used to identify and extract this statement from raw text
        /// </summary>
		public static Regex Pattern { get; } = new Regex(@"^\w{1,20} <?= ~?(~?\(*?\w{1,20}?\)*? ?\+? ?)+~?\w{1,20}\)?;$");

        /// <summary>
        /// Constructs an instance of BooleanAssignmentStmt
        /// </summary>
        /// <param name="lnNum">The line number that this statement is located on within edit mode - not simulation mode</param>
        /// <param name="txt">The raw, unparsed text of this statement</param>
		public BooleanAssignmentStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

	    /// <summary>
	    /// Parses the Text of this statement into a list of discrete IObjectCodeElement elements
	    /// to be used by the html parser to generate formatted output to be displayed in simulation mode.
	    /// </summary>
        public override void Parse()
        {
            //line of code to start with
            string fullExpression = Text;

            if(fullExpression.Contains(';'))
            {
                fullExpression = fullExpression.Substring(0, fullExpression.IndexOf(';'));
            }

            //get our dependent variable and expression
            string dependent = fullExpression.Substring(0, fullExpression.IndexOf('='));
            string expression = fullExpression.Substring(fullExpression.IndexOf('=') + 1);

            //format the dependent variable and expression
            dependent = dependent.Trim();
            expression = expression.Trim();

            //add the expression to this dependent variable
            Database.AddExpression(dependent, expression);

            //create dependencies list to add expression variables too
            Database.CreateDependenciesList(dependent);

            //compute our expression and set it to dependentValue
            Expression exp = new Expression();
            bool dependentValue = exp.Solve(expression);

            //make a dependent variable
            DependentVariable depVariable = Database.TryGetVariable<DependentVariable>(dependent) as DependentVariable;
            if(depVariable != null)
            {
                Database.SetDepVar(dependent, dependentValue);
            }
            else
            {
                depVariable = new DependentVariable(dependent, dependentValue);
            }

            //add the variable to the Database
            Database.AddVariable<DependentVariable>(depVariable);

            MakeOrderedOutput(depVariable, expression);
        }

        /// <summary>
        /// Arranges the output (IObjectCodeElement) elements to represent this statement as it is written, left to right.
        /// </summary>
        /// <param name="dependentVar">The dependent variable being assigned to in the given expression</param>
        /// <param name="expression">The string boolean expression associated with the given dependent variable</param>
        private void MakeOrderedOutput(DependentVariable dependentVar, string expression)
        {
            //Add dependent to output
            Output.Add(dependentVar);

            //Add sign to output
            Operator sign = new Operator("=");
            Output.Add(sign);

            //Add expression variables to output
            string exp = expression;
            string[] elements = exp.Split(' ');
            foreach (string item in elements)
            {
                string variable = item.Trim();
                if(variable.Contains('~'))
                {
                    int closedParenCount = 0;
                    while (variable.Contains("("))
                    {
                        Parentheses openParen;
                        try
                        {
                            if (variable[variable.IndexOf('(') - 1] == '~')
                            {
                                Operator notGate = new Operator("~");
                                openParen = new Parentheses("(");
                                variable = variable.Remove(variable.IndexOf('(') - 1, 2);
                                Output.Add(notGate);
                            }
                            else
                            {
                                openParen = new Parentheses("(");
                                variable = variable.Remove(variable.IndexOf('('), 1);
                            }
                        }
                        catch (Exception e)
                        {
                            openParen = new Parentheses("(");
                            variable = variable.Remove(variable.IndexOf('('), 1);
                        }

                        Output.Add(openParen);
                    }

                    while (variable.Contains(")"))
                    {
                        variable = variable.Remove(variable.IndexOf(')'), 1);
                        closedParenCount++;
                    }

                    string newVariable = variable;
                    //If it STILL contains a not gate. HACK.
                    if (variable.Contains('~'))
                    {
                        newVariable = variable.Substring(1);
                    }
                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(newVariable) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(newVariable) as DependentVariable;
                    if (indVar != null)
                    {
                        IndependentVariable var = new IndependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else if (depVar != null)
                    {
                        DependentVariable var = new DependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else
                    {
                        Operator op = new Operator(variable);
                        Output.Add(op);
                    }

                    for (int i = closedParenCount; i != 0; i--)
                    {
                        Parentheses closedParen = new Parentheses(")");
                        Output.Add(closedParen);
                    }
                }
                else if(variable.Contains('('))
                {
                    while (variable.Contains("("))
                    {
                        Parentheses openParen;
                        try
                        {
                            if (variable[variable.IndexOf('(') - 1] == '~')
                            {
                                Operator notGate = new Operator("~");
                                openParen = new Parentheses("(");
                                variable = variable.Remove(variable.IndexOf('(') - 1, 2);
                                Output.Add(notGate);
                            }
                            else
                            {
                                openParen = new Parentheses("(");
                                variable = variable.Remove(variable.IndexOf('('), 1);
                            }
                        }
                        catch (Exception e)
                        {
                            openParen = new Parentheses("(");
                            variable = variable.Remove(variable.IndexOf('('), 1);
                        }

                        Output.Add(openParen);
                    }

                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(variable) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(variable) as DependentVariable;
                    if (indVar != null)
                    {

                        IndependentVariable var = new IndependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else if (depVar != null)
                    {

                        DependentVariable var = new DependentVariable(variable, depVar.Value);
                        Output.Add(var);
                    }
                    else
                    {
                        Operator op = new Operator(variable);
                        Output.Add(op);
                    }
                }
                else if(variable.Contains(')'))
                {
                    int closedParenCount = 0;
                    while (variable.Contains(")"))
                    {
                        variable = variable.Remove(variable.IndexOf(')'), 1);
                        closedParenCount++;
                    }

                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(variable) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(variable) as DependentVariable;
                    if (indVar != null)
                    {
                        IndependentVariable var = new IndependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else if (depVar != null)
                    {
                        DependentVariable var = new DependentVariable(variable, depVar.Value);
                        Output.Add(var);
                    }
                    else
                    {
                        Operator op = new Operator(variable);
                        Output.Add(op);
                    }

                    for (int i = closedParenCount; i != 0; i--)
                    {
                        Parentheses closedParen = new Parentheses(")");
                        Output.Add(closedParen);
                    }
                }
                else if(variable.Contains('~') && variable.Contains(';'))
                {
                    string newVariable = variable.Substring(1, variable.IndexOf(';'));
                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(newVariable) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(newVariable) as DependentVariable;
                    if (indVar != null)
                    {
                        IndependentVariable var = new IndependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else if (depVar != null)
                    {
                        DependentVariable var = new DependentVariable(variable, indVar.Value);
                        Output.Add(var);
                    }
                    else
                    {
                        Operator op = new Operator(variable);
                        Output.Add(op);
                    }
                }
                else
                {
                    if (variable.Contains(';'))
                    {
                        variable = variable.Substring(0, variable.IndexOf(';'));
                    }
                    IndependentVariable indVar = Database.TryGetVariable<IndependentVariable>(variable) as IndependentVariable;
                    DependentVariable depVar = Database.TryGetVariable<DependentVariable>(variable) as DependentVariable;

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
                        Operator op = new Operator(variable);
                        Output.Add(op);
                    }
                }
            }

            //Add linefeed to output
            LineFeed lf = new LineFeed();
            Output.Add(lf);
        }

    }

}
