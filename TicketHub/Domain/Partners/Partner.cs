using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Partners
{
    public sealed class Partner : Entity
    {
        public Partner(
            Guid id, 
            Name name, 
            Cnpj cnpj, 
            Email email) : base(id)
        {
            Name = name;
            Cnpj = cnpj;
            Email = email;
        }

        public Name Name { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public Email Email { get; private set; }
    }
}
