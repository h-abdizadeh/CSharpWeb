using System.ComponentModel.DataAnnotations;

namespace FardaOnlineShop.Models;

public class Group
{
    [Key]//auto number
    [Display(Name ="ردیف")]
    public int Id { get; set; }

    [Required(ErrorMessage ="ثبت عنوان گروه الزامیست")]
    [Display(Name ="نام گروه",Prompt ="نام گروه")]
    [MaxLength(15,ErrorMessage ="نام گروه کمتر از 15 کاراکتر باشد")]
    public string Title { get; set; }

    virtual public ICollection<Product>? Products { get; set; }
}
