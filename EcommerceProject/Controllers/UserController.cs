using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EcommerceProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAsyncRepository userasynrepo;
        public UserController(IUserAsyncRepository userasynrepo)
        {
            this.userasynrepo = userasynrepo;
        }

         // GET: UserController
        public async Task<ActionResult> Index()
        {
            var result = await userasynrepo.GetAllUsers();
            return View(result);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult UserRegistration()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRegistration(UserRegistrationModel userregistration)
        {
            try
            {
                var result=await userasynrepo.UserRegistration(userregistration);

                return RedirectToAction(nameof(SetPassword), new { result });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddEmployee()
        {
            var emp = await userasynrepo.GetAllUsersAdd();
            return View(emp);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(UserRegistrationModel userregistration)
        {
            try
            {
                var result = await userasynrepo.AddEmployee(userregistration);

                return RedirectToAction(nameof(SetPassword), new { result });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SetPassword(long result)
        {
            ViewBag.id = result;
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(UserPasswordModel userPassword)
        {
            try

            {

                var resultt = await userasynrepo.SetPassword(userPassword);

                return RedirectToAction("Index","Product");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLogInModel userloginmodel)
        {
            var user = await userasynrepo.UserLogIn(userloginmodel);
            if (user != null)
            {
                if (user.Password == userloginmodel.Password)
                {
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userRole", user.Role);
                    ViewBag.user = user.UserName;

                    if (user.Role == "Admin")
                        return RedirectToAction("OrderContSales", "Product");
                    else
                        return RedirectToAction("Index", "Product");
                }
                else
                    ViewBag.num = 1;
                return View();
            }
            else
            {
                ViewBag.num = 1;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
