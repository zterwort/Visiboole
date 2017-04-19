using System;
using System.Collections.Generic;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine
{
    /// <summary>
    /// The database containing useful data that is parsed by the parsing engine along with their corresponding accessor methods
    /// </summary>
	public static class Database
	{
        /// <summary>
        /// All independent variables parsed by the parsing engine
        /// </summary>
		private static readonly Dictionary<string, IndependentVariable> IndVars = new Dictionary<string, IndependentVariable>();

	    /// <summary>
	    /// All dependent variables parsed by the parsing engine
	    /// </summary>
        private static readonly Dictionary<string, DependentVariable> DepVars = new Dictionary<string, DependentVariable>();

	    /// <summary>
	    /// All variables parsed by the parsing engine
	    /// </summary>
        public static readonly Dictionary<string, Variable> AllVars = new Dictionary<string, Variable>();

        // Dependencies - List of all variables in the expression that 
        //                relates to the dependent variable for the expression

        /// <summary>
        /// List of all variables in the expression that relates to the dependent variable for the expression
        /// </summary>
        private static readonly Dictionary<string, List<string>> Dependencies = new Dictionary<string, List<string>>();

        /// <summary>
        /// expression that relates to the dependent variable for the expression
        /// </summary>
        private static readonly Dictionary<string, string> Expressions = new Dictionary<string, string>();

        /// <summary>
        /// list of "compiled" VisiBoole Object Code. Each item has text and value to be interpreted by the HTML parser
        /// </summary>
		private static List<IObjectCodeElement> ObjectCode { get; set; }

	    #region Accessor methods

        public static Dictionary<string, DependentVariable> GetDepVars()
        {
            return DepVars;
        }
        public static Dictionary<string, IndependentVariable> GetIndVars()
        {
            return IndVars;
        }

        public static void SetOutput(List<IObjectCodeElement> list)
        {
            ObjectCode = list;
        }

        public static List<IObjectCodeElement> GetOutput()
        {
            return ObjectCode;
        }

        public static void SetDepVar(string name, bool value)
        {
            DepVars[name].Value = value;
        }

        /// <summary>
        /// Adds a variable to the collection of variables of the given type
        /// </summary>
        /// <typeparam name="T">The type matching the target collection of variables</typeparam>
        /// <param name="v">The variable to add to the collection of matching type</param>
        /// <returns>Returns true if the variable was successfully added</returns>
		public static bool AddVariable<T>(T v)
		{
			Type varType = typeof(T);
			if (varType == typeof(IndependentVariable))
			{
				IndependentVariable iv = (IndependentVariable)Convert.ChangeType(v, typeof(IndependentVariable));
                if (!IndVars.ContainsKey(iv.Name))
                {
                    IndVars.Add(iv.Name, iv);
                }
				if (!AllVars.ContainsKey(iv.Name))
                {
                    AllVars.Add(iv.Name, iv);
                }
			}
			else
			{
				DependentVariable dv = (DependentVariable)Convert.ChangeType(v, typeof(DependentVariable));
                if (!DepVars.ContainsKey(dv.Name))
                {
				    DepVars.Add(dv.Name, dv);
                }
                if (!AllVars.ContainsKey(dv.Name))
                {
                    AllVars.Add(dv.Name, dv);
                }
			}
			return true;
		}

        /// <summary>
        /// Fetches a variable from the collection of variables matching the given type
        /// </summary>
        /// <typeparam name="T">The type of the collection of variables to search</typeparam>
        /// <param name="name">The string representation of the given variable to search for</param>
        /// <returns>Returns the variable if it was found, else returns null</returns>
		public static Variable TryGetVariable<T>(string name) where T : Variable
		{
			Type varType = typeof(T);
			if (varType == typeof(IndependentVariable))
			{
				if (IndVars.ContainsKey(name))
					return IndVars[name];
			}
			else if (varType == typeof(DependentVariable))
			{
				if (DepVars.ContainsKey(name))
					return DepVars[name];
			}
			else if (varType == typeof(Variable))
			{
				if (AllVars.ContainsKey(name))
					return AllVars[name];
			}
			return null;
		}
	    

	    #endregion

        /// <summary>
        /// Toggles the value of the given variable in its corresponding collections
        /// </summary>
        /// <param name="variableName">The name of the variable to search for</param>
        public static void VariableClicked(string variableName)
        {
            if(IndVars.ContainsKey(variableName))
            {
                if(IndVars[variableName].Value.Equals(true))
                {
                    IndVars[variableName].Value = false;
                    return;
                }
                else
                {
                    IndVars[variableName].Value = true;
                    return;
                }
            }
            if(DepVars.ContainsKey(variableName))
            {
                if (DepVars[variableName].Value.Equals(true))
                {
                    DepVars[variableName].Value = false;
                    return;
                }
                else
                {
                    DepVars[variableName].Value = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Sets specific value
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public static void SetValue(string variableName, bool value)
        {
            if (IndVars.ContainsKey(variableName))
            {
                IndVars[variableName].Value = value;
                return;
            }
            else if (DepVars.ContainsKey(variableName))
            {
                DepVars[variableName].Value = value;
                return;
            }
            else
            {
                IndependentVariable Ind = new IndependentVariable(variableName, value);
                IndVars.Add(variableName, Ind);
            }
        }

        /// <summary>
        /// Creates a list containing the expression associated with the dependent variable
        /// </summary>
        /// <param name="dependentName"></param>
        public static void CreateDependenciesList(string dependentName)
        {
            if(!Dependencies.ContainsKey(dependentName))
            {
                Dependencies.Add(dependentName, new List<string>());
            }
        }

        /// <summary>
        /// Adds the given variable name to the list of dependencies it is associated with
        /// </summary>
        /// <param name="dependentName">The name of the dependent variable containing the expression</param>
        /// <param name="ExpressionVariableName">The name of the variable to add to the dependency list</param>
        public static void AddDependencies(string dependentName, string ExpressionVariableName)
        {
            if(!Dependencies[dependentName].Contains(ExpressionVariableName))
            {
                Dependencies[dependentName].Add(ExpressionVariableName);
            }
        }

        /// <summary>
        /// Adds the expression to the collection of expressions associated with the given dependent variable
        /// </summary>
        /// <param name="dependentName">The name of the variable containing the expression</param>
        /// <param name="expressionValue">The expression to add to the collection of expressions associated with the given dependent variable</param>
        public static void AddExpression(string dependentName, string expressionValue)
        {
            if (Expressions.ContainsKey(dependentName))
            {
                if (!Expressions[dependentName].Contains(expressionValue))
                {
                    Expressions[dependentName] = expressionValue;
                }
            }
            else
            {
                Expressions.Add(dependentName, expressionValue);
            }
        }
	}
}
