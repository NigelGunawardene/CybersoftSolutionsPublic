using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Models;

internal class OrderDetailsModel
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double TotalPrice { get; set; }
    public Orders Order { get; set; }
    public Products Product { get; set; }
}
