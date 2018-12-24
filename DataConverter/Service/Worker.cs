using DataConverter.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace DataConverter.Service
{
    public class Worker : IWorker
    {
        private readonly ConverterConfiguration configuration;

        public Worker(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void CreateDB()
        {
            IBuilder customersBuilder = new CustomerBuilder(this.configuration);
            var customers = customersBuilder.BuildData();

            IBuilder groupsBuilder = new GroupsBuilder(this.configuration);
            var groups = groupsBuilder.BuildData();

            IBuilder productsBuilder = new ProductsBuilder(this.configuration);
            var products = productsBuilder.BuildData();

            JObject exportDb = GetDBData(customers, groups, products);

            using (StreamWriter file = File.CreateText(this.configuration.PixikaDbOutputFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                exportDb.WriteTo(writer);
            }
        }

        private JObject GetDBData(JProperty customers, JProperty groups, JProperty products)
        {
            return new JObject(customers, groups, products);
        }
    }
}
