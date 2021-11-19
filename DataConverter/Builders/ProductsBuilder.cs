using DataConverter.Models;
using DataConverter.DataLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Builders
{
    public class ProductsBuilder : IBuilder
    {
        private readonly ConverterConfiguration configuration;

        public ProductsBuilder(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JProperty BuildData()
        {
            TariffsLoader tariffsLoader = new TariffsLoader();
            List<TariffsDTO> tariffs = tariffsLoader.LoadData(this.configuration.TariffsInputFile);

            ProductsLoader productsLoader = new ProductsLoader(tariffs);
            List<ProductsDTO> productsData = productsLoader.LoadData(this.configuration.ProductsInputFile);

            var products = GetProducts(productsData);
            return products;
        }

        private JProperty GetProducts(List<ProductsDTO> products)
        {
            return new JProperty("products",
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
                                                    "brand", "DT"
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
                                                        {
                                                            "medias", new JArray
                                                                (
                                                                    new JObject
                                                                    {
                                                                        { "type", "image" },
                                                                        { "source", product.Variants.FirstOrDefault().ImagePath}
                                                                })
                                                        },
                                                        {
                                                            "selectors", GetSelectors(product)
                                                        },
                                                   }

                                   )
                                 )
                              );
        }

        private JArray GetSelectors(ProductsDTO product)
        {
            if (product.Variants.Any(_ => !string.IsNullOrEmpty(_.Color)) && product.Variants.Any(_ => !string.IsNullOrEmpty(_.Capacity)))
            {
                return new JArray
                                (
                                        new JObject { { "spec_id", "color" } },
                                        new JObject { { "spec_id", "capacity" } }
                                );
            }
            else if (product.Variants.Any(_ => !string.IsNullOrEmpty(_.Color)))
            {
                return new JArray
                                (
                                        new JObject { { "spec_id", "color" } }
                                );
            }
            else if (product.Variants.Any(_ => !string.IsNullOrEmpty(_.Capacity)))
            {
                return new JArray
                                (
                                        new JObject { { "spec_id", "capacity" } }
                                );
            }
            return new JArray();
        }
    }
}
