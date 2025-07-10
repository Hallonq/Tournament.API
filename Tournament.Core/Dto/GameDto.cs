using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tournament.Core.Dto;
[DataContract]
public class GameDto
{
    [DataMember]
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string? Title { get; set; }
    [DataMember]
    public DateTime Time { get; set; }
}
