using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mdsAlliance.Models
{
    public class Status
    {

        public string status { get; set; }
        public string effectiveDate { get; set; }


        public Status(JObject data)
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                prop.SetValue(this, Convert.ChangeType(data[prop.Name], prop.PropertyType));
            }
        }
    }
}
