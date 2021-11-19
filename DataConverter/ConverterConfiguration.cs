using System;
using System.Collections.Generic;
using System.Text;

namespace DataConverter
{
    /// <summary>
    /// Configuration for the MedNotes application.
    /// </summary>
    public class ConverterConfiguration
    {
        /// <summary>
        /// Gets or sets products input json file.
        /// </summary>
        public string ProductsInputFile { get; set; }

        /// <summary>
        /// Gets or sets tariffs input json file.
        /// </summary>
        public string TariffsInputFile { get; set; }

        /// <summary>
        /// Gets or sets groups input json file.
        /// </summary>
        public string GroupsInputFile { get; set; }

        /// <summary>
        /// Gets or sets customers input json file.
        /// </summary>
        public string CustomersInputFile { get; set; }

        /// <summary>
        /// Gets or sets DT db output json file.
        /// </summary>
        public string DtDbOutputFile { get; set; }
    }
}
