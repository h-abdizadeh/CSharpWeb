

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace SheypoorChi.DataLayer.Models;

public class Factor
{
    

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "شماره فاکتور")]
    public int Id { get; set; }
    public Guid UserId { get; set; }

    [Display(Name = "پرداخت شده")]
    public bool IsPay { get; set; } = false;

    [Display(Name = "تاریخ پرداخت")]
    public string? PayDate { get; set; } 
    
    [Display(Name = "رهگیری تراکنش")]
    public string? PayInfo { get; set; }//refId

    [Display(Name = "تاریخ تکمیل سفارش")]
    public string? CloseDate { get; set; }

    [Display(Name = "مبلغ فاکتور")]
    public int TotalPrice { get; set; } = 0;

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [Display(Name = "وضعیت")]
    public string Status { get; set; } = new Factor().StatusArray[0];

    //test
    public string[] StatusArray { get; } =
        { "پرداخت نشده", "پرداخت شده", "در حال آماده سازی", "ارسال شده", "بسته شده" };

    //test


    //(factor) n : 1 (user)
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    //(factor) 1 : n (factorDetail)
    public virtual List<FactorDetail>? Details { get; set; }
}
