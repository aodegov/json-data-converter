using DataConverter.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            JObject exportDb = GetDBData();

            using (StreamWriter file = File.CreateText(this.configuration.DTDbOutputFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                exportDb.WriteTo(writer);
            }
        }

        private JObject GetDBData()
        {
            IBuilder shopsBuilder = new ShopsBuilder(this.configuration);
            var shops = shopsBuilder.BuildData();

            IBuilder sellersBuilder = new SellersBuilder(this.configuration);
            var sellers = sellersBuilder.BuildData();

            IBuilder customersBuilder = new CustomerBuilder(this.configuration);
            var customers = customersBuilder.BuildData();

            IBuilder groupsBuilder = new GroupsBuilder(this.configuration);
            var groups = groupsBuilder.BuildData();

            IBuilder productsBuilder = new ProductsBuilder(this.configuration);
            var products = productsBuilder.BuildData();

            return new JObject(shops, sellers, customers, groups, products);
        }
    }
}
