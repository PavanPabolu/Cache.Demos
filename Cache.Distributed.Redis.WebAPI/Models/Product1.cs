using System;
using System.Collections.Generic;

namespace Cache.Distributed.Redis.WebAPI.Models;

public class Product1
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal ListPrice { get; set; }

}
