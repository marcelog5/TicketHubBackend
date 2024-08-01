using Domain.Abstracts;

namespace Domain.Partners
{
    public static class PartnerErrors
    {
        public static Error NotFound = new(
            "Partner.NotFound",
            "The Partner with the specified identifier was not found.");

        public static Error AlreadyExist = new(
            "Partner.AlreadyExist",
            "The Partner with the specified CNPJ or Email already exists.");
    }
}
