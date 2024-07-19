using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheypoorChi.DataLayer.Models;

public class UserInfo
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
