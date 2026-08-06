

using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface ISupervisorsService
    {
        public UserViewModel Login(LoginRequest loginRequest);
        
        public List<ElectionSupervisorsViewModel> GetAllElectionSupervisors(); 

        public ElectionSupervisorsViewModel GetSupervisorById(int supervisorId);

        public bool AddSupervisor(ElectionSupervisorsDto electionSupervisorsDto); 

        public bool UpdateSupervisor(ElectionSupervisorsDto electionSupervisorsDto);

        public bool DeleteSupervisor(ElectionSupervisorsDto electionSupervisorsDto);
    }
}