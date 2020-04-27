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
    public class CipherTests
    {
        delegate string CipherMethod(string key, string text); 
        [TestMethod()]
        public void DecryptTest()
        {
            // arrange
            var key = "Кларнет";
            var text = "хлРь б пюкьы дшхтц цобнрюё";
            var expected = "карл у клары украл кораллы";
            

            // act
            var result = Cipher.Decrypt(key, text);

            // assert
            Assert.AreEqual(expected, result);

        }
        [TestMethod()]
        public void DecryptNotRussianTextTest()
        {
            // arrange
            var key = "кларнет";
            var text = "хлрN,ь б пkekekюкьы дшхтц ;цобнрюё";
            var expected = "карn,л у кkekekлары украл ;кораллы";


            // act
            var result = Cipher.Decrypt(key, text);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void DecryptNotRussianKeyTest()
        {
            // arrange
            var key = "абвгhg&";
            var text = "Карл у Клары украл кораллы";

            // assert
            Assert.ThrowsException<KeyException>(() => Cipher.Decrypt(key, text));
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // arrange
            var key = "кларнет";
            var text = "Карл у Клары украл кораллы";
            var expected = "хлрь б пюкьы дшхтц цобнрюё";


            // act
            var result = Cipher.Encrypt(key, text);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptNotRussianTextTest()
        {
            // arrange
            var key = "кларнет";
            var text = "КарN,л у Кkekekлары украл ;кораллы";
            var expected = "хлрn,ь б пkekekюкьы дшхтц ;цобнрюё";


            // act
            var result = Cipher.Encrypt(key, text);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptNotRussianKeyTest()
        {
            // arrange
            var key = "абвгhg&";
            var text = "Карл у Клары украл кораллы";

            // assert
            Assert.ThrowsException<KeyException>(()=>Cipher.Encrypt(key,text));
        }
    }
}