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
    public class MantenimientoEstudiantesController : Controller
    {
        private DBModel db = new DBModel();

        // GET: MantenimientoEstudiantes
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.Include(e => e.Cursos).Include(e => e.Usuarios);
            return View(estudiantes.ToList());
        }

        // GET: MantenimientoEstudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }

        // GET: MantenimientoEstudiantes/Create
        public ActionResult Create()
        {
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre");
            return View();
        }

        // POST: MantenimientoEstudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudianteID,EstudianteNombre,EstudianteApellido,EstudianteDireccion,EstudianteTelefono,EstudianteCorreo,UsuarioID,CursoID")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", estudiantes.CursoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", estudiantes.UsuarioID);
            return View(estudiantes);
        }

        // GET: MantenimientoEstudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", estudiantes.CursoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", estudiantes.UsuarioID);
            return View(estudiantes);
        }

        // POST: MantenimientoEstudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudianteID,EstudianteNombre,EstudianteApellido,EstudianteDireccion,EstudianteTelefono,EstudianteCorreo,UsuarioID,CursoID")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "CursoNombre", estudiantes.CursoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", estudiantes.UsuarioID);
            return View(estudiantes);
        }

        // GET: MantenimientoEstudiantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }

        // POST: MantenimientoEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiantes);
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
