using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheypoorChi.DataLayer.Models;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Display(Name = "نام نقش", Prompt = "نام نقش")]
    [MaxLength(15, ErrorMessage = "نمیتواند بیش از 15 کاراکتر باشد")]
    [MinLength(4, ErrorMessage = "نمیتواند کمتر از 4 کاراکتر باشد")]
    public string RoleName { get; set; } = "user";//en

    [Display(Name = "توضیح نقش", Prompt = "توضیح نقش")]
    [MaxLength(15, ErrorMessage = "نمیتواند بیش از 15 کاراکتر باشد")]
    public string? RoleTitle { get; set; }//fa

    //ICollection === List, IEnumerable, ...
    //Role -> User : n
    virtual public ICollection<User> Users { get; set; }
}
