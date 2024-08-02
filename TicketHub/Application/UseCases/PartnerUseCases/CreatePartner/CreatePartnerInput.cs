using Domain.Partners;
using Domain.Shared;

namespace Application.UseCases.PartnerUseCases.CreatePartner
{
    public sealed record CreatePartnerInput(
        Name Name,
        Email Email,
        Cnpj Cnpj);
}