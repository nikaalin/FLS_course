using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VigenereCipher
{
    public class DocxHandler
    {
        public static string ParseDocx(string path)
        {
            string result = "";
            using (var stream = new MemoryStream())
            {
                using (var fileStream = File.OpenRead(path))
                {


                    var bytes = getStreamBytes(fileStream);
                    stream.Write(bytes, 0, bytes.Length);

                    using (WordprocessingDocument doc =
                        WordprocessingDocument.Open(stream, true))
                    {
                        foreach (var outer in doc.MainDocumentPart.Document.Body.Descendants())
                        {
                            if (outer is Run)
                            {
                                foreach (var inner in outer.Elements())
                                {
                                    result += inner.InnerText;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        

        private static byte[] getStreamBytes(Stream stream)
        {
            using (var resultStream = new MemoryStream())
            {
                stream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}