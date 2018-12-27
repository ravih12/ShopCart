using ShopCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCart.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult HomePage()
        {
           using (ShoppingCartEntities db = new ShoppingCartEntities())
            {
                var result = db.tblProductDetails.ToList();

                return View(result);
            }
        }
        [HttpGet]
        public ActionResult Cart()
        {
            DisplayOrderDetailsVM cdList = new DisplayOrderDetailsVM();
            cdList.CDVM = new List<CartDetailsViewModel>();
            if (Session["cart"] != null)
            {
                DisplayOrderDetailsVM cartIds = (DisplayOrderDetailsVM)Session["cart"]; //type casting
                using (ShoppingCartEntities db = new ShoppingCartEntities())
                {
                    CartDetailsViewModel cd = new CartDetailsViewModel();
                    
                    foreach (var item in cartIds.CDVM)
                    {
                        
                         cd = db.tblProductDetails.Where(c => c.id == item.ProductId).Select(c => new CartDetailsViewModel
                        {
                            ProductName = c.productName,
                            Description = c.description,
                            Id = c.id,
                            ImagePath = c.imagePath,
                            Price = c.price,
                            ProductId = c.productId,
                            Quantity = item.Quantity,
                            TotalPrice = item.Quantity * c.price
                        }).FirstOrDefault();

                        cdList.CDVM.Add(cd);
                    }
                }
            }
            return View(cdList.CDVM);

        }
        [HttpPost]
        public ActionResult Addtocart(int id)
        {
            ShoppingCartEntities objdb = new ShoppingCartEntities();
            var price = objdb.tblProductDetails.Where(c => c.productId == id).FirstOrDefault().price;
            DisplayOrderDetailsVM cartIds = new DisplayOrderDetailsVM();
            cartIds.CDVM = new List<CartDetailsViewModel>();
            if (Session["cart"] == null)
            {
                CartDetailsViewModel cv = new CartDetailsViewModel()
                {
                    ProductId = id,
                    Quantity = 1,
                    Price = price
                };
                cartIds.CDVM.Add(cv);
                Session["cart"] = cartIds;
            }
            else
            {
                cartIds = (DisplayOrderDetailsVM)Session["cart"];
                var prod = cartIds.CDVM.Where(c => c.ProductId == id).FirstOrDefault();
                if (prod != null)
                {
                    prod.Quantity = prod.Quantity + 1;
                }
                else
                {
                    CartDetailsViewModel cv = new CartDetailsViewModel()
                    {
                        ProductId = id,
                        Quantity = 1,
                        Price = price
                    };
                    cartIds.CDVM.Add(cv);
                }
                Session["cart"] = cartIds;

            }
            return Json(new { totalCount = cartIds.CDVM.Count }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult GetCartItems()
        {
            
            DisplayOrderDetailsVM ids = new DisplayOrderDetailsVM();
            CartDetailsViewModel objcdvm = new CartDetailsViewModel();
           
            if (Session["Cart"] != null)
            {
                
                ids = (DisplayOrderDetailsVM)Session["Cart"];
                            
            
                return Json(new { cartItemsCount = ids.CDVM.Count, cartItems = ids});

            }
            else
            {
                return Json(new { cartItemsCount = 0, cartItems = ids }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult Shipping()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Shipping(ShippingViewModel objship)
        {
            
            DisplayOrderDetailsVM ids = new DisplayOrderDetailsVM();
            if (Session["Cart"] != null)
            {
                ids = (DisplayOrderDetailsVM)Session["Cart"];
                ids.SVM = objship;

            }
            using (ShoppingCartEntities objdb = new ShoppingCartEntities())
            {
                tblCustomerDetail objtblcd = new tblCustomerDetail();
                tblItemQuantity objtbliq = new tblItemQuantity();
                objtblcd.customerName = ids.SVM.CustomerName;
                objtblcd.customerAddress = ids.SVM.Address;
                objtblcd.mobile = ids.SVM.Mobile;
                objdb.tblCustomerDetails.Add(objtblcd);
                objdb.SaveChanges();

                foreach (var i in ids.CDVM)
                {
                    
                    objtbliq.itemId = i.ProductId;
                    objtbliq.Quantity = i.Quantity;
                    objtbliq.customerId = objtblcd.customerId;
                    objdb.tblItemQuantities.Add(objtbliq);
                    objdb.SaveChanges();
                }
                
               

                
               

                
                return Json(new { msg="Order Placed", Url = "/Home/HomePage" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult RemoveFromCart(int id)
        {
            //DisplayOrderDetailsVM ids = new DisplayOrderDetailsVM();
            DisplayOrderDetailsVM cartIds = (DisplayOrderDetailsVM)Session["cart"];
            var prod = cartIds.CDVM.Where(c => c.ProductId == id).FirstOrDefault();
            if (prod != null)
            {
                cartIds.CDVM.Remove(prod);
            }

            Session["cart"] = cartIds;
            return Json(new {msg="success", cartItemsCount = cartIds.CDVM.Count });
        }

        public ActionResult DecreaseQty(int id)

        {
            int grandAmount = 0;
            DisplayOrderDetailsVM cartIds = (DisplayOrderDetailsVM)Session["cart"];
            var qty = cartIds.CDVM.Where(c => c.ProductId == id).FirstOrDefault();
            Session["cart"] = null;
            if (qty != null)
            {
                qty.Quantity = qty.Quantity - 1;
                

            }
            Session["cart"] = cartIds;
            foreach (var i in cartIds.CDVM)
            {
                grandAmount = grandAmount + (i.Price * i.Quantity);
             
            }
            return Json(new { msg = "DecreaseQty_Success", updateQty=qty.Quantity, grandAmount });
        }
        public ActionResult IncreaseQty(int id)

        {
            int grandAmount = 0;
            DisplayOrderDetailsVM cartIds = (DisplayOrderDetailsVM)Session["cart"];
            var qty = cartIds.CDVM.Where(c => c.ProductId == id).FirstOrDefault();
            Session["cart"] = null;
            if (qty != null)
            {
                
                qty.Quantity = qty.Quantity + 1;


            }
            Session["cart"] = cartIds;
            foreach (var i in cartIds.CDVM)
            {
                grandAmount = grandAmount + (i.Price * i.Quantity);

            }
            return Json(new { msg = "DecreaseQty_Success", updateQty = qty.Quantity, grandAmount });
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}