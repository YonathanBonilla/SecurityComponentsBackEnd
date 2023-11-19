using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAuth.service.CookieSec
{
    public interface ICookieSec
    {
        void CreateCookie<T>(string name, T data);
        T GetCookieValue<T>(string name);
    }
}
