using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.PartnerUseCases.CreatePartner
{
    public interface ICreatePartnerUseCase : IUseCase<CreatePartnerInput, Result<CreatePartnerOutput>>
    {
    }
}
