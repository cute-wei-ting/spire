using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace Pdf_Font_Not_Display_Correctly
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var doc = new Document();
            Section section = doc.AddSection();
            Paragraph paragraph = section.AddParagraph();
            TextRange tr = paragraph.AppendText("HELLO 你好啊嘗試標楷體");
            TextRange tr2 = paragraph.AppendText("HELLO 你好啊嘗試細明體");
            tr.CharacterFormat.FontName = "標楷體";
            tr2.CharacterFormat.FontName = "細明體";

            ToPdfParameterList ppl = new ToPdfParameterList()
            {
                PrivateFontPaths = new List<PrivateFontPath>()

                {

                    new PrivateFontPath("標楷體", "font/kaiu.ttf"),
                    new PrivateFontPath("細明體", "font/mingliu.ttc")
                }

            };

            //method 1
            doc.SaveToFile("outputs/testSaveToFile.pdf", ppl);


            //method 2
            var ms = new MemoryStream();
            doc.SaveToStream(ms, ppl);
            using (FileStream fs = new FileStream("outputs/testSaveToStream.pdf", FileMode.OpenOrCreate))
            {
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fs);
                fs.Flush();
            }
        }
    }
}