using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisiBoole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VisiBoole.Tests
{
    [TestClass()]
    public class InputParserTests
    {
        /// <summary>
        /// Tests the InputParser Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void InputParserTest()
        {
            InputParser inputParser = new InputParser();
        }

        /// <summary>
        /// Tests the ParseInput Method inside of /Models/InputParser.cs
        /// </summary>
        [TestMethod()]
        public void ParseInputTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser();

            inputParser.ParseInput(subDesign, null);
            inputParser.ParseInput(subDesign, "A3");
            inputParser.ParseInput(subDesign, "B0");

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
            InputParser inputParser = new InputParser();

            inputParser.ParseInput(subDesign, null);

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
            InputParser inputParser = new InputParser();

            inputParser.ParseInput(subDesign, null);

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
            InputParser inputParser = new InputParser();

            inputParser.ParseInput(subDesign, null);

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
            InputParser inputParser = new InputParser();

            inputParser.ParseInput(subDesign, null);

            Assert.AreEqual(inputParser.BinaryToDecimal("1010"), 10);
        }
    }
}