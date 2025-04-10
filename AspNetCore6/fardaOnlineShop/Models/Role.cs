using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FardaOnlineShop.Models;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Display(Name ="نقش کاربر", Prompt ="سطح دسرسی کاربر")]
    [Required(ErrorMessage ="ثبت نقش کاربر الزامیست")]
    public string Title { get; set; }


    virtual public ICollection<User>? Users { get; set; }
}
