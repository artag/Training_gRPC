using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var serverAddress = "https://localhost:5001";
            using var channel = GrpcChannel.ForAddress(serverAddress);

            await GreeterUsingExample(channel);
            await CustomersUsingExample(channel);

            Console.WriteLine("\nPress \"Enter\" to quit...");
            Console.ReadLine();
        }

        private static async Task GreeterUsingExample(ChannelBase channel)
        {
            var client = new Greeter.GreeterClient(channel);

            var input = new HelloRequest { Name = "Tema" };
            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
            Console.WriteLine();
        }

        private static async Task CustomersUsingExample(ChannelBase channel)
        {
            Console.WriteLine("Customers:");
            var id = 4;
            Console.WriteLine($"Get customer info for id {id}: ");

            var client = new Customer.CustomerClient(channel);
            var customer = await GetCustomerAsync(client, id);
            DisplayCustomer(customer);

            Console.WriteLine();

            Console.WriteLine("Get all customers:");
            await GetAndDisplayAllCustomers(client);
        }

        private static async Task<CustomerModel> GetCustomerAsync(Customer.CustomerClient client, int id)
        {
            var clientRequested = new CustomerLookupModel { UserId = id };
            var customer = await client.GetCustomerInfoAsync(clientRequested);

            return customer;
        }

        private static async Task GetAndDisplayAllCustomers(Customer.CustomerClient client)
        {
            using var call = client.GetAllCustomers(new AllCustomersRequest());
            while (await call.ResponseStream.MoveNext())
            {
                var customer = call.ResponseStream.Current;
                DisplayCustomer(customer);
            }
        }

        private static void DisplayCustomer(CustomerModel customer)
        {
            Console.WriteLine($"id:{customer.Id} {customer.FirstName} {customer.LastName}\n" +
                              $"    Age: {customer.Age}\n" +
                              $"    Email: {customer.EmailAddress}\n" +
                              $"    IsAlive: {customer.IsAlive}\n");
        }
    }
}
