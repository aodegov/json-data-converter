using DataConverter.Models;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace DataConverter.Tools
{
    public class GroupsLoader : DataLoader<GroupsDTO>
    {
        public override List<GroupsDTO> LoadData(string file)
        {
            List<GroupsDTO> lstResult = new List<GroupsDTO>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateCsvReader(stream);
                var lstCheck = new List<string>();
                GroupsDTO group = null;

                // Skip first row if necesary
                // reader.Read();

                while (reader.Read())
                {
                    group = new GroupsDTO();

                    var currCategory = reader.GetString(0);

                    if (!lstCheck.Contains(currCategory))
                    {
                        group.CategoryName = currCategory;

                        lstResult.Add(group);

                        lstCheck.Add(currCategory);
                    }
                }
            }

            return lstResult;
        }
    }
}
