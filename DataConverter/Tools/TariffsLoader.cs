using DataConverter.Models;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace DataConverter.Tools
{
    public class TariffsLoader : DataLoader<TariffsDTO>
    {
        public override List<TariffsDTO> LoadData(string file)
        {
            List<TariffsDTO> lstResult = new List<TariffsDTO>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateCsvReader(stream);
                var lstCheck = new List<string>();
                TariffsDTO tariff = null;

                // Skip first row if necesary
                // reader.Read();

                while (reader.Read())
                {
                    tariff = new TariffsDTO();

                    var currReference = reader.GetString(0);

                    if (!lstCheck.Contains(currReference))
                    {
                        tariff.Reference = currReference;

                        tariff.Quantity = reader.GetString(1);

                        tariff.Price = reader.GetString(2);

                        lstResult.Add(tariff);

                        lstCheck.Add(currReference);
                    }
                }
            }

            return lstResult;
        }
    }
}