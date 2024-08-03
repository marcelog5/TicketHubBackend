using Application.UseCases.PartnerUseCases.CreatePartner;
using Application.UseCases.PartnerUseCases.GetPartner;
using Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly ICreatePartnerUseCase _createPartnerUseCase;
        private readonly IGetPartnerByIdUseCase _getPartnerByIdUseCase;

        public PartnerController
        (
            ICreatePartnerUseCase createPartnerUseCase,
            IGetPartnerByIdUseCase getPartnerByIdUseCase
        )
        {
            _createPartnerUseCase = createPartnerUseCase;
            _getPartnerByIdUseCase = getPartnerByIdUseCase;
        }

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public async Task<Result<GetPartnerByIdOutput>> Get(Guid id)
        {
            var input = new GetPartnerByIdInput(id);
            return await _getPartnerByIdUseCase.Execute(input);
        }

        // POST api/<PartnerController>
        [HttpPost]
        public Task<Result<CreatePartnerOutput>> Post([FromBody] CreatePartnerInput input)
        {
            return _createPartnerUseCase.Execute(input);
        }
    }
}
