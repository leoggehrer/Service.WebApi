using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.WebApi.Models
{
    [Serializable]
    public class Product : ModelObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Stars { get; set; }
    }
}
