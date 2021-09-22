using sinemovie.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sinemovie.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security

        sinemovieEntities db = new sinemovieEntities();
        
   

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(users users)
        {
            var userindb = db.users.FirstOrDefault(x => x.username == users.username && x.password == users.password);
            if(userindb!=null)
            {
                FormsAuthentication.SetAuthCookie(userindb.username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "false";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(users users)
        {
            var isUsernameAlreadyExist = db.users.Any(x => x.username == users.username);
            var isEmailAlreadyExists = db.users.Any(x => x.email == users.email);
            if (isEmailAlreadyExists)
            {
                TempData["msg"] = "false";
            }
            else if (isUsernameAlreadyExist)
            {
                TempData["msg"] = "false";
            }
            
            else
            {
                users.role = "U";
                db.users.Add(users);
                db.SaveChanges();
                TempData["msg"] = "true";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(users users)
        {
            var model = db.users.Where(x => x.email == users.email).FirstOrDefault();
            if(model!=null)
            {
                Guid random = Guid.NewGuid();
                model.password = random.ToString().Substring(0, 8);
                db.SaveChanges();
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("cinemovieproje@yandex.com", "Parola Sıfırlama");
                mail.To.Add(model.email);
                mail.IsBodyHtml = true;
                mail.Subject = "Parola Sıfırlama Talebi";
                mail.Body += "Merhaba " + model.username + "<br/> Kullanıcı Adınız: " + model.username + "<br/> Yeni Parolanız: " + model.password;
                NetworkCredential net = new NetworkCredential("enter mail address","enter mail password");
                client.Credentials = net;
                client.Send(mail);
                TempData["msg"] = "true";
                return RedirectToAction("Index", "Home");
            }
            TempData["msg"] = "false";
            return RedirectToAction("Index", "Home");
        }

    }
}