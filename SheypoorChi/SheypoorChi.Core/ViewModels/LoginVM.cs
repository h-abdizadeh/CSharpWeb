

using System.ComponentModel.DataAnnotations;

namespace SheypoorChi.Core.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage ="شماره موبایل 11 رقمی وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره موبایل 11 رقمی وارد کنید")]
    [MinLength(11, ErrorMessage = "شماره موبایل 11 رقمی وارد کنید")]
    [Display(Name ="نام کاربری",Prompt ="شماره موبایل")]
    [DataType(DataType.PhoneNumber)]
    public string UserName { get; set; }//userMobile

    [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
    [MaxLength(15,ErrorMessage ="رمز عبور نمیتواند بیش از 15 کاراکتر باشد")]
    [MinLength(8,ErrorMessage ="رمز عبور نمیتواند کمتر از 8 کاراکتر باشد")]
    [Display(Name ="رمز عبور")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
