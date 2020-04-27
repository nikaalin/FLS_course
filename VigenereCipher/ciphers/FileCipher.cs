using System;
using System.Linq;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using VigenereCipher;
using VigenereCipherAPI;

namespace VigenereCipher
{
    public class FileCipher
    {
        private const string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        

        public static TextModel Encrypt(string path, string key)
        {

            var text = DocxHandler.ParseDocx(path);

            var result = "";
            try
            {
                result = Cipher.Encrypt(key, text);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            TextModel model = new TextModel() { Key = key, SourceText = text, ResultText = result};
            return model;
        }

        public static TextModel Decrypt(string path, string key)
        {
            var text = DocxHandler.ParseDocx(path);

            var result = "";
            try
            {
                result = Cipher.Decrypt(key, text);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            TextModel model = new TextModel() { Key = key, SourceText = text, ResultText = result };
            return model;
        }
    }
}