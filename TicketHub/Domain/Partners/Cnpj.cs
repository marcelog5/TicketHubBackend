using System.Text.RegularExpressions;

namespace Domain.Partners
{
    public record Cnpj
    {
        public string Value { get; init; }

        public Cnpj(string value)
        {
            // Remove máscara do CNPJ
            string cleanValue = RemoveMask(value);

            if (!IsValidCnpj(cleanValue))
                throw new ArgumentException("Invalid CNPJ number.", nameof(value));

            Value = cleanValue;
        }

        private static string RemoveMask(string cnpj)
        {
            return Regex.Replace(cnpj, @"[^\d]", "");
        }

        private static bool IsValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d+$"))
                return false;

            int[] digits = Array.ConvertAll(cnpj.ToCharArray(), c => (int)char.GetNumericValue(c));

            int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum1 = 0, sum2 = 0, checkDigit1, checkDigit2;

            for (int i = 0; i < 12; i++)
                sum1 += digits[i] * weights1[i];

            checkDigit1 = (sum1 % 11 < 2) ? 0 : 11 - (sum1 % 11);

            if (digits[12] != checkDigit1)
                return false;

            for (int i = 0; i < 13; i++)
                sum2 += digits[i] * weights2[i];

            checkDigit2 = (sum2 % 11 < 2) ? 0 : 11 - (sum2 % 11);

            return digits[13] == checkDigit2;
        }
    }
}
