using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.WebApi.Models
{
    [Serializable]
    public class Post : ModelObject
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
