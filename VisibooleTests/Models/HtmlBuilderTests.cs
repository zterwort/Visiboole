using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisiBoole.Models;

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
            outputParser.Input = subDesign.Text;

            List<string> outputText = outputParser.GenerateOutput();
            HtmlBuilder htmlBuilder = new HtmlBuilder(outputText, filename, subDesign.Variables, subDesign.Expressions);

            Assert.AreNotEqual(htmlBuilder.HtmlText, null);
            Assert.AreNotEqual(htmlBuilder.HtmlText, "");
            Assert.AreNotEqual(htmlBuilder.currentLine, 0);
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
            outputParser.Input = subDesign.Text;

            List<string> outputText = outputParser.GenerateOutput();
            HtmlBuilder htmlBuilder = new HtmlBuilder(outputText, filename, subDesign.Variables, subDesign.Expressions);
            string html = htmlBuilder.GetHTML();

            Assert.IsNotNull(html);
            Assert.IsNotNull(html, "");
        }

        /// <summary>
        /// Tests the DisplayHtml Method inside of /Models/HtmlBuilder.cs
        /// For some odd reason, it fails when hitting "Run all tests" But passes when you step through a debug.
        /// </summary>
        [TestMethod()]
        public void DisplayHtmlTest()
        {
            string filename = "newFile1.vbi";
            SubDesign subDesign = new SubDesign(filename);
            FileInfo file = new FileInfo(filename);
            InputParser inputParser = new InputParser(subDesign);
            OutputParser outputParser = new OutputParser();
            WebBrowser browser = new WebBrowser();

            inputParser.ParseInput(null);
            outputParser.Input = subDesign.Text;

            List<string> outputText = outputParser.GenerateOutput();
            HtmlBuilder htmlBuilder = new HtmlBuilder(outputText, filename, subDesign.Variables, subDesign.Expressions);
            string html = htmlBuilder.GetHTML();

            htmlBuilder.DisplayHtml(html, browser);
        }
    }
}