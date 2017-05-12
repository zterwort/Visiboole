using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisiBoole.Models;
using VisiBoole.ParsingEngine;
using VisiBoole.ParsingEngine.ObjectCode;

namespace VisibooleTests.Models
{
    [TestClass()]
    public class HtmlBuilderTests
    {
        /// <summary>
        /// Tests the HtmlBuilder Method inside of /Models/HtmlBuilder.cs
        /// </summary>
        [TestMethod()]
        public void HtmlBuilderTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);
            OutputParser outputParser = new OutputParser();

            inputParser.ParseInput(null);

            List<string> outputText = outputParser.GenerateOutput();

            Parser p = new Parser();

            List<IObjectCodeElement> output = p.Parse(subDesign, null, false);
            Assert.IsNotNull(output);

            HtmlBuilder html = new HtmlBuilder(output);
            Assert.IsNotNull(html);

        }

        /// <summary>
        /// Tests the GetHTML Method inside of /Models/HtmlBuilder.cs
        /// </summary>
        [TestMethod()]
        public void GetHTMLTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);
            OutputParser outputParser = new OutputParser();

            inputParser.ParseInput(null);

            Parser p = new Parser();

            List<IObjectCodeElement> output = p.Parse(subDesign, null, false);
            Assert.IsNotNull(output);

            HtmlBuilder html = new HtmlBuilder(output);
            Assert.IsNotNull(html.GetHTML());

        }
    }
}