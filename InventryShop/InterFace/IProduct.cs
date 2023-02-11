using InventryShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryShop.InterFace
{
    public interface IProduct
    {
        Task<bool> Create(Product pro);
        Task<bool> Edit(Product pro);
        Task<bool> Delete(int id);
        Task <Product> GetProductByID(int id);

    }
}
