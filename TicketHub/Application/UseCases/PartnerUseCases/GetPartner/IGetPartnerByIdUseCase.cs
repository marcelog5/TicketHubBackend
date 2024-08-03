using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.PartnerUseCases.GetPartner
{
    public interface IGetPartnerByIdUseCase : IUseCase<GetPartnerByIdInput, Result<GetPartnerByIdOutput>>
    {
    }
}
