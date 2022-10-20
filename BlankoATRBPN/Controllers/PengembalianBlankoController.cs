using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlankoATRBPN.Models;
using BlankoATRBPN.ViewModel;

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
            var query =
               from pb in db.PENGELOLAAN_BLANKO
               join b in db.BLANKOes on pb.BLANKOID equals b.BLANKOID
               join ba in db.BERITA_ACARA on pb.BERITA_ACARA_ID equals ba.BERITA_ACARA_ID
               where pb.TIPE_PROSES_BLANKO_ID == 4 &&
                    pb.PENGELOLAAN_BLANKO_ID == db.PENGELOLAAN_BLANKO.Where(x => x.PENGELOLAAN_BLANKO_ID == pb.PENGELOLAAN_BLANKO_ID).Max(x => x.PENGELOLAAN_BLANKO_ID) // pengembalian //
               select new { pb.PENGELOLAAN_BLANKO_ID, tanggal = pb.TANGGAL_PEMBUATAN, blanko = b.SERI, tipe_blanko = b.TIPE, beritaAcaraFile = ba.FILE_NAME };

            //var getdata = (db.Database.SqlQuery<List<PengembalianBlankoViewModel>>(@"SELECT 
            //            pb1.PENGELOLAAN_BLANKO_ID, 
            //            pb1.TANGGAL_PEMBUATAN AS tanggal,
            //            b.SERI as blanko,
            //            b.TIPE as tipe_blanko,
            //            ba.FILE_NAME as beritaAcaraFile
            //        FROM 
            //            ATRBPN.PENGELOLAAN_BLANKO pb1 LEFT JOIN 
            //            BERITA_ACARA ba ON pb1.BERITA_ACARA_ID = ba.BERITA_ACARA_ID LEFT JOIN 
            //            TIPE_BLANKO tb ON pb1.TIPE_BLANKO_ID = tb.TIPE_BLANKO_ID LEFT JOIN
            //            TIPE_PROSES_BLANKO tpb ON pb1.TIPE_PROSES_BLANKO_ID  = tpb.TIPE_PROSES_BLANKO_ID LEFT JOIN 
            //            STATUS_PENGELOLAAN_BLANKO spb ON pb1.STATUS_PENGELOLAAN_BLANKO_ID = spb.STATUS_PENGELOLAAN_BLANKO_ID LEFT JOIN 
            //            BLANKO b ON pb1.BLANKOID = b.BLANKOID LEFT JOIN 
            //            USERPERORANGAN u ON pb1.USERPERORANGANID = u.USERPERORANGANID 
            //        WHERE 
            //            pb1.TANGGAL_PEMBUATAN = (SELECT MAX(pb2.TANGGAL_PEMBUATAN) FROM ATRBPN.PENGELOLAAN_BLANKO pb2
            //                                      WHERE pb2.BLANKOID = pb1.BLANKOID AND pb2.TIPE_PROSES_BLANKO_ID = 4)")
            //  );


            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
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
                //obj.BERITA_ACARA_ID
                obj.TIPE_BLANKO_ID = blankoType.TIPE_BLANKO_ID;
                obj.TIPE_PROSES_BLANKO_ID = 4;
                obj.STATUS_PENGELOLAAN_BLANKO_ID = 1;
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
                
                //var blanko = db.BLANKOes.Find(obj.BLANKOID);
                //var blankoType = db.TIPE_BLANKO.Where(x => x.TIPE_BLANKO_CODE == blanko.TIPE).FirstOrDefault();
                var newIdTerima = db.PENGELOLAAN_BLANKO.Max(x => x.PENGELOLAAN_BLANKO_ID) + 1;

                PENGELOLAAN_BLANKO newKelola = new PENGELOLAAN_BLANKO();
                var oldKelola = db.PENGELOLAAN_BLANKO.Find(id);

                //obj.PENGELOLAAN_BLANKO_ID = id + 1;

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
                //var oldKelola = db.PENGELOLAAN_BLANKO.Find(id);

                //obj.PENGELOLAAN_BLANKO_ID = id + 1;

                newKelola2.PENGELOLAAN_BLANKO_ID = newIdKembali;
                newKelola2.BERITA_ACARA_ID = oldKelola.BERITA_ACARA_ID;
                newKelola2.TIPE_BLANKO_ID = oldKelola.TIPE_BLANKO_ID;
                newKelola2.TIPE_PROSES_BLANKO_ID = oldKelola.TIPE_PROSES_BLANKO_ID;
                newKelola2.STATUS_PENGELOLAAN_BLANKO_ID = 1;
                newKelola2.TANGGAL_PEMBUATAN = DateTime.Now;
                newKelola2.BLANKOID = oldKelola.BLANKOID;

                db.PENGELOLAAN_BLANKO.Add(newKelola2);
                db.SaveChanges();

                var blanko = db.BLANKOes.Find(oldKelola.BLANKOID);
                //var newIdBlanko = db.BLANKOes.Max(x => x.BLANKOID) + 1;
                var newVersi = db.BLANKOes.Where(x => x.BLANKOID == oldKelola.BLANKOID).Max(x => x.VERSI);
                var newBlanko = mapper.Map<BLANKO>(blanko);
                newBlanko.BLANKOID = Guid.NewGuid().ToString();
                newBlanko.VERSI = newVersi;
                newBlanko.STATUS = "R";
                newBlanko.TANGGALPERUBAHAN = DateTime.Now;
                newBlanko.POSITION = 2;



                return Json(new { message = "success" });
            }
            catch (Exception x)
            {
                return Json(new { message = "failed" });
            }
        }
    }
}
