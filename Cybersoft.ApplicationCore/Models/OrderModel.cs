using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
    public double TotalPrice { get; set; }
    public string PublicOrderNumber { get; set; }
    public string Message { get; set; }
    public string OrderDate { get; set; }
    public bool IsComplete { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }

}
