using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities.Product;
using OrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        private readonly OrderManagementDbContext _context;
        public ProductRepository(OrderManagementDbContext context):base(context)
        {
            _context = context;
        }

    }
}
