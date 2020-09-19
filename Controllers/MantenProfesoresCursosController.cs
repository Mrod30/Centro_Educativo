using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Centro_Educativo.Models;

namespace Centro_Educativo.Controllers
{
    public class MantenProfesoresCursosController : Controller
    {
        private DBModel db = new DBModel();

        // GET: MantenProfesoresCursos
        public ActionResult Index()
        {
            var profesoresCursos = db.ProfesoresCursos.Include(p => p.Cursos).Include(p => p.Profesores);
            return View(profesoresCursos.ToList());
        }

        // GET: MantenProfesoresCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            return View(profesoresCursos);
        }

        // GET: MantenProfesoresCursos/Create
        public ActionResult Create()
        {
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre");
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ProfesorID", "ProfesorNombre");
            return View();
        }

        // POST: MantenProfesoresCursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfesorCursoID,ProfesorID,CursoID")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.ProfesoresCursos.Add(profesoresCursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", profesoresCursos.CursoID);
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ProfesorID", "ProfesorNombre", profesoresCursos.ProfesorID);
            return View(profesoresCursos);
        }

        // GET: MantenProfesoresCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", profesoresCursos.CursoID);
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ProfesorID", "ProfesorNombre", profesoresCursos.ProfesorID);
            return View(profesoresCursos);
        }

        // POST: MantenProfesoresCursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfesorCursoID,ProfesorID,CursoID")] ProfesoresCursos profesoresCursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesoresCursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", profesoresCursos.CursoID);
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ProfesorID", "ProfesorNombre", profesoresCursos.ProfesorID);
            return View(profesoresCursos);
        }

        // GET: MantenProfesoresCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            if (profesoresCursos == null)
            {
                return HttpNotFound();
            }
            return View(profesoresCursos);
        }

        // POST: MantenProfesoresCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfesoresCursos profesoresCursos = db.ProfesoresCursos.Find(id);
            db.ProfesoresCursos.Remove(profesoresCursos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
