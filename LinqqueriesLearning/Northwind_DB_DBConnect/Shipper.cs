﻿using System;
using System.Collections.Generic;

namespace LinqqueriesLearning.Northwind_DB_DBConnect;

public partial class Shipper
{
    public int ShipperId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
