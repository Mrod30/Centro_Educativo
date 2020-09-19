using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Centro_Educativo.Models;

namespace Centro_Educativo.Controllers
{
    public class UsuariosController : Controller
    {
        DBModel db = new DBModel();
        // GET: Usuarios
        public ActionResult CrearUsuario()
        {
            return View();
        }

        public ActionResult IniciarSesion()
        {
            ViewBag.Admin = "Admin";
            return View();
        }

        public JsonResult ValidateLogin(Usuarios usuarios)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var value = false;
            var Validar = (from s in db.Usuarios 
                           where usuarios.UsuarioNombre.Equals(s.UsuarioNombre)
                           && usuarios.UsuarioPassword.Equals(s.UsuarioPassword) select s.UsuarioID).FirstOrDefault();
            if (Validar > 0)
            {
                value = true;
                Session["UsuarioNombre"] = usuarios.UsuarioNombre.ToString();
                return Json(value, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Logout()
        {
            Session["UsuarioNombre"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public JsonResult getCrearUsuarioEstudiante(Estudiantes estudiantes)
        {
            var value = false;
            Usuarios usuarios = new Usuarios();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                
                usuarios.UsuarioNombre = estudiantes.EstudianteCorreo;
                usuarios.UsuarioPassword = "123456789";
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                value = true;
            }
            catch (Exception)
            {

                throw;
            }
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCrearEstudiante(Estudiantes estudiantes)
        {
            var value = false;
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var UserID = (from s in db.Usuarios
                              where s.UsuarioNombre == estudiantes.EstudianteCorreo
                              select s.UsuarioID).FirstOrDefault();
                estudiantes.UsuarioID = UserID;
                db.Estudiantes.Add(estudiantes);
                db.SaveChanges();
                value = true;
            }
            catch (Exception)
            {

                throw;
            }
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCursos()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var lstCursos = db.Cursos.ToList();

            return Json(lstCursos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMaterias()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var lstMaterias = db.Materias.ToList();

            return Json(lstMaterias, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getCrearUsuarioProfesor(Profesores profesores)
        {
            var value = false;
            Usuarios usuarios = new Usuarios();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                usuarios.UsuarioNombre = profesores.ProfesorCorreo;
                usuarios.UsuarioPassword = profesores.ProfesorCedula;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                value = true;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCrearProfesor(Profesores profesores)
        {
            var value = false;
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var UserID = (from s in db.Usuarios
                              where s.UsuarioNombre == profesores.ProfesorCorreo
                              select s.UsuarioID).FirstOrDefault();
                profesores.UsuarioID = UserID;
                db.Profesores.Add(profesores);
                db.SaveChanges();
                value = true;
            }
            catch (Exception)
            {

                throw;
            }
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}