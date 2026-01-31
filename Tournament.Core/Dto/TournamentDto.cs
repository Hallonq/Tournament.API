using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tournament.Core.Dto;
[DataContract]
public class TournamentDto
{
    [DataMember]
    [Required]
    [Length(3, 20)]
    public string? Title { get; set; }
    [DataMember]
    public DateTime StartDate { get; set; }
    [DataMember]
    public DateTime EndDate => StartDate.AddMonths(3);
    [DataMember]
    public ICollection<GameDto> Games { get; set; } = [];
}
