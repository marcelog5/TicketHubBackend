using Application.UseCases.EventUseCases.CreateEvent;
using Application.UseCases.TicketUseCases;
using Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ICreateEventUseCase _createEventUseCase;
        private readonly ISubscribeCustomerToEventUseCase _subscribeCustomerToEventUseCase;

        public EventController
        (
            ICreateEventUseCase createEventUseCase,
            ISubscribeCustomerToEventUseCase subscribeCustomerToEventUseCase
        )
        {
            _createEventUseCase = createEventUseCase;
            _subscribeCustomerToEventUseCase = subscribeCustomerToEventUseCase;
        }

        // POST api/<EventController>
        [HttpPost]
        public async Task<Result<CreateEventOutput>> Post([FromBody] CreateEventInput input)
        {
            return await _createEventUseCase.Execute(input);
        }

        // POST api/<EventController/subscribe>
        [HttpPost("subscribe")]
        public async Task<Result<SubscribeCustomerToEventOutput>> Subscribe([FromBody] SubscribeCustomerToEventInput input)
        {
            return await _subscribeCustomerToEventUseCase.Execute(input);
        }
    }
}
