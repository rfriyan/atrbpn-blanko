using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlankoATRBPN.Models;

namespace BlankoATRBPN.Controllers
{
    
    public class PengembalianBlankoController : Controller
    {
        public Entities db = new Entities();
        // GET: PengembalianBlanko
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPengembalian()
        {
            var query =
               from pb in db.PENGELOLAAN_BLANKO
               join b in db.BLANKOes on pb.BLANKOID equals b.BLANKOID
               //where post.ID == id
               select new {  no = 0, tanggal = pb.TANGGAL_PEMBUATAN, blanko= b.SERI ,tipe_blanko = b.TIPE};
            return Json(new { data = query },JsonRequestBehavior.AllowGet);
        }

        // GET: PengembalianBlanko/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PengembalianBlanko/Create
        public ActionResult Create()
        {
            ViewBag.Blanko = db.BLANKOes.ToList();
            return View();
        }

        // POST: PengembalianBlanko/Create
        [HttpPost]
        public ActionResult Create(PENGELOLAAN_BLANKO obj)
        {
            try
            {
                db.PENGELOLAAN_BLANKO.Add(obj);
                db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PengembalianBlanko/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PengembalianBlanko/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PengembalianBlanko/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PengembalianBlanko/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
