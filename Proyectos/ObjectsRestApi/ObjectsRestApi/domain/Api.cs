using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsRestApi.domain
{
    internal class Api
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Data { get; set; }

        public Api(int id, string name, Dictionary<string, string> data)
        {
            Id = id;
            Name = name;
            Data = data;
        }

    }
}
