using DataConverter.DataLoader;
using DataConverter.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Builders
{
    public class GroupsBuilder : IBuilder
    {
        private readonly ConverterConfiguration configuration;

        public GroupsBuilder(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JProperty BuildData()
        {
            GroupsLoader groupsLoader = new GroupsLoader();
            List<GroupsDTO> groupsData = groupsLoader.LoadData(this.configuration.GroupsInputFile);

            var groups = GetGroups(groupsData);
            return groups;
        }

        private JProperty GetGroups(List<GroupsDTO> groups)
        {
            return new JProperty("families",
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
                                                {"parent", "0" },
                                                {"has_sub_families", false }
                                               }
                                           )
                                      )
                                    );
        }
    }
}
