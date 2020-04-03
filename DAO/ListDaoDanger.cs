using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab2.DataSource;
using Lab2.Entities;
using Lab2.IDAO;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Lab2.DAO
{
    public class DaoDanger: IDao<Danger>
    {
        private static DaoDanger instance;
        private List<Danger> dangerList;
        private IDSManager<Danger> dsManager;

        private DaoDanger()
        {
            dsManager = DangerExcelManager.GetInstance();
            dangerList = dsManager.GetSourceAsList();
        }

        public static DaoDanger GetInstance()
        {
            return instance ?? (instance = new DaoDanger());
        }


        public void Update(Danger danger)
        {
            dangerList[danger.Id] = danger;
        }

        public void Remove(Danger danger)
        {
            dangerList.Remove(danger);
        }

        public void Remove(int id)
        {
            dangerList.RemoveAt(id);
        }

        public void Create(Danger danger)
        {
            dangerList.Add(danger);
        }

        public Danger Get(int id)
        {
            return dangerList[id];
        }

        public List<Danger> GetAll()
        {
            return dangerList;
        }

        public void Commit()
        {
            dsManager.RewriteDataFromList(dangerList);
        }


    }
}