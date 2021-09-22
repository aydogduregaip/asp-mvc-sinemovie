using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sinemovie.Models.EntityFramework;
using sinemovie.Models.IEnumerable;

namespace sinemovie.Controllers
{
    [Authorize(Roles = "A")]
    public class AdminController : Controller
    {
        
        
        sinemovieEntities db = new sinemovieEntities();
        MoviesComments mc = new MoviesComments();

        public ActionResult Index()
        {
            var movie = db.movies.ToList();
            return View(movie);
        }

        [HttpGet]
        public ActionResult NewMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMovie(movies m)
        {
            db.movies.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMovie(int id)
        {
            var b = db.movies.Find(id);
            db.movies.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailsMovie(int id)
        {
            var b1 = db.movies.Find(id);
            return View("DetailsMovie",b1);
        }

        public ActionResult UpdateMovie(movies b)
        {
            var mov = db.movies.Find(b.id);
            mov.name = b.name;
            mov.release_date = b.release_date;
            mov.time = b.time;
            mov.genre = b.genre;
            mov.director = b.director;
            mov.summary = b.summary;
            mov.poster = b.poster;
            mov.fragman = b.fragman;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult DetailsComment()
        {
            var com = db.comments.ToList();
            return View(com);
        }

        public ActionResult DeleteComment(int id)
        {
            var b = db.comments.Find(id);
            db.comments.Remove(b);
            db.SaveChanges();
            return RedirectToAction("DetailsComment");
        }

        public ActionResult EditComment(int id)
        {
            var yr = db.comments.Find(id);
            return View("EditComment", yr);
        }

        public ActionResult UpdateComment(comments y)
        {
            var yrm = db.comments.Find(y.id);
            yrm.comment = y.comment;
            db.SaveChanges();
            return RedirectToAction("DetailsComment");
        }

        public ActionResult Actors()
        {
            var ac = db.actors.ToList();
            return View(ac);
        }

        public ActionResult DeleteActor(int id)
        {
            var oyuncu = db.actors.Find(id);
            db.actors.Remove(oyuncu);
            db.SaveChanges();
            return RedirectToAction("Actors");
        }

        [HttpGet]
        public ActionResult NewActor()
        {
            List<SelectListItem> degerler = (from i in db.movies.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.name.ToString(),
                                                 Value = i.id.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult NewActor(actors a)
        {
            var mi = db.movies.Where(m => m.id == a.movies.id).FirstOrDefault();
            a.movies = mi;
            db.actors.Add(a);
            db.SaveChanges();
            return RedirectToAction("Actors");
        }

        public ActionResult EditActor(int id)
        {
            var ea = db.actors.Find(id);
            return View("EditActor", ea);
        }

      
        public ActionResult UpdateActor(actors b)
        {
            var act = db.actors.Find(b.id);
            act.name = b.name;
            act.poster = b.poster;
            act.biography = b.biography;
            act.gender = b.gender;
            act.age = b.age;
            act.country = b.country;
            db.SaveChanges();
            return RedirectToAction("Actors");
        }









    }
}