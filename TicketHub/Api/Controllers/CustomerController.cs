using Application.UseCases.CustomerUseCases.CreateCustomer;
using Application.UseCases.CustomerUseCases.GetCustomer;
using Domain.Abstracts;
using Domain.Customers.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerUseCase _createCustomerUseCase;
        private readonly IGetCustomerByIdUseCase _getCustomerByIdUseCase;

        public CustomerController
        (
            ICreateCustomerUseCase createCustomerUseCase,
            IGetCustomerByIdUseCase getCustomerByIdUseCase
        )
        {
            _createCustomerUseCase = createCustomerUseCase;
            _getCustomerByIdUseCase = getCustomerByIdUseCase;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<Result<GetCustomerByIdOutput>> Get(Guid id)
        {
            var input = new GetCustomerByIdInput(id);
            return await _getCustomerByIdUseCase.Execute(input);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<Result<CreateCustomerOutput>> Post([FromBody] CreateCustomerInput input)
        {
            return await _createCustomerUseCase.Execute(input);
        }
    }
}
