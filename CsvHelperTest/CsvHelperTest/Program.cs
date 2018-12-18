using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using CsvHelper;

namespace CsvHelperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = GetProducts();

            Export(products);

            Dump(products);

            var importerdProducts = Import();

            Dump(importerdProducts);
        }

        private static string _fileName = "products.csv";
        static void Export(IEnumerable<Product> products)
        {
            using (var writer = new StreamWriter(_fileName, false))
            {
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.RegisterClassMap<ProductMap>();
                    csvWriter.WriteRecords(products);
                }
            }
        }

        static IEnumerable<Product> Import()
        {
            using (var reader = new StreamReader(_fileName))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    csvReader.Configuration.RegisterClassMap<ProductMap>();
                    return csvReader.GetRecords<Product>().ToList();
                }
            }
        }

        static IEnumerable<Product> GetProducts()
        {
            int id = 1;

            var categories = new Faker<Category>()
                .RuleFor(p => p.Id, (f, p) => id++)
                .RuleFor(p => p.Name, (f, p) => f.Commerce.Categories(1)[0])
                .Generate(5);

            id = 1;
            return new Faker<Product>()
                .RuleFor(p => p.Id, (f, p) => id++)
                .RuleFor(p => p.Name, (f, p) => f.Commerce.ProductName())
                .RuleFor(p => p.Category, (f, p) => f.PickRandom(categories))
                .Generate(10);
        }

        static void Dump(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}: {product.Name}: {product.Category.Id}: {product.Category.Name}");
            }
        }
    }
}
