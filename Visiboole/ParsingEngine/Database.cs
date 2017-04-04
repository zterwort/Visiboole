using System;
using System.Collections.Generic;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine
{
	public static class Database
	{
		private static readonly Dictionary<string, IndependentVariable> IndVars = new Dictionary<string, IndependentVariable>();
		private static readonly Dictionary<string, DependentVariable> DepVars = new Dictionary<string, DependentVariable>();
		private static readonly Dictionary<string, Variable> AllVars = new Dictionary<string, Variable>();
		public static readonly List<IObjectCodeElement> ObjectCode = new List<IObjectCodeElement>();

		public static bool AddVariable<T>(T v)
		{
			Type varType = typeof(T);
			if (varType == typeof(IndependentVariable))
			{
				IndependentVariable iv = (IndependentVariable)Convert.ChangeType(v, typeof(IndependentVariable));
				IndVars.Add(iv.Name, iv);
				AllVars.Add(iv.Name, iv);
			}
			else
			{
				DependentVariable dv = (DependentVariable)Convert.ChangeType(v, typeof(DependentVariable));
				DepVars.Add(dv.Name, dv);
				AllVars.Add(dv.Name, dv);
			}
			return true;
		}

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
	}
}
