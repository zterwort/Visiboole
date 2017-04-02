using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisiBoole.Models;

namespace VisibooleTests.Models
{
    [TestClass()]
    public class OutputParserTests
    {
        /// <summary>
        /// Tests the OutputParser Method inside of /Models/OutputParser.cs
        /// </summary>
        [TestMethod()]
        public void OutputParserTest()
        {
            OutputParser outputParser = new OutputParser();
            Assert.AreEqual(outputParser.Input, String.Empty);
        }

        /// <summary>
        /// Tests the GenerateOutput Method inside of /Models/OutputParser.cs
        /// </summary>
        [TestMethod()]
        public void GenerateOutputTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);
            OutputParser outputParser = new OutputParser();

            inputParser.ParseInput(null);
            outputParser.Input = subDesign.Text;

            List<string> outputText = outputParser.GenerateOutput();

            Assert.IsNotNull(outputText);
            Assert.IsNotNull(outputText[0]);
        }
    }
}