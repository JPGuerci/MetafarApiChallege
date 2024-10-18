namespace MetafarApiChallege.Infrastructure.Helpers
{
    public class AppSettings
    {
        public string? Secret { get; set; }
        public int TokenExpiresHours { get; set; }
        public int CountLoginFailures { get; set; }
    }
}
