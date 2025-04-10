using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FardaOnlineShop.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Display(Name ="شماره تماس")]
    public string Mobile { get; set; }//username
    public string Password { get; set; }

    [Display(Name ="شناسه نقش کاربری")]
    public Guid RoleId { get; set; }

    [Display(Name ="فعال")]
    public bool IsActive { get; set; } = true;


    [ForeignKey(nameof(RoleId))]
    virtual public Role? Role { get; set; }
}
