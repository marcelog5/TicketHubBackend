using Domain.Partners;
using Domain.Shared;

namespace Application.UseCases.PartnerUseCases.GetPartner
{
    public sealed record GetPartnerByIdOutput(
        Name Name,
        Email Email,
        Cnpj Cnpj);
}