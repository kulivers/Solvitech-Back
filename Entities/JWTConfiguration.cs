namespace Entities
{
    public class JwtConfiguration//todo rename
    {
        public string Section { get; set; } = "JwtSettings"; // todo rename
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }

    }
}