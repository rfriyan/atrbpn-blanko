using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlankoATRBPN.Models;
using BlankoATRBPN.ViewModel;
using BlankoATRBPN.Helper;

namespace BlankoATRBPN.Controllers
{

    public class PengembalianBlankoController : Controller
    {
        private Entities db = new Entities();
        private readonly IMapper mapper;
        public PengembalianBlankoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        // GET: PengembalianBlanko
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPengembalian()
        {

            var data = db.VPENGELOLAAN_BLANKO.ToList();

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: PengembalianBlanko/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PengembalianBlanko/Create
        public ActionResult Create()
        {
            var blanckoRList = db.BLANKOes.Where(x => x.STATUS == "R").Select(x => x.SERI).ToList();
            ViewBag.Blanko = db.BLANKOes.Where(x => x.STATUS == "C" && !blanckoRList.Contains(x.SERI)).ToList();
            ViewBag.BeritaAcaraList = db.BERITA_ACARA.ToList();
            return View();
        }

        // POST: PengembalianBlanko/Create
        [HttpPost]
        public ActionResult Create(PENGELOLAAN_BLANKO obj)
        {
            try
            {
                var blanko = db.BLANKOes.Find(obj.BLANKOID);
                var blankoType = db.TIPE_BLANKO.Where(x => x.TIPE_BLANKO_CODE == blanko.TIPE).FirstOrDefault();
                var id = db.PENGELOLAAN_BLANKO.Max(x => x.PENGELOLAAN_BLANKO_ID);
                obj.PENGELOLAAN_BLANKO_ID = id + 1;
                obj.TIPE_BLANKO_ID = blankoType.TIPE_BLANKO_ID;
                obj.TIPE_PROSES_BLANKO_ID = 4;
                obj.STATUS_PENGELOLAAN_BLANKO_ID = 5;
                obj.TANGGAL_PEMBUATAN = DateTime.Now;

                db.PENGELOLAAN_BLANKO.Add(obj);

                db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                return View();
            }
        }

        // GET: PengembalianBlanko/Edit/5
        public ActionResult Edit(int? id)
        {


            ViewBag.Blanko = db.BLANKOes.ToList();
            ViewBag.BeritaAcaraList = db.BERITA_ACARA.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var pengolelolaan = db.PENGELOLAAN_BLANKO.Find(id);
                return View(pengolelolaan);
            }

        }

        // POST: PengembalianBlanko/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PENGELOLAAN_BLANKO obj)
        {
            try
            {
                // TODO: Add update logic here
                var pengelolaanBanko = db.PENGELOLAAN_BLANKO.Find(id);
                obj.BERITA_ACARA_ID = obj.BERITA_ACARA_ID;
                pengelolaanBanko.TIPE_BLANKO_ID = obj.TIPE_BLANKO_ID;
                pengelolaanBanko.TIPE_PROSES_BLANKO_ID = obj.TIPE_PROSES_BLANKO_ID;
                pengelolaanBanko.STATUS_PENGELOLAAN_BLANKO_ID = obj.STATUS_PENGELOLAAN_BLANKO_ID;
                pengelolaanBanko.TANGGAL_PEMBUATAN = obj.TANGGAL_PEMBUATAN;

                db.SaveChanges();



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

        public ActionResult Approval()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ApprovePengembalian(int id)
        {
            try
            {
                var newIdTerima = db.PENGELOLAAN_BLANKO.Max(x => x.PENGELOLAAN_BLANKO_ID) + 1;

                PENGELOLAAN_BLANKO newKelola = new PENGELOLAAN_BLANKO();
                var oldKelola = db.PENGELOLAAN_BLANKO.Find(id);

                newKelola.PENGELOLAAN_BLANKO_ID = newIdTerima;
                newKelola.BERITA_ACARA_ID = oldKelola.BERITA_ACARA_ID;
                newKelola.TIPE_BLANKO_ID = oldKelola.TIPE_BLANKO_ID;
                newKelola.TIPE_PROSES_BLANKO_ID = 4;
                newKelola.STATUS_PENGELOLAAN_BLANKO_ID = 1;
                newKelola.TANGGAL_PEMBUATAN = DateTime.Now;
                newKelola.BLANKOID = oldKelola.BLANKOID;
                db.PENGELOLAAN_BLANKO.Add(newKelola);
                db.SaveChanges();



                var newIdKembali = db.PENGELOLAAN_BLANKO.Max(x => x.PENGELOLAAN_BLANKO_ID) + 1;

                PENGELOLAAN_BLANKO newKelola2 = new PENGELOLAAN_BLANKO();
                
                newKelola2.PENGELOLAAN_BLANKO_ID = newIdKembali;
                newKelola2.BERITA_ACARA_ID = oldKelola.BERITA_ACARA_ID;
                newKelola2.TIPE_BLANKO_ID = oldKelola.TIPE_BLANKO_ID;
                newKelola2.TIPE_PROSES_BLANKO_ID = oldKelola.TIPE_PROSES_BLANKO_ID;
                newKelola2.STATUS_PENGELOLAAN_BLANKO_ID = 1;
                newKelola2.TANGGAL_PEMBUATAN = DateTime.Now;
                newKelola2.BLANKOID = oldKelola.BLANKOID;

                db.PENGELOLAAN_BLANKO.Add(newKelola2);
                db.SaveChanges();

                var blanko = (BLANKO)db.BLANKOes.AsNoTracking().Where(x=>x.BLANKOID == oldKelola.BLANKOID).FirstOrDefault();
                var newVersi = db.BLANKOes.Where(x => x.BLANKOID == oldKelola.BLANKOID).Max(x => x.VERSI);
                var newBlanko = mapper.Map<BLANKO>(blanko);
                newBlanko.BLANKOID = Utils.DotNetToOracle(Guid.NewGuid().ToString());
                newBlanko.VERSI = newVersi + 1;
                newBlanko.STATUS = "R";
                newBlanko.TANGGALPERUBAHAN = DateTime.Now;
                newBlanko.POSITION = 2;
                
                db.BLANKOes.Add(newBlanko);
                db.SaveChanges();

                return Json(new { message = "success" });
            }
            catch (DbEntityValidationException e)
            {
                return Json(new { message = "failed" });
            }
        }
    }
}
