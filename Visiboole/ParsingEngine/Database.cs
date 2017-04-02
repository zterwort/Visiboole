using System;
using System.Collections.Generic;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisiBoole.ParsingEngine
{
	public static class Database
	{
		private static Dictionary<string, IndependentVariable> _indVars = new Dictionary<string, IndependentVariable>();
		private static Dictionary<string, DependentVariable> _depVars = new Dictionary<string, DependentVariable>();
		private static Dictionary<string, Variable> _allVars = new Dictionary<string, Variable>();

		public static bool AddVariable<T>(T var)
		{
			Type varType = typeof(T);
			if (varType == typeof(IndependentVariable))
			{
				IndependentVariable iv = (IndependentVariable) Convert.ChangeType(var, typeof(IndependentVariable));
				_indVars.Add(iv.Name, iv);
			}
			else
			{
				DependentVariable dv = (DependentVariable)Convert.ChangeType(var, typeof(DependentVariable));
				_depVars.Add(dv.Name, dv);
			}
			Variable av = (Variable)Convert.ChangeType(var, typeof(Variable));
			_allVars.Add(av.Name, av);
			return true;
		}
	}
}
