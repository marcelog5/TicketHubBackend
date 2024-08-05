using System.Text.RegularExpressions;

namespace Domain.Shared
{
    public record Email
    {
        public string Value { get; init; }

        public Email(string value)
        {
            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email address.", nameof(value));

            Value = value.ToLower();
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Simple regex for basic email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email.ToLower(), pattern, RegexOptions.IgnoreCase);
        }
    }
}
