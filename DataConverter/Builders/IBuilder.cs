using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataConverter.Builders
{
    public interface IBuilder
    {
        JProperty BuildData();
    }
}
