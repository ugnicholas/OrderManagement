﻿using OrderManagement.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
    }
}
