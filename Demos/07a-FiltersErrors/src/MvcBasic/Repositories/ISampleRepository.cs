using System.Threading.Tasks;

namespace MvcBasic.Repositories
{
    public interface ISampleRepository
    {
        Task<string> GetUserDetailsAsync(string userName);
    }
}
