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
        [TestMethod()]
        public void InputParserTest()
        {
            InputParser inputParser = new InputParser();
        }

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

        [TestMethod()]
        public void SolveExpressionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NegateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BinaryToDecimalTest()
        {
            Assert.Fail();
        }
    }
}