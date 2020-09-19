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
    public class MetenimientoProfesoresController : Controller
    {
        private DBModel db = new DBModel();

        // GET: MetenimientoProfesores
        public ActionResult Index()
        {
            var profesores = db.Profesores.Include(p => p.Materias).Include(p => p.Usuarios);
            return View(profesores.ToList());
        }

        // GET: MetenimientoProfesores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            return View(profesores);
        }

        // GET: MetenimientoProfesores/Create
        public ActionResult Create()
        {
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre");
            return View();
        }

        // POST: MetenimientoProfesores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfesorID,ProfesorNombre,ProfesorApellido,ProfesorTelefono,ProfesorCedula,ProfesorCorreo,UsuarioID,MateriaID")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", profesores.MateriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", profesores.UsuarioID);
            return View(profesores);
        }

        // GET: MetenimientoProfesores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", profesores.MateriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", profesores.UsuarioID);
            return View(profesores);
        }

        // POST: MetenimientoProfesores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfesorID,ProfesorNombre,ProfesorApellido,ProfesorTelefono,ProfesorCedula,ProfesorCorreo,UsuarioID,MateriaID")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MateriaID = new SelectList(db.Materias, "MateriaID", "MateriaNombre", profesores.MateriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", profesores.UsuarioID);
            return View(profesores);
        }

        // GET: MetenimientoProfesores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            return View(profesores);
        }

        // POST: MetenimientoProfesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesores profesores = db.Profesores.Find(id);
            db.Profesores.Remove(profesores);
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
