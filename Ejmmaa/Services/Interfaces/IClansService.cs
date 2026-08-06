
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IClansService
    {

          public List<ClanViewModel> GetClanData(UserDto user); 
          public bool AddClan(ClanDto  clanDto); 

    }
}