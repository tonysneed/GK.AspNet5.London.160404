using System.Threading.Tasks;

namespace MvcBasic.Repositories
{
    public class SampleRespository : ISampleRepository
    {
        public async Task<string> GetUserDetailsAsync(string userName)
        {
            // Simulate async I/O
            return await Task.FromResult("City:New York");
        }
    }
}
