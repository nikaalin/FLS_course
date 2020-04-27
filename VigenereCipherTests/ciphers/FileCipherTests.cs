using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereCipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Tests
{
    [TestClass()]
    public class DocxHandlerTests
    {
        [TestMethod()]
        public void ReadDocxTest()
        {
            // arrange
            var path = Environment.CurrentDirectory + "\\" + "files\\test.docx";
            var path1 = Environment.CurrentDirectory + "\\" + "files\\test1.docx";
            var path2 = Environment.CurrentDirectory + "\\" + "files\\test2.docx";

            var expected = "Тевирп";
            var expected1 = "Тевирп, второе третье огонь";
            var expected2 = "ТевирпА это параграфНу и что?";


            // act
            var result = DocxHandler.ParseDocx(path);
            var result1 = DocxHandler.ParseDocx(path1);
            var result2 = DocxHandler.ParseDocx(path2);


            // assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);

        }
    }
}