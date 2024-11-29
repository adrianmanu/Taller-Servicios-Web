using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IProductService
    {
        Products Create(Products products);
        Products RetrieveById(int id);
        bool Update(Products products);
        bool Delete(int id);
    }
}
