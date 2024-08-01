using Domain.Partners;
using Domain.Shared;

namespace Application.UseCases.PartnerUseCases.CreatePartner
{
    public sealed record CreatePartnerOutput(
        Guid Id,
        Cnpj Cnpj,
        Email Email,
        Name Name);
}