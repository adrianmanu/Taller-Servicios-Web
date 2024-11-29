using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            addCategryAndProdcut();
        }

        static void addCategryAndProdcut() {
            var categories = new Categories();
            categories.CategoryName = "Bebidas";
            categories.Description = "Bebidas frias";

            var products = new Products();
            products.ProductName = "Coca Cola";
            products.UnitPrice = 10;
            products.UnitsInStock = 100;

            categories.Products.Add(products);

            using (var repository = RepositoryFactory.CreateRepository())
            {
                repository.Create(categories);
            }

            Console.WriteLine($"CategoryID: { categories.CategoryID}, ProductID: { products.ProductID}");
            Console.WriteLine();


        }
    }
}
