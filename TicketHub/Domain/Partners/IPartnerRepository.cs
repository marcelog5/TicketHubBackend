using Domain.Shared;

namespace Domain.Partners
{
    public interface IPartnerRepository
    {
        Task<Partner> GetById(Guid Id, CancellationToken cancellationToken = default);
        Task<bool> AlreadyExist(Email email, Cnpj cnpj, CancellationToken cancellationToken = default);
        Task Add(Partner partner, CancellationToken cancellationToken = default);
    }
}
