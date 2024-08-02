using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Partners
{
    public sealed class Partner : Entity
    {
        public Partner(
            Guid id,
            Name name,
            Email email,
            Cnpj cnpj) : base(id)
        {
            Name = name;
            Email = email;
            Cnpj = cnpj;
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cnpj Cnpj { get; private set; }
    }
}
