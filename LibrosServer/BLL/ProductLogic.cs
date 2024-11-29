using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductLogic
    {
        public Products Create(Products products)
        {
            Products res = null;

            using (var r = RepositoryFactory.CreateRepository())
            {
                Products result = r.Retrieve<Products>(p => p.ProductName == products.ProductName);
                if (result == null)
                {
                    res = r.Create(products);
                }
                else
                {
                    throw new Exception("Producto ya existe");
                }
                return res;
            }
        }

        public Products RetrieveById(int id)
        {
            Products res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Products>(p => p.ProductID == id);
            }
            return res;
        }

        public bool Update(Products products)
        {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                //Validar que el nombre del prodcuto no existas
                Products result = r.Retrieve<Products>
                    (p => p.ProductName == products.ProductName
                    && p.ProductID != products.ProductID);
                if (result == null)
                {
                    //No existe, se puede actualizar
                    res = r.Update(products);
                }
                else
                {
                    throw new Exception("Producto ya existe");
                }
                return res;
            }
        }

        public bool Delete(int id) {
            bool res = false;

            var products = RetrieveById(id);
            if (products != null)
            {
                if (products.UnitsInStock == 0)
                {
                    using (var r = RepositoryFactory.CreateRepository())
                    {
                        res = r.Delete(products);
                    }
                }
                else
                {
                    //Logica adicional para indicar que producto no existe
                    //para indicar que no se puede eliminar
                    throw new Exception("Producto con existencias");

                }
            }
            else
            {
                throw new Exception("Producto no existe");
            }
            return res;

        }

        public List<Products> Filter(string name) {
            List<Products> res = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                res = repository.Filter<Products>(
                    p => p.ProductName == name);
            }
            return res;
        }

    }
}
