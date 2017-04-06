using System;
using System.Collections.Generic;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine
{
	public static class Database
	{
        // Independent Variables
		private static readonly Dictionary<string, IndependentVariable> IndVars = new Dictionary<string, IndependentVariable>();

        // Dependent Variables
		private static readonly Dictionary<string, DependentVariable> DepVars = new Dictionary<string, DependentVariable>();

        // Dependencies - List of all variables in the expression that 
        //                relates to the dependent variable for the expression
        private static readonly Dictionary<string, List<string>> Dependencies = new Dictionary<string, List<string>>();

        // Expressions - expression that relates to the dependent variable
        //               for the expression
        private static readonly Dictionary<string, string> Expressions = new Dictionary<string, string>();

        // All Variables - list of all variables independent and dependent
        public static readonly Dictionary<string, Variable> AllVars = new Dictionary<string, Variable>();

        // ObjectCode - list of "compiled" VisiBoole Object Code. Each item 
        //              has text and value to be interpreted by the HTML parser
		private static List<IObjectCodeElement> ObjectCode { get; set; }

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
        /// Adds a variable to that respective variables dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
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
        /// Returns the variable if it exists. If the variable does not
        ///   exist then it will simply return null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
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

        public static void CreateDependenciesList(string dependentName)
        {
            if(!Dependencies.ContainsKey(dependentName))
            {
                Dependencies.Add(dependentName, new List<string>());
            }
        }

        public static void AddDependencies(string dependentName, string ExpressionVariableName)
        {
            if(!Dependencies[dependentName].Contains(ExpressionVariableName))
            {
                Dependencies[dependentName].Add(ExpressionVariableName);
            }
        }

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
