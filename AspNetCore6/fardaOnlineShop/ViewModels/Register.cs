using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FardaOnlineShop.ViewModels;

public class Register
{
    [Display(Name ="نام کاربری",Prompt ="شماره موبایل")]
    [Required(ErrorMessage ="شماره موبایل الزامیست")]
    [MinLength(11,ErrorMessage ="شماره موبایل 11 رقم")]
    [MaxLength(11,ErrorMessage ="شماره موبایل 11 رقم")]
    public string  Mobile { get; set; }//user name

    [Display(Name ="رمز عبور",Prompt ="8 تا 15 کاراکتر")]
    [Required(ErrorMessage = "رمز عبور الزامیست")]
    [MinLength(8, ErrorMessage = "رمز حداقل 8 کاراکتر")]
    [MaxLength(15, ErrorMessage = "رمز حداکثر 15 کاراکتر")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار رمز عبور", Prompt = "8 تا 15 کاراکتر")]
    [Required(ErrorMessage = "تکرار رمز عبور الزامیست")]
    [MinLength(8, ErrorMessage = "رمز حداقل 8 کاراکتر")]
    [MaxLength(15, ErrorMessage = "رمز حداکثر 15 کاراکتر")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),ErrorMessage ="رمز همخوانی ندارد")]
    public string RePassword { get; set; }
}
