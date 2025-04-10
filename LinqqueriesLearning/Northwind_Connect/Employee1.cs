﻿using System;
using System.Collections.Generic;

namespace LinqqueriesLearning.Northwind_Connect;

public partial class Employee1
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }

    public string? TitleOfCourtesy { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? HomePhone { get; set; }

    public string? Extension { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public int? ReportsTo { get; set; }

    public string? PhotoPath { get; set; }

    public long? DeptId { get; set; }

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Employee1> InverseReportsToNavigation { get; set; } = new List<Employee1>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Employee1? ReportsToNavigation { get; set; }

    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
