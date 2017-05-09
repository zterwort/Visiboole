using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiBoole.ParsingEngine.ObjectCode
{
    public class Parentheses: IObjectCodeElement
    {
        public bool? Value;

        public string ObjCodeText { get { return OperatorChar; } }

        /// <summary>
        /// The boolean value of this output element, null
        /// </summary>
		public bool? ObjCodeValue { get { return this.Value; } set { this.Value = value; } }

        /// <summary>
        /// The string representation of this element
        /// </summary>
		public string OperatorChar { get; set; }

        public int Match { get; set; }

        public int MatchingIndex { get; set; }

        /// <summary>
        /// Constructs an instance of Operator 
        /// </summary>
        /// <param name="opChar">The string representation of this element</param>
		public Parentheses(string opChar)
        {
            OperatorChar = opChar;
            this.Value = false;
        }
    }
}
