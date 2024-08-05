using System.Text.RegularExpressions;

namespace Domain.Customers
{
    public record Cpf
    {
        public string Value { get; init; }

        public Cpf(string value)
        {
            // Remove máscara do CPF
            string cleanValue = RemoveMask(value);

            if (!IsValidCpf(cleanValue))
                throw new ArgumentException("Invalid CPF number.", nameof(value));

            Value = cleanValue;
        }

        private static string RemoveMask(string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d+$"))
                return false;

            int[] digits = Array.ConvertAll(cpf.ToCharArray(), c => (int)char.GetNumericValue(c));

            int sum1 = 0, sum2 = 0, checkDigit1, checkDigit2;

            for (int i = 0; i < 9; i++)
                sum1 += digits[i] * (10 - i);

            checkDigit1 = (sum1 * 10) % 11;
            if (checkDigit1 == 10) checkDigit1 = 0;

            if (digits[9] != checkDigit1)
                return false;

            for (int i = 0; i < 10; i++)
                sum2 += digits[i] * (11 - i);

            checkDigit2 = (sum2 * 10) % 11;
            if (checkDigit2 == 10) checkDigit2 = 0;

            return digits[10] == checkDigit2;
        }
    }
}
