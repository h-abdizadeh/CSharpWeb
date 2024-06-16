using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheypoorChi.DataLayer.Models;

public class FactorDetail
{
    [Key]
    public int Id { get; set; }

    public int FactorId { get; set; }

    //[ForeignKey(nameof(ProductId))] //(detail) 1 : 1 (product)
    public int ProductId { get; set; }

    [Display(Name ="تعداد")]
    public int DetailCount { get; set; }

    [Display(Name ="قیمت نهایی")]
    public int DetailPrice { get; set; }


    [ForeignKey(nameof(FactorId))]
    public virtual Factor? Factor { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
}
