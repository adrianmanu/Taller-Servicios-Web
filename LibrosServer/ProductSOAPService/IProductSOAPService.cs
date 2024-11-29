using Entities;
using System.ServiceModel;

namespace ProductSOAPService
{
    [ServiceContract]
    public interface IProductSOAPService
    {
        [OperationContract]
        Products Create(Products products);

        [OperationContract]
        Products RetrieveById(int productId);

        [OperationContract]
        bool Update(Products products);

        [OperationContract]
        bool Delete(int productId);
    }
}
