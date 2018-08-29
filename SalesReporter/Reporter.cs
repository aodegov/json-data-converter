using Newtonsoft.Json;
using OfficeOpenXml;
using SalesReporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesReporter
{
    class Reporter
    {
        private string FormatCsvItem(string content)
        {
            return "\"" + content.Replace("\"", "\"\"") + "\"";
        }

        public bool CreateReports(string inputDir, string outputDir)
        {
            var files = Directory.GetFiles(inputDir, "*.json");

            if (files.Length == 0)
            {
                MessageBox.Show("Input folder does not contains .json sales reports", "No report files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var salesModels = new Dictionary<string, SalesModel>();

            foreach (var file in files)
            {
                var outputFile = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(file) + ".csv");

                using (StreamReader r = new StreamReader(file))
                {
                    var json = r.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<SalesModel>(json);
                    salesModels.Add(outputFile, result);
                }
            }

            try
            {
                WriteCsvFiles(salesModels);

                WriteExcelFiles(outputDir);

                MessageBox.Show("Successfully created", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         

            return true;
        }


        private void WriteCsvFiles(Dictionary<string, SalesModel> salesModels)
        {
            foreach (var model in salesModels)
            {
                using (StreamWriter file = new StreamWriter(model.Key))
                {
                    file.WriteLine(
                                    "organization_name, " +             // 0
                                    "product_id, " +                    // 1
                                    "quantity, " +                      // 2
                                    "sku_id, " +                        // 3
                                    "unit_price, " +                    // 4
                                    "currency, " +                      // 5
                                    "shop_id, " +                       // 6
                                    "contact_first_name, " +            // 7
                                    "contact_last_name, " +             // 8
                                    "contact_email, " +                 // 9
                                    "comment"                           // 10
                                   );

                    foreach (var purcashe in model.Value.sales)
                    {
                        foreach (var item in purcashe.basket.items)
                        {
                            file.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                                            FormatCsvItem(purcashe.customer.professional_details.organization_name),    // 0
                                            FormatCsvItem(item.product_id),                                             // 1
                                            FormatCsvItem(item.qty.ToString()),                                         // 2
                                            FormatCsvItem(item.sku_id),                                                 // 3
                                            FormatCsvItem(item.unit_price.ToString()),                                  // 4
                                            FormatCsvItem("EUR"),                                                       // 5
                                            FormatCsvItem(purcashe.basket.shop_id),                                     // 6
                                            FormatCsvItem(purcashe.customer.details.first_name),                        // 7
                                            FormatCsvItem(purcashe.customer.details.last_name),                         // 8
                                            FormatCsvItem(purcashe.customer.details.email.personal),                    // 9
                                            FormatCsvItem(string.Empty)                                                 // 10
                                            );
                        }

                        file.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                                           FormatCsvItem("Total:"),                                                     // 0
                                           FormatCsvItem(string.Empty),                                                 // 1
                                           FormatCsvItem(purcashe.basket.items_quantity.ToString()),                    // 2
                                           FormatCsvItem(string.Empty),                                                 // 3
                                           FormatCsvItem(purcashe.basket.items_total_amount.ToString()),                // 4
                                           FormatCsvItem(string.Empty),                                                 // 5
                                           FormatCsvItem(string.Empty),                                                 // 6
                                           FormatCsvItem(string.Empty),                                                 // 7
                                           FormatCsvItem(string.Empty),                                                 // 8
                                           FormatCsvItem(string.Empty),                                                 // 9
                                           FormatCsvItem(purcashe.comment)                                              // 10
                                           );
                    }
                }
            }
        }

        private void WriteExcelFiles(string outputDir)
        {

            var files = Directory.GetFiles(outputDir, "*.csv");

            foreach (var csvFile in files)
            {

                string excelFile = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(csvFile) + ".xls");

                if (File.Exists(excelFile))
                {
                    File.Delete(excelFile);
                }

                string worksheetsName = "TEST";
                bool firstRowIsHeader = false;

                var excelTextFormat = new ExcelTextFormat();
                excelTextFormat.Delimiter = ',';
                excelTextFormat.TextQualifier = '"';

                var excelFileInfo = new FileInfo(excelFile);
                var csvFileInfo = new FileInfo(csvFile);

                using (ExcelPackage package = new ExcelPackage(excelFileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet.Cells["A1"].LoadFromText(csvFileInfo, excelTextFormat, OfficeOpenXml.Table.TableStyles.Medium25, firstRowIsHeader);
                    package.Save();
                }
            }
        }

    }
}
