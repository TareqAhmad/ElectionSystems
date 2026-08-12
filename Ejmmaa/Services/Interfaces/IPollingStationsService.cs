
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IPollingStationsService
    {

      public List<PollingStationsViewModel> GetAllPollingStations(); 
      public PollingStationsViewModel GetPollingStationById(PollingStationsDto pollingStationDto); 
      public bool AddPollingStation(PollingStationsDto pollingStationsDto); 

      public bool UpdatePollingStation(PollingStationsDto pollingStationsDto);

      public bool DeletePollingStation(PollingStationsDto pollingStationsDto);


    }
}