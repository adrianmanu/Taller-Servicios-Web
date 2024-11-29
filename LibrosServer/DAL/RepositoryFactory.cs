using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryFactory 
    {
        public static IRepository CreateRepository()
        {
            //retornar el repositorio de Entity Framework (EF)
            return new EFRespository(new Entities.Sales_DBEntities());
        }
    }
}
