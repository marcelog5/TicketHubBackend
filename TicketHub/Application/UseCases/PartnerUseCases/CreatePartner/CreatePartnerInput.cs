using Domain.Partners;
using Domain.Shared;

namespace Application.UseCases.PartnerUseCases.CreatePartner
{
    public sealed record CreatePartnerInput(
        Cnpj Cnpj,
        Email Email,
        Name Name);
}