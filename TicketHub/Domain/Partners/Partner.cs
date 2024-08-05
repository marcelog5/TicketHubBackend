using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Partners
{
    public sealed class Partner : Entity
    {
        private Partner(
            Guid Id,
            Name name,
            Email email,
            Cnpj cnpj) : base(Id)
        {
            Name = name;
            Email = email;
            Cnpj = cnpj;
        }

        private Partner()
        {
        }

        public static Partner Create(Name name, Email email, Cnpj cnpj)
        {
            return new Partner(Guid.NewGuid(), name, email, cnpj);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cnpj Cnpj { get; private set; }
    }
}
