

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SheypoorChi.DataLayer.Models;

public class UserDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid UserId { get; set; }

    public string Fname { get; set; }
    public string Lname { get; set; }

    public string Province { get; set; }
    public string City { get; set; }

    public string Address { get; set; }
    public string PostalCode { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
}
