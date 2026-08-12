
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IElectionsService
    {

          public List<ElectionsViewModel> GetAllElections(ElectionDto electionDto); 

          public ElectionsViewModel GetElectionById(ElectionDto electionDto);

          public int GetMaxSelection(ElectionDto electionDto); 
          
          public bool AddElection(ElectionDto electionDt); 

          public bool UpdateElection(ElectionDto electionDto);

          public bool DeleteElection(ElectionDto electionDto);

    }
}