namespace Ejmmaa.Models.DTOs
{
public class VotingRegistryDto { 
    public int RegistryId { get; set; }
     public int MemberId { get; set; }
     public int ElectionId {get; set;}
     public int BoxId { get; set; }
     public DateTime VotedAt { get; set; } 
     public bool IsShow {get; set;}
       
    }

}