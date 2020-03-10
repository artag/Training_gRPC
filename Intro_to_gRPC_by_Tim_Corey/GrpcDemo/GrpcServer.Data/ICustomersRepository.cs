using System.Collections.Generic;
using Common;

namespace GrpcServer.Data
{
    public interface ICustomersRepository
    {
        Customer GetCustomer(int id);

        IEnumerable<Customer> GetAllCustomers();
    }
}
