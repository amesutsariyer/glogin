namespace GLogin2.Common
{
    public class GoogleTokenResponseModel
    {
        public string id_token { get; set; }
        public string expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}