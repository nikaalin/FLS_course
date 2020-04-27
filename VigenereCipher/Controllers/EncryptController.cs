using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VigenereCipherAPI;
using FormatException = VigenereCipherAPI.FormatException;

namespace VigenereCipher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptController : ControllerBase
    {
        // POST: api/Encrypt
        [HttpPost]
        public TextModel Post(string key, string format)
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            try
            {

                var path = "files/encrypt_temp." + format;

                if (format == "string")
                    using (var stream = Request.Body)
                    {
                        var text = new StreamReader(stream).ReadToEnd();
                        return new TextModel() {Key = key, SourceText = text, ResultText = Cipher.Encrypt(key, text)};
                    }
                else if (format == "docx" || format == "txt")
                {
                    using (var file = Request.Body)
                    {
                        using (var stream = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            file.CopyTo(stream);
                        }
                    }

                    if (format == "docx")
                    {
                        return FileCipher.Encrypt(path, key);
                    }
                    else
                    {
                        var text = System.IO.File.ReadAllText(path);
                        var res = Cipher.Encrypt(key, text);
                        return new TextModel() {Key = key, SourceText = text, ResultText = res};
                    }
                }
                else
                    throw new FormatException("Неверный формат");
            }
            catch (Exception e)
            {
                return new TextModel(){Key=key,SourceText = e.Message, ResultText = "Так не пойдет. Попробуйте по-другому"};
            }
        }
    }
}
