using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace Lab2.DataSource
{
    public abstract class ExcelManager<T> : IDSManager<T>
    {
        private const string sourceUrl = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        protected const string localUrl = "thrlist.xlsx";
        private const string prevLocalUrl = "thrlistOld.xlsx";

        public void Create()
        {
            downloadFile(sourceUrl, localUrl);
        }

        public void UpdateFromRemote()
        {
            File.Copy(localUrl, prevLocalUrl);
            downloadFile(sourceUrl, localUrl);
        }

        public void Delete()
        {
            File.Copy(localUrl, prevLocalUrl);
            File.Delete(localUrl);
        }

        public void Repair()
        {
            File.Copy(prevLocalUrl, localUrl);
        }

        public abstract List<T> GetSourceAsList();

        public abstract void RewriteDataFromList(List<T> list);

        private void downloadFile(string source, string receiver)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(source, receiver);
            }
        }
    }
}