using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Customers
{
    public sealed class Customer : Entity
    {
        private Customer(
            Guid Id,
            Name name, 
            Email email, 
            Cpf cpf) : base(Id)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        private Customer() 
        { 
        }

        public static Customer Create(Name name, Email email, Cpf cpf)
        {
            return new Customer(Guid.NewGuid(), name, email, cpf);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
    }
}
