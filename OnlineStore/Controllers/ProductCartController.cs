using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Models;
using OnlineStore.RepoServices;

namespace OnlineStore.Controllers
{
    public class ProductCartController : Controller
    {
        public IProductRepository productRepository { get; set; }
        public ICartRepository cartRepository { get; set; }
        public IProductCartRepository productCartRepository { get; set; }

        public ProductCartController(IProductRepository productRepository, ICartRepository cartRepository, IProductCartRepository productCartRepository)
        {
            this.productCartRepository = productCartRepository;
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
        }
        // GET: ProductCartController
        public ActionResult Index()
        {
            return View(productCartRepository.GetAll());
        }

        // GET: ProductCartController/Details/5
        public ActionResult Details(int CartId, int ProductId)
        {
            var obj = productCartRepository.GetDetails(CartId, ProductId);
            return View(productCartRepository.GetDetails(CartId, ProductId));
            
            return View(productCartRepository.GetDetails(CartId, ProductId));

        }

        // GET: ProductCartController/Create
        public ActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(productRepository.GetAll(), "ProductID", "ProductName");
            ViewData["CartId"] = new SelectList(cartRepository.GetAll(), "CartId", "CartId");

            return View();
        }

        // POST: ProductCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCart productCart)
        {
            if (ModelState.IsValid)
            {

                try { productCartRepository.Insert(productCart); }
                catch
                {
                    return Problem("Cant Create");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ProductCartController/Edit/5
        public ActionResult Edit(int CartId, int ProductId)
        {

            if (CartId == null || ProductId == null)
            {
                return View();
            }
            try
            {
                ProductCart productCart = productCartRepository.GetDetails(CartId, ProductId);
                ViewData["ProductId"] = new SelectList(productRepository.GetAll(), "ProductID", "ProductName", productCart.ProductId);
                ViewData["CartId"] = new SelectList(cartRepository.GetAll(), "CartId", "CartId",productCart.CartId);
                return View(productCart);
            }
            catch
            {
                return View();
            }



        }

        // POST: ProductCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try {
                var obj1 = collection["CarId"];
                var obj2 = collection["ProductId"];
                int oldCartId = Convert.ToInt32(obj1[0]);
                int oldProductId = Convert.ToInt32(obj2[0]);
                int newCartId = Convert.ToInt32(obj1[1]);
                int newProductId = Convert.ToInt32(obj2[1]);

                ProductCart productCart = new ProductCart();
                productCart.CartId = newCartId;
                productCart.ProductId = newProductId;
                productCartRepository.DeleteProductCart(oldCartId, oldProductId);
                productCartRepository.Insert(productCart);
                return RedirectToAction("Index");
            }
            catch
            {
                return Problem("Can't Edit");
            }

        }
       
        public ActionResult EditProductCart(int cartId,int prodId,int quantity)
        {
            try
            {
                ProductCart product = productCartRepository.GetDetails(cartId, prodId);
                product.CartId = cartId;
                product.ProductId = prodId;
                product.ProductQuantity= quantity;
                
                productCartRepository.UpdateProductCart(product);
               
                return  RedirectToAction("Index", "Cart", new { area = "", id = cartId});
            }
            catch
            {
                return Problem("Can't Edit");
            }

        }

        // GET: ProductCartController/Delete/5
        public ActionResult Delete(int? CarId, int? ProductId)
        {
            if (CarId == null||ProductId==null)
            {
                return NotFound();
            }

            ProductCart productCart = productCartRepository.GetDetails((int)CarId,(int) ProductId);

            if (productCart == null)
            {
                return NotFound();
            }
           
            return View(productCart);
        }

        // POST: ProductCartController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int CartId, int ProductId)
        {
           
            productCartRepository.DeleteProductCart(CartId, ProductId);
            return RedirectToAction("Index");
        }
    }
}
