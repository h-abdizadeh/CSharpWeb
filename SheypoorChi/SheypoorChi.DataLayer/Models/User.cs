using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SheypoorChi.DataLayer.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }


    [Required]
    public Guid RoleId { get; set; }

    [Required]
    public string UserName { get; set; }//UserMobile
    [Required]
    public string UserPassword { get; set; }//UserMobile
    public bool IsActive { get; set; } = true;


    //User : Role -> 1 : n
    //User -> Role : 1
    [ForeignKey(nameof(RoleId))]
    virtual public Role? Role { get; set; }

    public virtual List<Factor>? Factors { get; set; }
}
