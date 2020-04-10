﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace Lab2.DataSource
{
    public abstract class ExcelManager<T> : IDSManager<T>
    {
        private const string sourceUrl = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        protected const string localUrl = "thrlist.xlsx";
        protected const string prevLocalUrl = "thrlistOld.xlsx";

        public string GetLocalFile()
        {
            return localUrl;
        }

        public string GetOldFile()
        {
            return localUrl;
        }

        public void Create()
        {
            if (!downloadFile(sourceUrl, localUrl))
                throw new Exception();
        }

        public bool UpdateFromRemote()
        {
            try
            {
                File.Copy(localUrl, prevLocalUrl, true);
            }
            catch (Exception)
            {
            }
            return downloadFile(sourceUrl, localUrl);
            
        }

        public void Delete()
        {
            File.Copy(localUrl, prevLocalUrl, true);
            File.Delete(localUrl);
        }

        public void Repair()
        {
            File.Copy(prevLocalUrl, localUrl, true);
        }

        public bool ExistLocal()
        {
            return File.Exists(localUrl);
        }

        public void SaveLocal(string path)
        {
            File.Copy(localUrl, path);
        }

        public abstract List<T> GetSourceAsList();
        public abstract List<T> GetOldSourceAsList();

        public abstract void RewriteDataFromList(List<T> list);

        private bool downloadFile(string source, string receiver)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(source, receiver);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}