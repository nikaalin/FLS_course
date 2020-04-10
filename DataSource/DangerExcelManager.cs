using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Lab2.Entities;
using OfficeOpenXml;

namespace Lab2.DataSource
{
    public class DangerExcelManager : ExcelManager<Danger>
    {
        private static DangerExcelManager instance;
        private DangerExcelManager() { }

        public static DangerExcelManager GetInstance()
        {
            return instance ?? (instance = new DangerExcelManager());
        }
        public override List<Danger> GetSourceAsList()
        {
            return Parse(localUrl);
        }

        public override List<Danger> GetOldSourceAsList()
        {
            return Parse(prevLocalUrl);
        }
        public override void RewriteDataFromList(List<Danger> list)
        {
            var path = localUrl;
            using (var file = File.OpenWrite(path))
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                var sheet = excelPackage.Workbook.Worksheets[1];
                foreach (var danger in list)
                {
                    var i = 3;
                    var row = danger.ToString().Split();
                    for (int j = 1; j <= 8; j++)
                    {
                        sheet.Cells[i, j].Value = row[j];
                    }

                    i++;
                }
            }
        }

        private  List<Danger> Parse(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelData = new List<Danger>();
            byte[] bin = File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                var sheet = excelPackage.Workbook.Worksheets[0];
                for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                {
                    var row = new string[8];
                    for (int j = 1; j <= 8; j++)
                    {
                        var value = sheet.Cells[i, j].Value.ToString();
                        row[j - 1] = value;
                    }

                    excelData.Add(new Danger(row));
                }
            }

            return excelData;
        }
    }
}