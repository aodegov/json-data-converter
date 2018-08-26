using DataConverter.Models;
using DataConverter.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Builders
{
    public class ProductsBuilder
    {
        private readonly ProductsLoader prodLoader;
        private readonly string prodInput;
        private readonly string prodOutput;
        private readonly string groupInput;

        public ProductsBuilder()
        {
            prodLoader = new ProductsLoader(@"C:\temp\AndroidPixika\Load\Tariffs_Android.csv");
            prodInput = @"C:\temp\AndroidPixika\Load\Produits_2057.csv";
            groupInput = @"C:\temp\AndroidPixika\Load\ProductGroups.csv";
            prodOutput = @"C:\temp\AndroidPixika\Result\Groups_and_Products.json";
        }

        public void BuildProducts()
        {
            try
            {

                List<ProductsDTO> products = prodLoader.LoadData(prodInput);

                GroupsLoader gl = new GroupsLoader();
                List<GroupsDTO> groups = gl.LoadData(groupInput);

                JObject export =
                new JObject(
                    new JProperty("families",
                         new JArray(
                                     new JObject
                                            {
                                                { "id", 0 },
                                                {"name", new JArray( new JObject
                                                                        {
                                                                          { "lang", "en-US" },
                                                                          { "text",  "Catalog"}
                                                                        }
                                                                    )
                                                },
                                                {"has_sub_families", true }
                                            },
                                     groups.
                                     Select
                                     (group => 
                                        new JObject
                                            {
                                                { "id", group.CategoryName },
                                                {"name", new JArray( new JObject
                                                                        {
                                                                          { "lang", "en" },
                                                                          { "text",  group.CategoryName}
                                                                        }
                                                                    )
                                                },
                                                {"prent", "0" },
                                                {"has_sub_families", false }
                                            }
                                        )
                                   )
                                 ),
                             new JProperty("products",
                                     new JArray
                                     (
                                        products.
                                        Select(product => 
                                            new JObject
                                            {
                                                {
                                                    "id", product.Reference },
                                                {
                                                    "name", new JArray( new JObject
                                                                            {
                                                                              { "lang", "en" },
                                                                              { "text",  product.Name}
                                                                            })
                                                },
                                                {
                                                    "abstract_product", false
                                                },
                                                {
                                                    "short_desc", new JArray( new JObject
                                                                                {
                                                                                   { "lang", "en" },
                                                                                   { "text",  string.IsNullOrEmpty(product.ShortDescr) ? "" : $"<strong>{product.ShortDescr}</strong>"}
                                                                                })
                                                },
                                                {
                                                    "long_desc", new JArray( new JObject
                                                                                {
                                                                                  { "lang", "en" },
                                                                                  { "text",  $"{product.LongDescr}"}
                                                                                })
                                                },
                                                {
                                                    "family_ids", new JArray(product.
                                                                                Groups.
                                                                                Split(','))
                                                },
                                                {
                                                    "brand", "PIXIKA"
                                                },
                                                {
                                                    "skus", new JArray
                                                            ( 
                                                                product.
                                                                Variants.
                                                                Select(variant => 
                                                                        new JObject
                                                                        {
                                                                            { "id", variant.VariantReference },
                                                                            { "desc",  new JArray()},
                                                                            {
                                                                                "name", new JArray( 
                                                                                        new JObject{
                                                                                            { "lang", "en" },
                                                                                            { "text",  variant.Name}
                                                                                        })
                                                                            },
                                                                            {
                                                                                "specs", new JArray
                                                                                    (
                                                                                        new JObject
                                                                                        {
                                                                                        { "id", "color" },
                                                                                        {
                                                                                            "spec", new JArray(
                                                                                                        new JObject
                                                                                                        {
                                                                                                            { "lang", "en" },
                                                                                                            { "text",  "Color"}
                                                                                                        })
                                                                                        },
                                                                                        {
                                                                                            "value", new JArray(
                                                                                                        new JObject
                                                                                                        {
                                                                                                            { "lang", "en" },
                                                                                                            { "text",  variant.Color}
                                                                                                        })
                                                                                        },
                                                                                        { "unit",  new JArray()}
                                                                                    },
                                                                                        new JObject{
                                                                                        { "id", "capacity" },
                                                                                        {
                                                                                            "spec", new JArray(
                                                                                                        new JObject
                                                                                                        {
                                                                                                            { "lang", "en" },
                                                                                                            { "text",  "Capacity"}
                                                                                                        })
                                                                                        },
                                                                                        {
                                                                                            "value", new JArray(
                                                                                                        new JObject
                                                                                                        {
                                                                                                            { "lang", "en" },
                                                                                                            { "text",  variant.Capacity}
                                                                                                        })
                                                                                        },
                                                                                        { "unit",  new JArray()}
                                                                                    })
                                                                            },
                                                                            {
                                                                                "medias", new JArray
                                                                                    (
                                                                                        new JObject
                                                                                        {
                                                                                        { "type", "PICTURE" },
                                                                                        { "source", variant.ImagePath}
                                                                                    })
                                                                            },
                                                                            { "related_sku_ids",  new JArray()},
                                                                            {
                                                                                "availabilities", new JArray
                                                                                                (
                                                                                                    new JObject
                                                                                                    {
                                                                                                        { "shop_id", variant.Tariff.Shop },
                                                                                                        { "price",  variant.Tariff.Price },
                                                                                                        { "special_prices",  new JArray()},
                                                                                                        { "restocking",  new JArray()},
                                                                                                })
                                                                            },
                                                                            { "unit",  new JArray()}
                                                                        }))
                                                        },

                                                   }
                                                
                                   )
                                 )
                              )
                           );

                using (StreamWriter file = File.CreateText(prodOutput))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    export.WriteTo(writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
