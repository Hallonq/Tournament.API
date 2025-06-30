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

    private DateTime endDate;
    [DataMember]
    public DateTime EndDate
    {
        get
        {
            return endDate;
        }

        set
        {
            value = StartDate.AddMonths(3);
        }
    }
}
