﻿using DataAccess.Repositories;
using ProductWebApi.Models;

namespace BusinessLogic.Services
{
    public interface IProductService : IService<Product, int>
    {
        Task<IEnumerable<Product>> GetOrderedByPrice();
    }
}
