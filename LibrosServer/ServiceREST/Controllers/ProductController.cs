using BLL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceREST.Controllers
{
    public class ProductController : ApiController, IProductService
    {
        [HttpPost]
        public Products Create(Products products)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Create(products);
            return product;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Delete(id);
            return product;
        }

        [HttpGet]
        public Products RetrieveById(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.RetrieveById(id);
            return product;
        }

        [HttpPut]
        public bool Update(Products products)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Update(products);
            return product;
        }
    }
}
