namespace Domain.Shared
{
    public record Name
    {
        public string Value { get; init; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));

            Value = value;
        }
    }
}