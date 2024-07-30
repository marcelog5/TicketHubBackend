using Domain.Abstracts;

namespace Domain.Customers
{
    public sealed class Customer : Entity
    {
        public Customer(
            Guid Id,
            Name name, 
            Email email, 
            Cpf cpf) : base(Id)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
    }
}
