using sinemovie.Models;
using sinemovie.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using sinemovie.Models.IEnumerable;

namespace sinemovie.Controllers
{
    public class HomeController : Controller
    {
        sinemovieEntities db = new sinemovieEntities();

        MoviesComments mc = new MoviesComments();
        public ActionResult Index(int sayfa = 1)
        {
            var model = db.movies.ToList().ToPagedList(sayfa, 20);
            return View(model);
        }

        
        public ActionResult FilmDetay(int id)
        {
            mc.Movies = db.movies.Where(x => x.id == id).ToList();
            mc.Comments = db.comments.Where(x => x.movie_id == id).ToList();
            return View(mc);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Comments(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Comments(comments comments)
        {
            
                comments.date = DateTime.Now;
                db.comments.Add(comments);
                db.SaveChanges();
                return PartialView();
               

        }
        public PartialViewResult Actor(int id)
        {
            mc.Actors = db.actors.Where(x => x.movie_id == id).ToList();
            return PartialView(mc);
        }

        public ActionResult OyuncuDetay(int id)
        {
            mc.Actors = db.actors.Where(x => x.id == id).ToList();
            return View(mc);
        }

        public ActionResult Oyuncular(int sayfa=1)
        {
            var model = db.actors.ToList().ToPagedList(sayfa, 10);
            return View(model);
        }

        public ActionResult Hakkımızda()
        {
            return View();
        }

        public ActionResult Yardım()
        {
            return View();
        }
    }
}