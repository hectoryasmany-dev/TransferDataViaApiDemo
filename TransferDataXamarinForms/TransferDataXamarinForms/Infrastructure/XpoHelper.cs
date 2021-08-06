using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using TransferDataWinBackend.Module.BusinessObjects;

namespace TransferDataXamarinForms.Infrastructure
{
    public static class XpoHelper
    {
        static readonly Type[] entityTypes = new Type[] {
            typeof(Clients)
        };
        public static void InitXpo(string connectionString)
        {
            var dictionary = PrepareDictionary();
            var conection = WebApiDataStoreClient.GetConnectionString(connectionString);
            if (XpoDefault.DataLayer == null)
            {
                using (var updateDataLayer = XpoDefault.GetDataLayer(conection, dictionary, AutoCreateOption.DatabaseAndSchema))
                {
                    updateDataLayer.UpdateSchema(false, dictionary.CollectClassInfos(entityTypes));
                }
            }

            //var dataStore = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.SchemaAlreadyExists);
            var dataStore = XpoDefault.GetConnectionProvider(conection, AutoCreateOption.SchemaAlreadyExists);

            XpoDefault.DataLayer = new ThreadSafeDataLayer(dictionary, dataStore);
            XpoDefault.Session = null;


        }
        public static UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
        static XPDictionary PrepareDictionary()
        {
            var dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(entityTypes);
            return dict;
        }

    }
}
