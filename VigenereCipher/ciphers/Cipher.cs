using System.Security.Cryptography.Xml;
using Microsoft.VisualBasic;

namespace VigenereCipher
{
    public static class Cipher
    {
        private const string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public static string Encrypt(string key, string text)
        {
            string lowerText = text.ToLower();
            string lowerKey = key.ToLower();
            checkKeyValid(key);

            string result = "";
            int letterIndex = 0;

            foreach (var textSymbol in lowerText)
            {
                if (abc.Contains(textSymbol.ToString()))
                {
                    var textSymbolNumber = abc.IndexOf(textSymbol);
                    var keySymbol = lowerKey[letterIndex % lowerKey.Length];
                    var keySymbolNumber = abc.IndexOf(keySymbol);
                    var sum = textSymbolNumber + keySymbolNumber;
                    var resultSymbolNumber = sum > 33 ? sum - 33 : sum;
                    result += abc[resultSymbolNumber];

                    letterIndex++;
                }
                else
                {
                    result += textSymbol;
                }
            }

            return result;
        }

        public static string Decrypt(string key, string text)
        {
            string lowerText = text.ToLower();
            string lowerKey = key.ToLower();
            checkKeyValid(lowerKey);

            string result = "";
            int letterIndex = 0;


            foreach (var textSymbol in lowerText)
            {
                if (abc.Contains(textSymbol.ToString()))
                {
                    var textSymbolNumber = abc.IndexOf(textSymbol);
                    var keySymbolNumber = abc.IndexOf(lowerKey[letterIndex % lowerKey.Length]);
                    var dis = textSymbolNumber - keySymbolNumber;
                    var resultSymbolNumber = dis < 0 ? dis + 33 : dis;
                    result += abc[resultSymbolNumber];

                    letterIndex++;
                }
                else
                {
                    result += textSymbol;
                }
            }

            return result;
        }

        private static void checkKeyValid(string key)
        {
            foreach (var symbol in key)
            {
                if (!abc.Contains(symbol.ToString()))
                {
                    throw new KeyException("Invalid key value");
                }
            }
        }

    }
}