using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
        public string ReleaseDate { get; set; }
        public decimal Weight { get; set; }
        public string ImagePath { get; set; }
    }
}
