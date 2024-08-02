using Domain.Abstracts;

namespace Domain.Customers
{
    public static class CustomerErrors
    {
        public static Error NotFound = new(
        "Customer.NotFound",
        "The Customer with the specified identifier was not found.");

        public static Error AlreadyExist = new(
            "Customer.AlreadyExists",
            "The Customer already exists.");
    }
}
