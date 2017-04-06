﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine.Statements
{
	public class BooleanAssignmentStmt : Statement
	{
		public static Regex Pattern { get; } = new Regex(@"^\w{1,20} <?= ~?(~?\(?\w{1,20}\)? ?\+? ?)+~?\w{1,20}\)?;$");

		public BooleanAssignmentStmt(int lnNum, string txt) : base(lnNum, txt)
		{
		}

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
            bool dependentValue = SolveExpression(dependent, expression);

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
                    string newVariable = variable.Substring(1);
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
                else if(variable.Contains('('))
                {
                    string newVariable = variable.Substring(1);
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
                else if(variable.Contains(')'))
                {
                    string newVariable = variable.Substring(0, variable.IndexOf(')'));
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

        private bool SolveExpression(string dependent, string expression)
        {
            string fullExp = expression;
            string exp = "";
            string value = "";
            while (!GetInnerMostExpression(fullExp).Equals(fullExp))
            {
                exp = GetInnerMostExpression(fullExp);
                value = SolveBasicBooleanExpression(dependent ,exp);
                exp = "(" + exp + ")";
                fullExp = fullExp.Replace(exp, value);
            }
            fullExp = SolveBasicBooleanExpression(dependent ,fullExp);
            if (fullExp.Equals("TRUE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetInnerMostExpression(string expression)
        {
            // this variable keeps track of the ('s in the expression.
            int innerStart;
            // this variable makes sure to keep the farthest inward  (  before hitting a  )  .
            int lastStart = 0;
            // this variable finds the index innermost  )  .
            int innerEnd = expression.IndexOf(')');
            // this will be the final expression if there are  ()  within the starting expression.
            string exp;
            // check to see if any  )'s  were found.
            if (innerEnd != -1)
            {
                // chop off the right side of the expression where the  )  starts.
                exp = expression.Substring(0, innerEnd);
                // chop off all  ('s  until there is only one left.
                do
                {
                    innerStart = exp.IndexOf('(');
                    // if there was a  (  found chop off the left side of expression where the  ( starts.
                    if (innerStart != -1)
                    {
                        lastStart = innerStart;
                        exp = exp.Substring(lastStart + 1);
                    }
                } while (innerStart != -1);
                // now return the inner most expression with no  ()'s  .
                return exp;
            }
            return expression;
        }

        private bool GetVariable(string variableName)
        {
            //See if variable was already declared in IndependentVariables
            IndependentVariable indVariable = Database.TryGetVariable<IndependentVariable>(variableName) as IndependentVariable;

            //See if variable was already declared in DependentVariables
            DependentVariable depVariable = Database.TryGetVariable<DependentVariable>(variableName) as DependentVariable;

            //If variable was found in IndependentVariables
            if(indVariable != null)
            {
                //add variable to Output
                //Output.Add(indVariable);

                //return the value of the independent variable
                return indVariable.Value;
            }

            //If variable was found in DependentVariables
            else if(depVariable != null)
            {
                //add variable to Output
                //Output.Add(depVariable);

                //return the value of the dependent variable
                return depVariable.Value;
            }

            //Else the variable was not found
            else
            {
                //create a variable with a false value since it was not declared
                indVariable = new IndependentVariable(variableName, false);
                
                //Now add the variable to the database
                Database.AddVariable<IndependentVariable>(indVariable);

                //Add variable to Output
                //Output.Add(indVariable);

                //return the value of the independent variable
                return indVariable.Value;
            }
        }

        private string SolveBasicBooleanExpression(string dependent, string expression)
        {
            // set basicExpression variable
            string basicExpression = expression;

            // look for [not] gates
            basicExpression = ParseNots(dependent, basicExpression);

            // look for [and] gates
            basicExpression = ParseAnds(dependent, basicExpression);

            // look for [or] gates
            basicExpression = ParseOrs(dependent, basicExpression);

            // return the end result ("TRUE" or "FALSE")
            return basicExpression;
        }

        /// <summary>
        /// NEED TO ADD DEPENDENCIES
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string ParseNots(string dependent, string expression)
        {
            // set basicExpression variable
            string basicExpression = expression;

            //get first not gate's index (if there is one)
            int notGate = basicExpression.IndexOf('~');

            while (notGate != -1)
            {
                // eleminating everything but the varible
                string oldVariable = basicExpression.Substring(notGate);
                if (!oldVariable.IndexOf(' ').Equals(-1))
                {
                    oldVariable = oldVariable.Substring(0, oldVariable.IndexOf(' '));
                }

                // get rid of the ~ so we can check for the variable in the dictionary
                string newVariable = oldVariable.Substring(1);

                bool variableValue = GetVariable(newVariable);

                ///
                /// Might have to switch around
                ///
                if(variableValue)
                {
                    basicExpression = basicExpression.Replace(oldVariable, "FALSE");
                }
                else
                {
                    basicExpression = basicExpression.Replace(oldVariable, "TRUE");
                }

                // Add the variable to the Dependencies
                Database.AddDependencies(dependent, newVariable);

                // find the next not gate
                notGate = basicExpression.IndexOf('~');
            }

            // return expression with [not] gates replaced with values
            return basicExpression;
        }

        /// <summary>
        /// NEED TO ADD DEPENDENCIES
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string ParseAnds(string dependent, string expression)
        {
            // set basicExpression variable
            string basicExpression = expression;

            // split into a string array off of the [or] gate
            string[] andExpression = basicExpression.Split('+');

            // format the expression
            for (int i = 0; i < andExpression.Length; i++)
            {
                andExpression[i] = andExpression[i].Trim();
            }

            // loop through each element
            foreach (string exp in andExpression)
            {
                // break element up to see if it has multiple variables
                string[] elements = exp.Split(' ');

                // make a new array to store int's instead of string's
                int[] inputs = new int[elements.Length];

                // loop through each element to get their boolean value
                for (int i = 0; i < elements.Length; i++)
                {
                    // check for TRUE
                    if (elements[i].Equals("TRUE"))
                    {
                        inputs[i] = 1;
                    }
                    // check for FALSE
                    else if (elements[i].Equals("FALSE"))
                    {
                        inputs[i] = 0;
                    }
                    // check independent and dependent variables
                    else
                    {
                        bool variableValue = GetVariable(elements[i]);
                        if(variableValue)
                        {
                            inputs[i] = 1;
                        }
                        else
                        {
                            inputs[i] = 0;
                        }

                        // Add the variable to the Dependencies
                        Database.AddDependencies(dependent, elements[i]);
                    }
                }
                // applies [and] gate to each input/expression
                if (And(inputs) == 1)
                {
                    // replace variable with TRUE
                    basicExpression = basicExpression.Replace(exp, "TRUE");
                }
                else
                {
                    // replace variable with FALSE
                    basicExpression = basicExpression.Replace(exp, "FALSE");
                }
            }

            // return expression with [and] gates replaced with values
            return basicExpression;
        }

        /// <summary>
        /// NEED TO ADD DEPENDENCIES
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string ParseOrs(string dependent, string expression)
        {
            // set basicExpression variable
            string basicExpression = expression;

            // split into a string array off of the [or] gate
            string[] elements = basicExpression.Split('+');

            // format the expression
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].Trim();
            }

            // make a new array to store int's instead of string's
            int[] inputs = new int[elements.Length];

            // loop through each element of get their boolean value
            for (int i = 0; i < elements.Length; i++)
            {
                // check for TRUE
                if (elements[i].Equals("TRUE"))
                {
                    inputs[i] = 1;
                }
                // check for FALSE
                else if (elements[i].Equals("FALSE"))
                {
                    inputs[i] = 0;
                }
                // check independent and dependent variables
                else
                {
                    bool variableValue = GetVariable(elements[i]);
                    if (variableValue)
                    {
                        inputs[i] = 1;
                    }
                    else
                    {
                        inputs[i] = 0;
                    }

                    // Add the variable to the Dependencies
                    Database.AddDependencies(dependent, elements[i]);
                }
            }
            // compute the whole value of the expression
            int finalValue = Or(inputs);

            // return the result as a string 
            if (finalValue == 1)
            {
                return "TRUE";
            }
            else
            {
                return "FALSE";
            }
        }

        //[Negate]s a variable (1 or 0)
        private int Negate(int value)
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

        //[And]s variable's (1 or 0)
        private int And(int[] values)
        {
            foreach (int value in values)
            {
                if (value == 0)
                {
                    return 0;
                }
            }
            return 1;
        }

        //[Or]s variable's (1 or 0)
        private int Or(int[] values)
        {
            foreach (int value in values)
            {
                if (value == 1)
                {
                    return 1;
                }
            }
            return 0;
        }
    }

}