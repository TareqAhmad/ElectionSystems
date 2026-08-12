

using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IVotersService
    {
        public UserViewModel Login(LoginRequest loginRequest);

        public bool SubmitVote(VotingRegistryDto votingRegistryDto); 
        
    }
}