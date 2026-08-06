

namespace Ejmmaa.Models.Entities
{
public class Votes 
{ public int VoteID { get; set; } 
public int? OptionID { get; set; } 
public int? CandidateID { get; set; } 
public int? TypeID { get; set; }
 public int BoxID { get; set; } 
 public DateTime VotedAt { get; set; } }

}