using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using VisiBoole.ErrorHandling;
using VisiBoole.Models;
using VisiBoole.ParsingEngine.Statements;

namespace VisiBoole.ParsingEngine.ObjectCode
{
	public class Parser
	{
		public void Parse(SubDesign sd)
		{
			List<Statement> stmtList = ParseStatements(sd);
			foreach (Statement stmt in stmtList)
			{
				stmt.Parse();
			}
		}

		/// <summary>
		/// Parses the source code into discrete statements of their respective visiboole type
		/// </summary>
		/// <param name="sd">The subdesign containing the user source code to be parsed</param>
		/// <returns>Returns a list of visiboole statements, indexed by line number</returns>
		private List<Statement> ParseStatements(SubDesign sd)
		{
			List<Statement> stmtList = new List<Statement>();
			string txt = sd.Text;
			byte[] byteArr = Encoding.UTF8.GetBytes(txt);
			MemoryStream stream = new MemoryStream(byteArr);
			using (StreamReader reader = new StreamReader(stream))
			{
				string nextLine;
				int preLnNum = 0;     // the line number in edit mode, before the text is parsed
				int postLnNum = 0;    // the line number in simulation mode, after the text is parsed
				bool flag = false;    // flag is set to true after the first non-empty/comment is found
				while ((nextLine = reader.ReadLine()) != null)
				{
					// check for an empty statement
					if (string.IsNullOrEmpty(nextLine.Trim()))
					{
						stmtList.Add(new EmptyStmt(postLnNum, nextLine));
						preLnNum++;
						postLnNum++;
						continue;
					}

					// check for a comment
					Match match = CommentStmt.Pattern.Match(nextLine);
					if (match.Success)
					{
						stmtList.Add(new CommentStmt(postLnNum, nextLine));
						preLnNum++;
						postLnNum++;
						continue;
					}

					// collate statement if end of statement not detected
					do
					{
						string substr = reader.ReadLine();
						if (substr == null)
							throw new MissingEndOfStatementException("Expected end of statement. Missing ';' character?", preLnNum);
						preLnNum++;
						nextLine += substr;
					} while (!nextLine.Contains(";"));

					// check for a module declaration statement
					match = ModuleDeclarationStmt.Pattern.Match(nextLine);
					if (flag == false && match.Success)
					{
						stmtList.Add(new ModuleDeclarationStmt(postLnNum, nextLine));
						flag = true;
						preLnNum++;
						postLnNum++;
						continue;
					}

					// check for a boolean assignment statement
					match = BooleanAssignmentStmt.Pattern.Match(nextLine);
					if (match.Success)
					{
						stmtList.Add(new BooleanAssignmentStmt(postLnNum, nextLine));
						flag = true;
						preLnNum++;
						postLnNum++;
						continue;
					}

					// check for a variable list statement
					match = VariableListStmt.Pattern.Match(nextLine);
					if (match.Success)
					{
						stmtList.Add(new VariableListStmt(postLnNum, nextLine));
						flag = true;
						preLnNum++;
						postLnNum++;
						continue;
					}

					// check for a submodule instantiation statement
					match = SubmoduleInstantiationStmt.Pattern.Match(nextLine);
					if (match.Success)
					{
						stmtList.Add(new SubmoduleInstantiationStmt(postLnNum, nextLine));
						flag = true;
						preLnNum++;
						postLnNum++;
					}

					// check for a format specifier statement
					bool success = FormatSpecifierStmt.Pattern1.Match(nextLine).Success || FormatSpecifierStmt.Pattern2.Match(nextLine).Success;
					if (match.Success)
					{
						stmtList.Add(new FormatSpecifierStmt(postLnNum, nextLine));
						flag = true;
						preLnNum++;
						postLnNum++;
					}
				}
			}
			return stmtList;
		}
	}
}
