using Client.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.NetCore.Services
{
    public interface IVersionService
    {
        Task<VersionDto> AddVersion(VersionDto add);
        Task<bool> DeleteVersion(int id);
        Task<List<VersionDto>> GetAllVersion();
        Task<VersionDto> GetVersion(string version = "0");
    }
}