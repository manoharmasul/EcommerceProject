using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

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
        public async Task<ActionResult> GetAllOrders()
        {     
            var resutl = await orderasyncrepo.GetAllOrders();   
            return View(resutl);
        }
        public async Task<ActionResult> GetMyOrders(long userId)
        {

           
            var uId = HttpContext.Session.GetString("userId");
             userId = Int32.Parse(uId);          
            var resutl = await orderasyncrepo.GetMyOrders(userId);
            var x = HttpContext.Request.QueryString.Value;
            if(x == "?ord=1")
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

        // GET: OrderController/Create
        public ActionResult UpdateOrderStatuss()
        {
          
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOrderStatuss(UpdateOrderStatus updateordstatus)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                updateordstatus.ModifiedBy = UserId;
              
                var ord = await orderasyncrepo.UpdateOrderStatuss(updateordstatus);
                if (ord > 0)
                {

                    return RedirectToAction(nameof(GetAllOrders), new { ord });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> UpdateOrder(long id)
        {
            var ord=await orderasyncrepo.GetOrderById(id);
            return View(ord);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOrder(Order updateorder)
        {
            try
            {
               
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                updateorder.modifiedBy = UserId;

                var ord = await orderasyncrepo.UpdateOrdre(updateorder);

                return RedirectToAction(nameof(GetAllOrders));
            }
            catch
            {
                return View();
            }
        }
        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOrderByCustomer(long Id)
        {
            try
            {

                var uId = HttpContext.Session.GetString("userId");
                long UserId = Int32.Parse(uId);
               

                var ord = await orderasyncrepo.UpdateOrdreByCustomer(Id, UserId);

                return RedirectToAction(nameof(GetAllOrders));
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
