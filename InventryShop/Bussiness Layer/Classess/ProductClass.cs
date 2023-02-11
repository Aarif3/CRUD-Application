using InventryShop.InterFace;
using InventryShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace InventryShop.Bussiness_Layer.Classess
{
    public class ProductClass : IProduct
    {
        readonly ProductContext db;
        public ProductClass()
        {
            db = new ProductContext();
        }
        public async Task<bool> Create(Product products)
        {
            using (SqlConnection con = new SqlConnection(db.cs))
            {
                SqlCommand cmd = new SqlCommand("spCreateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", products.ProductName);
                cmd.Parameters.AddWithValue("@Description", products.ProductDescription);
                cmd.Parameters.AddWithValue("@Price", products.ProductPrice);
                cmd.Parameters.AddWithValue("@Userid", products.UserId);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }


        

        public async Task<bool> Edit(Product products)
        {
            using (SqlConnection conn = new SqlConnection(db.cs))
            {
                SqlCommand cmd = new SqlCommand("spEditProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", products.Id);
                cmd.Parameters.AddWithValue("@Name", products.ProductName);
                cmd.Parameters.AddWithValue("@price", products.ProductPrice);
                cmd.Parameters.AddWithValue("@Description", products.ProductDescription);
                conn.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        

       public async Task<Product>GetProductByID(int id)
       {
            Product p = new Product();
            using (SqlConnection conn = new SqlConnection(db.cs))
            {
                SqlCommand cmd = new SqlCommand("SpSearchId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        p.Id = dr.GetFieldValue<int>(0);
                        p.ProductName = dr.GetFieldValue<string>(1);
                        p.ProductDescription = dr.GetFieldValue<string>(2);
                        p.ProductPrice = dr.GetFieldValue<int>(3);

                    }
                }
            }
            return p;
        }
        public async Task<bool> Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(db.cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}