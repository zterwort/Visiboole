using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisiBoole.Models;

namespace VisibooleTests.Models
{
    [TestClass()]
    public class InputParserTests
    {
        /// <summary>
        /// Tests the ParseInput Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void ParseInputTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);

            inputParser.ParseInput(null);
        }

        /// <summary>
        /// Tests the ContainsVariable Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void ContainsVariableTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);

            inputParser.ParseInput(null);

            inputParser.ContainsVariable("B2 = A3 A2 A1 A0", -1);
            inputParser.ContainsVariable("", -1);
            inputParser.ContainsVariable("A1", -1);

            try
            {
                inputParser.ContainsVariable(null, -1);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Tests the SolveExpression Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void SolveExpressionTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);

            inputParser.ParseInput(null);

            //A0, A1, A2, and A3 are all true (1)
            Assert.AreEqual(inputParser.SolveExpression("A3 + A2 + A1 + A0", -1), 0);
            Assert.AreEqual(inputParser.SolveExpression("~A3 + ~A0", -1), 1);
            Assert.AreEqual(inputParser.SolveExpression("B2 B1 B0", -1), 0);
            Assert.AreEqual(inputParser.SolveExpression("A3 A2 A1 A0", -1), 0);
        }

        /// <summary>
        /// Tests the Negate Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void NegateTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);

            inputParser.ParseInput(null);

            Assert.AreEqual(inputParser.Negate(0), 1);
            Assert.AreEqual(inputParser.Negate(1), 0);
        }

        /// <summary>
        /// Tests the BinaryToDecimal Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void BinaryToDecimalTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);

            inputParser.ParseInput(null);

            Assert.AreEqual(inputParser.BinaryToDecimal("1010"), 10);
        }
    }
}