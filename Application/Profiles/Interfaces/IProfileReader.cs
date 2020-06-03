using System.Threading.Tasks;
using Application.Profiles.DTO;

namespace Application.Profiles.Interfaces
{
    public interface IProfileReader
    {
        Task<ProfileDto> ReadProfile(string username);
    }
}