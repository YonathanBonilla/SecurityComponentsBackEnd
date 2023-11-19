namespace EcommerceAuth.commons.domains
{
    public class ReqMs
    {
        public string grant_type { get; set; }
        public string client_secret { get; set; }
        public string scope { get; set; }
        public string clientId { get; set; }
    }
}