using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAsyncRepository orderasyncrepo;
        public OrderController(IOrderAsyncRepository orderasyncrepo)
        {
            this.orderasyncrepo = orderasyncrepo;
        }

            // GET: OrderController
        public async Task<ActionResult> GetMyOrders(long userId)
        {

           
            var uId = HttpContext.Session.GetString("userId");
             userId = Int32.Parse(uId);          
            var resutl = await orderasyncrepo.GetMyOrders(userId);
            var x = HttpContext.Request.QueryString.Value;
            if(x == "?prod=1")
            {
                ViewBag.num =1; 
            }
            else
            {
                ViewBag.num = 0;

            }
            return View(resutl);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult OrderItem(long id,double price)
        {
            ViewBag.id = id;
            ViewBag.price = price;
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OrderItem(Order order)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                order.CustomerId = UserId;
                order.createdBy = UserId;
                var prod=await orderasyncrepo.OrdreItem(order);
                if (prod > 0)
                {

                    return RedirectToAction(nameof(GetMyOrders), new {prod});
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
