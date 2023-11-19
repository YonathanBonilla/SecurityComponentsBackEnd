using System.Threading.Tasks;

namespace EcommerceAuth.commons.utils
{
    public interface IKeyVaultUtils
    {
        Task<string> GetSecret(string secretName);
    }
}
