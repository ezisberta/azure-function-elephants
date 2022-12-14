using Microsoft.WindowsAzure.Storage.File.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElephantsAzure
{
    public class Models
    {
        public class Elephant
        {
            public Guid id { get; set; }
            public string species { get; set; }
            public string sex { get; set; }
            public int dob { get; set; }
            public int dod { get; set; }
            public string wikiLink { get; set; }
            public string note { get; set; }
        }

        public interface IElephantListReturner
        {
            public List<Elephant> ReturnElephants();
        }

        public interface ISingleElephantReturner
        {
            public Elephant GetElephant(string id);
        }

        public class JSonElephantListExtractor : IElephantListReturner
        {
            public List<Elephant> ReturnElephants()
            {
                string elephants = File.ReadAllText(@"..\..\..\elephants.json");
                return JsonSerializer.Deserialize<List<Elephant>>(elephants);
            }
        }

        public class ElephantProvider : ISingleElephantReturner
        {
            public List<Elephant> Elephants { get; set; }

            public ElephantProvider(IElephantListReturner returner)
            {
                Elephants = returner.ReturnElephants();
            }

            public Elephant GetElephant(string id)
            {
                return Elephants.Find(elephant => elephant.id == Guid.Parse(id));
            }

        }

    }
}
