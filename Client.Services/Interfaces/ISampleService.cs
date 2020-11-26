using System.Threading.Tasks;

namespace Client.Services
{
    public interface ISampleService
    {
        Task<string> RandomText(string text = null);
    }
}