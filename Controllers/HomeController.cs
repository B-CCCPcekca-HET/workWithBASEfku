
using System.Web;
using System.Web.Mvc;
using Webnfwithapi1.Models;

namespace Webnfwithapi1.Controllers
{
    public class HomeController : Controller
    {
        

        private MyContext db = new MyContext();
       
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Api( )
        {
            ViewBag.Message = "Your api page.";

          

            return View();
        }
    

        [Authorize]
        public ActionResult Zayavki()
        {
            ViewBag.Message = "Your zayavki page.";
            return View();
        }

        [System.Web.Http.Authorize]
        public ActionResult Users()
        {
            ViewBag.Message = "Your users page.";



            return View();
        }


        
    
         public ActionResult Upload()
         {

           
            return View();

        }



        

    }


    }
