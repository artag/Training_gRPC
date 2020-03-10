using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using GrpcServer.Data;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _repository;

        public CustomersService(IMapper mapper, ICustomersRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override Task<CustomerModel> GetCustomerInfo(
            CustomerLookupModel request, ServerCallContext context)
        {
            var customer = _repository.GetCustomer(request.UserId);
            if (customer == null)
                return Task.FromResult(new CustomerModel());

            return Task.FromResult(_mapper.Map<CustomerModel>(customer));
        }

        public override async Task GetAllCustomers(
            AllCustomersRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = _repository.GetAllCustomers();

            foreach (var customer in customers)
            {
                await Task.Delay(1000);     // Для эмуляции задержки
                await responseStream.WriteAsync(_mapper.Map<CustomerModel>(customer));
            }
        }
    }
}
