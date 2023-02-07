using InventryShop.Models;
using InventryShop.viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast;

namespace InventryShop.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
    
        ProductContext db = new ProductContext();
        // GET: Category
        public async Task<ActionResult> Index(int PageNumber =1)
        {
            //List<Category> data = await db.GetCategories();

            var parameter = new[]
            {
                new SqlParameter("@Pagenmbr",PageNumber),
                new SqlParameter("@Totalpage",SqlDbType.Int) {Direction= ParameterDirection.Output}
            };
            var data =await db.Categories.SqlQuery("Execute spGetpageRowCategory @Pagenmbr,@Totalpage output", parameter).ToListAsync();
            ViewBag.TotalPages = parameter[1].Value;

            //var data = await db.Categories.ToArrayAsync();
            //ViewBag.Totalpages = Math.Ceiling(data.Count() / 5.0);
            //var data2 = data.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(data);
        }

        [Authorize(Roles ="Admin")]
        public  ActionResult Create() 
        {
            return View();
        }

        [ActionName("Create")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAsync(Category c)
        {
            if (ModelState.IsValid == true)
            {
                bool a =await db.CreateCategory(c);
                //int a =await db.SaveChangesAsync();
                if (a == true)
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


        [ActionName("Edit")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> EditAsync(int id)
        {
            //var idvalue =await db.Categories.Where(Model => Model.Id == id).FirstOrDefaultAsync();
            var idvalue =await db.GetCategoryByID(id);
            return View(idvalue);
        }



        [HttpPost]
        [ActionName("Edit")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> EditAsync (Category c)
        {
            if (ModelState.IsValid)
            {
                bool a =await db.EditCategory(c);
                //db.Entry(c).State = EntityState.Modified;
                //int a =await  db.SaveChangesAsync();
                if (a== true)
                {
                    TempData["Message"] = "<script>alert('Edited Successfully')</script>";
                    return RedirectToAction("Index");
                }

                {
                    TempData["Message"] = "<script>alert('Data Not Edited')</script>";
                    ModelState.Clear();
                }

            }

            return View();
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            //var detailsValue = await db.Categories.Where(Model => Model.Id ==id).FirstOrDefaultAsync();
            var detailsvalue =await db.GetCategoryByID(id);
            return View(detailsvalue);

        }


        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteAsync(int id)
        {
            //var deleteValue =await db.Categories.Where(Model => Model.Id == id).FirstOrDefaultAsync();
            var deleteValue =await db.GetCategoryByID(id);
            return View(deleteValue);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteAsync(Category c)
        {

            //db.Entry(c).State = EntityState.Deleted;
            //int a =await db.SaveChangesAsync();
            bool a =await db.DeleteCategory(c.Id);
            if (a == true)
            {
                TempData["Message"] = "<script>alert('Deleted Successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "<script>alert('Data Not Deleted ')</script>";
                ModelState.Clear();
            }
            return View();

        }


        [ActionName("AddProduct")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> AddProductAsync(int id)
        {
            //var data = await db.AddProduct(id);
            Session["CatagoryId"] = id;
            var data = await db.Products.ToListAsync();
            return View(data);
        }

        [ActionName("AddProductdata")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> AddProductDataAsync(int Pid, int Cid)
        {
            bool a =await db.AddProductInCategory(Pid,Cid);
            if(a == true)
            {
                TempData["Message"] = "<script>alert('Product Added Successfully')</script>";
                return RedirectToAction("ProductList","Category",new {id = Cid});
            }
            else
            {
                TempData["Message"] = "<script>alert('Product Not Added')</script>";
            }
            return RedirectToAction("ProductList", "Category",new {id = Cid});
        }
        //    CategoryList list = new CategoryList();

        //    list.ProductId = Pid;
        //    list.CategoryId = Cid;
        //    list.IsActive = true;

        //    var data = db.CategoriesList.Add(list);
        //    int a = await db.SaveChangesAsync();
        //    if (a > 0)
        //    {
        //        TempData["Message"] = "<script>alert('Product Added Successfully')</script>";
        //        return RedirectToAction("ProductList", new { id = list.CategoryId });
        //    }
        //    else
        //    {
        //        TempData["Message"] = "<script>alert('Product Not Added')</script>";
        //    }

        //    return RedirectToAction("ProductList","Category", new {id = list.CategoryId });

        //}

        [ActionName("ProductList")]
        public async Task<ActionResult>GetProductList(int id)
        {
             var a =await db.GetProductList(id);
            return View(a);

        }
        //    SqlParameter[] param = new SqlParameter[]
        //    {
        //        new SqlParameter ("@CatagoryId",id)
        //    };
        //    var data = await db.Database.SqlQuery<Product>(" @CatagoryId = @CatagoryId",param).ToListAsync();



        //    return View(data); 
        //}

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeActive(Category cat)
        {
            bool a =await db.DeActiveCategory(cat.Id);
            if(a==true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.Clear();
            }
            return View();

        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Active(Category cat)
        {
            bool a =await db.ActiveCategory(cat.Id);
            if (a == true)
            {
                //TempData["Message"] = "<script>alert('Category Activated Successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.Clear();
            }
            return View();

        }

        public async Task<ActionResult> Report()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            var IdenfierName = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            string name = IdenfierName.Value;
            var data1 = db.SignupTbl.FirstOrDefault(model => model.UserName == name);

            var data =await db.GetReport(data1.id);
            return View(data);
        }

        public ActionResult AllReport()
        {
            var data = db.GetAllreport();
            return View(data);
        }
    }
}