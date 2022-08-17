namespace ApiCliente
{
    public static class Configuration
    {
        public static string JwtKey = "NzQ3NzJERjYtN0FBQy00OTA3LUE2RUYtMzNCNjM2QkQyMUFD";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "api_key_uz2zR0HrbalLDEm53XyP0DKVvqwum5NHWcsJQWMn7jJg519x";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string Password { get; set; }
            public string Username { get; set; }
        }
    }

    
}
