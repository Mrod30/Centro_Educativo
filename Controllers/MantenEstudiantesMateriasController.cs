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
    public class MantenEstudiantesMateriasController : Controller
    {
        private DBModel db = new DBModel();

        // GET: MantenEstudiantesMaterias
        public ActionResult Index()
        {
            var estudiantesMaterias = db.EstudiantesMaterias.Include(e => e.Estudiantes).Include(e => e.Materias);
            return View(estudiantesMaterias.ToList());
        }

        // GET: MantenEstudiantesMaterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantesMaterias estudiantesMaterias = db.EstudiantesMaterias.Find(id);
            if (estudiantesMaterias == null)
            {
                return HttpNotFound();
            }
            return View(estudiantesMaterias);
        }

        // GET: MantenEstudiantesMaterias/Create
        public ActionResult Create()
        {
            ViewBag.EstudianteID = new SelectList(db.Estudiantes, "EstudianteID", "EstudianteNombre");
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre");
            return View();
        }

        // POST: MantenEstudiantesMaterias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudianteMateriaID,EstudianteID,MateriaID")] EstudiantesMaterias estudiantesMaterias)
        {
            if (ModelState.IsValid)
            {
                db.EstudiantesMaterias.Add(estudiantesMaterias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstudianteID = new SelectList(db.Estudiantes, "EstudianteID", "EstudianteNombre", estudiantesMaterias.EstudianteID);
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", estudiantesMaterias.MateriaID);
            return View(estudiantesMaterias);
        }

        // GET: MantenEstudiantesMaterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantesMaterias estudiantesMaterias = db.EstudiantesMaterias.Find(id);
            if (estudiantesMaterias == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstudianteID = new SelectList(db.Estudiantes, "EstudianteID", "EstudianteNombre", estudiantesMaterias.EstudianteID);
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", estudiantesMaterias.MateriaID);
            return View(estudiantesMaterias);
        }

        // POST: MantenEstudiantesMaterias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudianteMateriaID,EstudianteID,MateriaID")] EstudiantesMaterias estudiantesMaterias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiantesMaterias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstudianteID = new SelectList(db.Estudiantes, "EstudianteID", "EstudianteNombre", estudiantesMaterias.EstudianteID);
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", estudiantesMaterias.MateriaID);
            return View(estudiantesMaterias);
        }

        // GET: MantenEstudiantesMaterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantesMaterias estudiantesMaterias = db.EstudiantesMaterias.Find(id);
            if (estudiantesMaterias == null)
            {
                return HttpNotFound();
            }
            return View(estudiantesMaterias);
        }

        // POST: MantenEstudiantesMaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstudiantesMaterias estudiantesMaterias = db.EstudiantesMaterias.Find(id);
            db.EstudiantesMaterias.Remove(estudiantesMaterias);
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
