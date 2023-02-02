using InventryShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InventryShop.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Product

        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(int PageNumber = 1)
        {
            
                var parameter = new[]
                {
                 new SqlParameter ("@PageNbr",PageNumber),      //[0]index
                 new SqlParameter ("@TotalPages",SqlDbType.Int) { Direction = ParameterDirection.Output}    //[1]index
                };
            var data =await db.Products.SqlQuery("Execute spGetpageRow @PageNbr,@Totalpages output", parameter).ToListAsync();

            //parameter[1] is SqlParameter ("@TotalPages",SqlDbType.Int) { Direction = ParameterDirection.Output}
            ViewBag.Totalpages = (int)parameter[1].Value;
            return View(data);

        }

        public ActionResult Create()
        {
            var data = db.SignupTbl.FirstOrDefault(model => model.UserName == User.Identity.Name);

            Product product = new Product()
            {
                UserId = data.id
            };
            return View(product);

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync(Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Products.Add(p);
                    bool check = await db.CreateProductAsync(p);

                    //int a =await db.SaveChangesAsync();
                    if (check == true)
                    {
                        TempData["Message"] = "<script>alert('Item Created successfully')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "<script>alert('Item Created successfully')</script>";
                    }
                }


                return View();
            }

            catch
            {
                return View();
            }


        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(int id)
        {
            //var row = await db.Products.Where(Model => Model.Id == id).FirstOrDefaultAsync();
            //var row =db.Getproduct().Find(Model => Model.Id== id);
            var row = await db.GetproductByIdAsync(id);
            return View(row);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(Product p)
        {
            //db.Entry(p).State = EntityState.Modified;
            //int a =await db.SaveChangesAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    bool a = await db.EditProductAsync(p);
                    if (a == true)
                    {
                        TempData["Message"] = "<script>alert('Data Edited Successfully')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "<script>alert('Data Not Edited')</script>";
                        ModelState.Clear();
                    }


                }
                return View();

            }
            catch
            {
                return View();
            }


        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            //var detailsvalue = await db.Products.Where(Model => Model.Id == id).FirstOrDefaultAsync();

            //var detailsvalue = db.DetailsProductAsync();
            var detailsvalue =await db.GetproductByIdAsync(id);
            return View(detailsvalue);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            //var deletevalue = await db.Products.Where(Model => Model.Id == id).FirstOrDefaultAsync();
            var deletevalue = await db.GetproductByIdAsync(id);
            return View(deletevalue);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(Product pro)
        {

                bool a = await db.DeleteProductAsync(pro.Id);
                if (a == true)
                {
                    TempData["Message"] = "<script>alert('Product Delted Successfully')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "<script>alert('Product Not Deleted')</script>";
                    ModelState.Clear();
                }


            
            return View();



            //    db.Entry(p).State = EntityState.Deleted;
            //    int a =await db.SaveChangesAsync();

            //    if (a > 0)
            //    {
            //        TempData["Message"] = "<script>alert('Data Deleted Successfully')</script>";
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        TempData["Message"] = "<script>alert('Data Not deleted')</script>";
            //    }

            //    return View();

            //}
        }
    }
}