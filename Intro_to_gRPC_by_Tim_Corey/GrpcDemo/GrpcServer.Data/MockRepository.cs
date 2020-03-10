using System.Collections.Generic;
using System.Linq;
using Common;

namespace GrpcServer.Data
{
    public class MockRepository : ICustomersRepository
    {
        private IEnumerable<Customer> _customers = new[]
        {
            new Customer
            {
                Id = 1,
                FirstName = "Jamie",
                LastName = "Smith",
                Email = "jsmith@gmail.com",
                IsAlive = true,
                Age = 25
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jdoe@gmail.com",
                IsAlive = true,
                Age = 42,
            },
            new Customer
            {
                Id = 3,
                FirstName = "Greg",
                LastName = "Thomas",
                Email = "gthomas@gmail.com",
                IsAlive = true,
                Age = 27,
            },
            new Customer
            {
                Id = 4,
                FirstName = "Tim",
                LastName = "Corey",
                Email = "tim@iamtimcorey.com",
                IsAlive = true,
                Age = 41,
            },
            new Customer
            {
                Id = 5,
                FirstName = "Sue",
                LastName = "Storm",
                Email = "sue@stormy.net",
                IsAlive = true,
                Age = 28,
            },
            new Customer
            {
                Id = 6,
                FirstName = "Bilbo",
                LastName = "Baggins",
                Email = "bilbo@middleearth.net",
                IsAlive = false,
                Age = 117,
            }
        };

        public Customer GetCustomer(int id)
        {
            return _customers.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers;
        }
    }
}
