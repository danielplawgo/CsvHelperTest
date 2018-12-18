using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace CsvHelperTest
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            AutoMap();
            Map(m => m.Category.Id).Name("CategoryId");
            Map(m => m.Category.Name).Name("CategoryName");
            Map(m => m.IsActive).Ignore();
        }
    }
}
