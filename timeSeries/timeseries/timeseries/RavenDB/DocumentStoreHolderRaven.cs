using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace timeseries
{
    public static class DocumentStoreHolderRaven
    {
        static string db1 = "timeseries"; //record every 1 s for 24 hours
        static string db2 = "timeseries2"; //record every 5 s for 24 hours
        static string db3 = "timeseries3"; //record every 10 s for 24 hours

        private static readonly Lazy<IDocumentStore> LazyStore =
        new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { "https://a.frmltrx.development.run/" },
                Database = db1,
                Certificate = new X509Certificate2("C:\\Users\\DELL\\Downloads\\admin.client.certificate.frmltrx.pfx")
            };
            //new DataBySeconds().Execute(store);
            //new DataByDate().Execute(store);
            return store.Initialize();
        });

        public static IDocumentStore Store => LazyStore.Value;
    }
}
