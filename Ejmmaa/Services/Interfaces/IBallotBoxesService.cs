
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IBallotBoxesService
    {

      public List<BallotBoxesViewModel> GetAllBallotBoxes(); 

      public BallotBoxesViewModel GetBallotBoxById(BallotBoxesDto ballotBoxesDto); 

      public bool AddBallotBox(BallotBoxesDto ballotBoxesDto); 

      public bool UpdateBallotBox(BallotBoxesDto ballotBoxesDto); 

      public bool DeleteBallotBox(BallotBoxesDto ballotBoxesDto); 


    }
}