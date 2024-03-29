﻿using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace SheypoorChi.DataLayer.Models;

public class Group
{
    [Key]
    public int Id { get; set; }

    [Display(Name ="نام گروه",Prompt ="نام گروه")]
    [Required(ErrorMessage ="درج نام گروه الزامیست")]
    [MaxLength(15,ErrorMessage ="نام گروه حداکثر 15 کاراکتر")]
    public string GroupName { get; set; }

    [Display(Name = "آیکن", Prompt = "کد آیکن")]
    public string? Img { get; set; }

    [Display(Name = "وضعیت نمایش")]
    public bool NotShow { get; set; } = false;

    //Group -> Product : n
    virtual public ICollection<Product>? Products { get; set; }
}
