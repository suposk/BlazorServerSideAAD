using Server.Entities;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IVersionRepository
    {
        Task<AppVersion> GetVersion(string version);
    }
}