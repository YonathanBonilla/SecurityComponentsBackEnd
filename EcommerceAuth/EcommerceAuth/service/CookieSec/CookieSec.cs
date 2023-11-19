using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;


namespace EcommerceAuth.service.CookieSec
{
    public class CookieSec : ICookieSec
    {
        private readonly IDataProtector _dataProtector;
        private readonly HttpContext _http;

        public CookieSec(IDataProtectionProvider dataProtectionProvider,
                         IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(nameof(CookieSec));
            _http = httpContextAccessor.HttpContext;
        }

        public void CreateCookie<T>(string name, T data)
        {
            try
            {
                var rawJson = JsonSerializer.Serialize(data);
                var secureJson = _dataProtector.Protect(rawJson);

                var localPath =  _http.Request.Host.Value;//
                _http.Response.Cookies.Append(name, localPath, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Domain = localPath
                }); ;
            }
            catch (Exception e)
            {
                throw;
            }
        }               

        public T GetCookieValue<T>(string name)
        {
            if (_http.Request.Cookies.ContainsKey(name))
            {
                var secureJson = _http.Request.Cookies[name];
                var rawJson = _dataProtector.Unprotect(secureJson);

                return JsonSerializer.Deserialize<T>(rawJson);
            }

            return default;
        }
    }
}
