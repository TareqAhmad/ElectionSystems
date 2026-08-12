
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IClansService
    {

          public List<ClanViewModel> GetAllClans(UserDto user); 

          public ClanViewModel GetClanById(ClanDto clanDto);
          public bool AddClan(ClanDto  clanDto); 

         public bool UpdateClan(ClanDto  clanDto); 
 
         public bool DeleteClan(ClanDto  clanDto);

    }
}