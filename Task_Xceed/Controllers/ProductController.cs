using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Xceed.Models;
using Task_Xceed.Repository;
using Task_Xceed.ViewModels;

namespace Task_Xceed.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _Productrepository;
        private readonly ICategoryRepository _CategoryRepository;
        public ProductController(IProductRepository productRepo , ICategoryRepository categoryRepository)
        {
            _Productrepository = productRepo;
            _CategoryRepository = categoryRepository;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            ViewBag.Categories = new SelectList(_CategoryRepository.GetCategories(), "Id", "Name");
            if (User.IsInRole("Admin"))
            {
                var products = _Productrepository.GetProducts();
                return View(products);
            }
            else
            {
                var products = _Productrepository.GetCurrentProducts();
                return View(products);
            }
           // var products = User.IsInRole("Admin")? _Productrepository.GetProducts()
                                                 //: _Productrepository.GetCurrentProducts();
            //return View(products);
        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            var CategoryID = int.Parse(collection["CateDDL"]);
            var SelectedProducts = _Productrepository.GetProductsByCategory(CategoryID);
            var products = _Productrepository.GetProducts();
            ViewBag.Categories = new SelectList(_CategoryRepository.GetCategories(), "Id", "Name",CategoryID);
            return View(SelectedProducts);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = _Productrepository.GetProductDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Categories = _CategoryRepository.GetCategories();
            return View(new Product());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                product.CreationByUserId = User.Identity?.Name;

           
                _Productrepository.InsertProduct(product);
                return RedirectToAction(nameof(Index));
                
                //return View(productViewModel);
                //return RedirectToAction(nameof(Index));
                //return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var product = _Productrepository.GetProductDetails(id);
            ViewBag.Categories = _CategoryRepository.GetCategories();
            if (product == null)
            {
                return NotFound();
            }


            //var model = new ProductViewModel
            //{
             //   Name = product.Name,
             //   StartDate = product.StartDate,
             //   Duration = product.Duration,
             //   Price = product.Price,
            //    CategoryId = product.CategoryId
           // };
            return View(product);
            //return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var product = new Product
                    //{
                        //Id = id,
                        //Name = productViewModel.Name,
                       // StartDate = productViewModel.StartDate,
                       // Duration = productViewModel.Duration,
                       // Price = productViewModel.Price,
                       // CategoryId = productViewModel.CategoryId
                   // };
                    _Productrepository.UpdateProduct(id, product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var product = _Productrepository.GetProductDetails(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _Productrepository.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
