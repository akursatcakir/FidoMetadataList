using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using mdsAlliance.Models;

namespace mdsAlliance.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage mdsResponse = await client.GetAsync("https://mds.fidoalliance.org/");
                string response = await mdsResponse.Content.ReadAsStringAsync();
                if (mdsResponse.IsSuccessStatusCode) {
                    string[] JWT = response.Split('.');
                    byte[] data = Convert.FromBase64String(JWT[1]);
                    string json = Encoding.UTF8.GetString(data);
                    JObject main = JObject.Parse(json);
                    JArray Entries = (JArray)main["entries"];
                    List<MetaData> metadatas = new List<MetaData>();
                    foreach(JObject Entry in Entries)
                    {
                        HttpResponseMessage subResponse = await client.GetAsync((string)Entry["url"]);
                        string md = await subResponse.Content.ReadAsStringAsync();
                        if (subResponse.IsSuccessStatusCode)
                        {
                            byte[] sdata = Convert.FromBase64String(md);
                            string metajson = Encoding.UTF8.GetString(sdata);
                            JObject meta = JObject.Parse(metajson);
                            metadatas.Add(new MetaData(meta, (JArray)Entry["statusReports"]));
                        }
                    }
                    return View(metadatas);
                }
                
                 
                return null;
            }
        }
    }
}
