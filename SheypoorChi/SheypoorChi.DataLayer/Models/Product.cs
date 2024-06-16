using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SheypoorChi.DataLayer.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "درج عنوان آگهی الزامیست")]
    [Display(Name = "گروه آگهی")]
    public int GroupId { get; set; }

    [Required(ErrorMessage = "درج عنوان آگهی الزامیست")]
    [Display(Name = "عنوان آگهی", Prompt = "عنوان آگهی")]
    public string Title { get; set; }

    [Display(Name = "توضحات آگهی", Prompt = "توضیحات آگهی")]
    public string? Description { get; set; }

    //[Required(ErrorMessage = "درج تصویر آگهی الزامیست")]
    [Display(Name = "تصویر آگهی", Prompt = "تصویر آگهی")]
    public string? Img { get; set; }

    [Display(Name = "تاریخ آگهی", Prompt = "تاریخ آگهی")]
    public string SubmitDate { get; set; }

    [Display(Name = "وضعیت نمایش آگهی")]
    public bool NotShow { get; set; } = true;

    [Display(Name = "قیمت")]
    public int Price { get; set; } = 0;

    [Display(Name ="درصد تخفیف")]
    public int SellOff { get; set; }= 0;

    [Display(Name = "موجودی")]
    public int Inventory { get; set; } = 0;


    //Product -> Group : 1
    [ForeignKey(nameof(GroupId))]
    virtual public Group? Group { get; set; }
}
