using Domain.Shared;

namespace Domain.Partners
{
    public interface IPartnerRepository
    {
        Task<Partner> GetPartnerById(Guid Id, CancellationToken cancellationToken = default);
        Task<bool> AlreadyExist(Cnpj cnpj, Email email, CancellationToken cancellationToken = default);
        Task Add(Partner partner, CancellationToken cancellationToken = default);
    }
}
