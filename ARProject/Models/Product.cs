using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.Models
{
    public class Product
    {
        public string Id { get; set; }         
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public string? Model3DUrl { get; set; }
        public List<string> Sizes { get; set; }
        public string CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public Product()
        {
            Images = new List<string>();
            Sizes = new List<string>();
        }
    }
}
