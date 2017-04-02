using System;
using VisiBoole.Models;

namespace VisiBoole.ErrorHandling
{
	/// <summary>
	/// The exception that is thrown when an empty value is detected that corresponds to a non-nullable field in the database
	/// </summary>
	/// <remarks></remarks>
	public class MissingEndOfStatementException : Exception
	{
		/// <summary>
		/// The edit mode line number that caused this exception
		/// </summary>
		public int LineNumber { get; set; }

		/// <summary>
		/// The control that is the cause of this exception
		/// </summary>
		public Statement SourceStatement { get; set; }

		/// <summary>
		/// Initializes a new instance of the MissingEndOfStatementException class
		/// </summary>
		/// <remarks></remarks>
		public MissingEndOfStatementException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the MissingEndOfStatementException class with a specified error message
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <remarks></remarks>
		public MissingEndOfStatementException(string message) : base(message)
		{
		}
		/// <summary>
		/// Initializes a new instance of the MissingEndOfStatementException class with a specified error message and a reference
		/// to the inner exception that is the cause of this exception
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="inner">The inner Exception if one exists</param>
		/// <remarks></remarks>
		public MissingEndOfStatementException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the MissingEndOfStatementException class with a specified error message and a reference
		/// to the inner exception that is the cause of this exception		
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="lineNumber">The edit mode line number that caused the exception</param>
		public MissingEndOfStatementException(string message, int lineNumber) : base(message)
		{			
		}

		/// <summary>
		/// Initializes a new instance of the MissingEndOfStatementException class with a specified error message and a reference
		/// to the control that is the cause of this exception
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="source">The control that caused the exception</param>
		/// <remarks></remarks>
		public MissingEndOfStatementException(string message, Statement source) : base(message)
		{
			SourceStatement = source;
		}

	}
}