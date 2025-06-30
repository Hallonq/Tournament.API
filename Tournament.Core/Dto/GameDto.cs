using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tournament.Core.Dto;
[DataContract]
public class GameDto
{
    [DataMember]
    [Required]
    [Length(3, 20)]
    public string? Title { get; set; }
    [DataMember]
    public DateTime StartDate { get; set; }
}
