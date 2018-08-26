using DataConverter.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Tools
{
    public class ProductsLoader : DataLoader<ProductsDTO>
    {
        private readonly string tariffsInput;

        public ProductsLoader(string inputFile)
        {
            tariffsInput = inputFile; 
        }

        public override List<ProductsDTO> LoadData(string file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<ProductsDTO> lstResult = new List<ProductsDTO>();

            TariffsLoader tl = new TariffsLoader();
            List<TariffsDTO> lstTariffs = tl.LoadData(tariffsInput);

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateCsvReader(stream);
                var lstCheck = new List<string>();
                ProductsDTO product = null;

                // Skip first row if necesary
                reader.Read();

                while (reader.Read())
                {
                    var reference = reader.GetString(5);

                    if (!lstCheck.Contains(reference))
                    {
                        product = new ProductsDTO();
                        product.Reference = reference;
                        product.Name = reader.GetString(20);
                        product.ShortDescr = reader.GetString(21);
                        product.LongDescr = reader.GetString(2);
                        product.Groups = reader.GetString(4);
                        product.Variants = new List<VariantsDTO> {
                                                                    new VariantsDTO {
                                                                                        VariantReference = reader.GetString(0),
                                                                                        Name = reader.GetString(1),
                                                                                        Color = reader.GetString(9),
                                                                                        Capacity = reader.GetString(7),
                                                                                        ImagePath = reader.GetString(8),
                                                                                        Tariff = lstTariffs.Where(t => t.Reference == reader.GetString(0)).FirstOrDefault()
                                                                                    }
                                                                 };

                        lstResult.Add(product);

                        lstCheck.Add(reference);
                    }

                    else if (product != null)
                    {
                        product.Variants.Add(
                                                new VariantsDTO
                                                {
                                                    VariantReference = reader.GetString(0),
                                                    Name = reader.GetString(1),
                                                    Color = reader.GetString(9),
                                                    Capacity = reader.GetString(7),
                                                    ImagePath = reader.GetString(8),
                                                    Tariff = lstTariffs.Where(t => t.Reference == reader.GetString(0)).FirstOrDefault()
                                                }
                                             );
                        lstResult[lstResult.IndexOf(product)] = product;
                    }
                }
            }

            return lstResult;
        }
    }
}
