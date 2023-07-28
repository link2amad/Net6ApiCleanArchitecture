namespace Application.Configurations
{
    public class AppSettings
    {
        /*----JWT accessToken Settings----*/
        public string Secret { get; set; }
        public int? JwtTokenExpireInMinute { get; set; }
        public string APIKey { get; set; }
        public string VerificationToken { get; set; }
    }
}