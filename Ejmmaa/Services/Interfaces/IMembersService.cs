
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface IMembersService
    {
         
        public List<ClanMembersViewModel> GetClanMembersData(MemberDto memberDto); 
         public ClanMembersViewModel GetClanMember(MemberDto memberDto); 

        public List<VotersViewModel> GetAllVoters(VotersDto votersDto); 

         public VotersViewModel GetVoter(VotersDto votersDto); 

        public bool AddMember(MemberDto  memberDto); 

        public bool UpdateMember(MemberDto memberDto);

        public bool DeleteMember(MemberDto memberDto);
    }
}