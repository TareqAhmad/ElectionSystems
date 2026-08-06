
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface ICandidatesService
    {

        public List<CandidatesViewModel> GetAllCandidates(CandidatesDto candidatesDto); 
        
        public CandidatesViewModel GetCandidateById(int candidateId);

        public bool AddCandidate(CandidatesDto candidatesDto); 

        public bool UpdateCandidate(CandidatesDto candidatesDto);

        public bool DeleteCandidate(CandidatesDto candidatesDto);
    }
}