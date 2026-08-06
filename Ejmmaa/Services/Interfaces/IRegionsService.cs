
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IRegionsService
    {

      public List<RegionsViewModel> GetAllRegions(); 
      public RegionsViewModel GetRegionById(int RegionId); 

      public bool AddRegion(RegionDto regionDto); 

      public bool UpdateRegion(RegionDto regionDto);

       public bool DeleteRegion(RegionDto regionDto);


    }
}