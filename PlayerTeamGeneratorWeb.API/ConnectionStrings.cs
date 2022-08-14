namespace PlayerTeamGeneratorWeb.API
{
    public sealed class ConnectionStrings
    {
        public string Value { get; set; }

        public ConnectionStrings(string value)
        {
            Value = value;
        }
    }
}
