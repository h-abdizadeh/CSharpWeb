using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FardaOnlineShop.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "کد محصول")]
    public int Id { get; set; }

    [Required(ErrorMessage ="ثبت عنوان کالا الزامیست")]
    [Display(Name = "نام محصول")]
    public string Name { get; set; }

    [Display(Name ="توضیحات محصول")]
    public string? Description { get; set; }

    [Display(Name = "قیمت")]
    public int Price { get; set; } = 0;

    [Display(Name = "درصد تخفیف")]
    public int SellOff { get; set; } = 0;

    [Display(Name = "تصویر محصول")]
    public string? Img { get; set; }

    [Display(Name = "تعداد موجودی")]
    public int Inventory { get; set; } = 0;

    [Display(Name = "عدم نمایش")]
    public bool NotShow { get; set; } = false;


    public int GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    virtual public Group? Group { get; set; }
}
