namespace Intacct.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("timesheet", Schema = "timesheet")]
public class TimeSheet {
    [Column("id")]
    public int? Id {get; set;}
    [Column("userid")]
    [Required]
    public int? Userid {get; set;}
    [Column("start")]
    [Required]
    public DateTime? Start {get; set;}
    [Column("end")]
    [Required]
    public DateTime? End {get; set;}
    [Column("created_at")]
    public DateTime? CreatedAt {get; set;} = DateTime.UtcNow;

}