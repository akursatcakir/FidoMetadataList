using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mdsAlliance.Models
{
    public class MetaData
    {
        public string aaid  { get; set; }
        public string assertionScheme { get; set; }
        public string description { get; set; }
        public int attachmentHint { get; set; }
        public int authenticationAlgorithm { get; set; }
        public bool isSecondFactorOnly { get; set; }
        public int keyProtection { get; set; }
        public int matcherProtection { get; set; }
        public int publicKeyAlgAndEncoding { get; set; }
        public int tcDisplay { get; set; }
        public List<Status> status { get; set; }

        public MetaData(JObject data, JArray status)
        {
            foreach (var prop in this.GetType().GetProperties())
                if (prop.Name != "status")
                    prop.SetValue(this, Convert.ChangeType(data[prop.Name], prop.PropertyType));
            this.status = new List<Status>();
            foreach (JObject state in status)
                this.status.Add(new Status(state));
        }
    }
}
