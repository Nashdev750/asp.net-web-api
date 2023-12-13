namespace Intacct.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users", Schema = "userSchema")]
public class User {
    [Column("id")]
    public int? Id {get; set;}
    [Required]
    [StringLength(50, MinimumLength=2)]
    [Column("fullname")]
    public string? FullName {get; set;}
     [Required]
    [StringLength(50, MinimumLength=2)]
    [Column("email")]
    public string? Email {get; set;}
    [Required]
    [Column("password")]
    public string? Password {get; set;}
    [Column("created_at")]
    public DateTime? CreatedAt {get; set;} = DateTime.UtcNow;

}