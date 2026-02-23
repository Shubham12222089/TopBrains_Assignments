using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class SalesRaw
{
    public int? OrderId { get; set; }

    public string? OrderDate { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerCity { get; set; }

    public string? ProductNames { get; set; }

    public string? Quantities { get; set; }

    public string? UnitPrices { get; set; }

    public string? SalesPerson { get; set; }
}
