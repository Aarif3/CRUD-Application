using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using InventryShop.Controllers;
using System.Web.UI.WebControls;
using InventryShop.viewmodel;

namespace InventryShop.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryList> CategoriesList { get; set; }
        public DbSet<SignUp> SignupTbl { get; set; }


        string cs = ConfigurationManager.ConnectionStrings["productcontext"].ConnectionString;

        public async Task<List<Product>> Getproduct()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Products", conn);


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        Product p = new Product();
                        p.Id = dr.GetFieldValue<int>(0);
                        p.ProductName = dr.GetFieldValue<string>(1);
                        p.ProductDescription = dr.GetFieldValue<string>(2);
                        p.ProductPrice = dr.GetFieldValue<int>(3);
                        products.Add(p);

                    }
                }
            }
            return products;

        }

        public async Task<Product> GetproductByIdAsync(int id)
        {

            Product p = new Product();
            using (SqlConnection conn = new SqlConnection(cs))
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

        public async Task<bool> CreateProductAsync(Product products)
        {
            using (SqlConnection con = new SqlConnection(cs))
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


        public async Task<bool> EditProductAsync(Product products)
        {
            using (SqlConnection conn = new SqlConnection(cs))
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


        public async Task<bool> DeleteProductAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
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



        //Category


        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Category cat = new Category();
                        cat.Id = dr.GetFieldValue<int>(0);
                        cat.CategoryName = dr.GetFieldValue<string>(1);
                        cat.IsActive = dr.GetFieldValue<bool>(2);

                        categories.Add(cat);
                    }
                }
                return categories;
            }
        }

        public async Task<bool> CreateCategory(Category cat)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCreatecategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                cmd.Parameters.AddWithValue("@Active", cat.IsActive);
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



        public async Task<Category> GetCategoryByID(int id)
        {
            Category cat = new Category();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        cat.Id = dr.GetFieldValue<int>(0);
                        cat.CategoryName = dr.GetFieldValue<string>(1);
                        cat.IsActive = dr.GetFieldValue<bool>(2);
                    }
                }

            }
            return cat;
        }




        public async Task<bool> EditCategory(Category cat)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spEditCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", cat.Id);
                cmd.Parameters.AddWithValue("@Name", cat.CategoryName);
                cmd.Parameters.AddWithValue("@Active", cat.IsActive);
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

        public async Task<bool> DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeletecategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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


        public async Task<bool> DeActiveCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeActiveCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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


        public async Task<bool> ActiveCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spActiveCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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

        public async Task<bool> AddProductInCategory(int id, int CatId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddProductToCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pid", id);
                cmd.Parameters.AddWithValue("@Cid", CatId);
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

        public async Task<List<Product>> GetProductList(int id)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spShowCatProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CatagoryId", id);
                con.Open();
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Product Pro = new Product();
                        Pro.Id = dr.GetFieldValue<int>(0);
                        Pro.ProductName = dr.GetFieldValue<string>(1);
                        Pro.ProductDescription = dr.GetFieldValue<string>(2);
                        Pro.ProductPrice = dr.GetFieldValue<int>(3);

                        products.Add(Pro);
                    }
                }
                return products;

            }


        }


        //public async Task<List<Product>> AddProduct(int id)
        //{
        //    List<Product> products = new List<Product>();
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("spShowAddProductList", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@CategoryId", id);
        //        con.Open();
        //        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
        //        {
        //            while (dr.Read())
        //            {
        //                Product Pro = new Product();
        //                Pro.Id = dr.GetFieldValue<int>(0);
        //                Pro.ProductName = dr.GetFieldValue<string>(1);
        //                Pro.ProductDescription = dr.GetFieldValue<string>(2);
        //                Pro.ProductPrice = dr.GetFieldValue<int>(3);

        //                products.Add(Pro);
        //            }
        //        }
        //        return products;

        //    }
        //}


        //public bool Login()
        //{

        //}

        public async Task<List<Report>> GetReport(int id)
        {
            List<Report> ree = new List<Report>();
            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);
                con.Open();
                using(SqlDataReader dr =await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Report re = new Report();
                        re.UserName = dr.GetFieldValue<string>(0);
                        re.CatName = dr.GetFieldValue<string>(1);
                        re.ProName = dr.GetFieldValue<string>(2);
                        re.price = dr.GetFieldValue<int>(3);

                        ree.Add(re);
                    }

                }
            return ree;
            }
        }

        public List <Report> GetAllreport()
        {
            List<Report> re = new List<Report>();
            using(SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spReports", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Report ree = new Report();
                        ree.UserName = dr.GetFieldValue<string>(0);
                        ree.CatName= dr.GetFieldValue<string>(1);
                        ree.ProName= dr.GetFieldValue<string>(2);
                        ree.price = dr.GetFieldValue<int>(3);
                        re.Add(ree);
                    }
                }
                return re;
            }
        }

        //public System.Data.Entity.DbSet<InventryShop.viewmodel.Report> Reports { get; set; }
    }
}

