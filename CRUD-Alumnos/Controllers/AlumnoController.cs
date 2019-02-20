using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Alumnos.Models;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {

                using (var db = new AlumnosContex())
                {
                    /*otra forma de ejecutar querys*/
                    /* var sql = @"
                                 SELECT  a.Id , a.Nombres , a.Apellidos , a.Edad , a.Sexo , a.FechaRegisro , c.Nombre as NombreCiudad 
                                 FROM Alumno a 
                                 INNER JOIN Ciudad c
                                 ON a.CodCiudad = c.Id; 
                                 ";*/


                    var alumnos = from a in db.Alumno
                               join c in db.Ciudad on a.CodCiudad equals c.Id
                               //where a.Edad < 18
                               select new AlumnoCE()
                               {
                                   Id = a.Id,
                                   Nombres = a.Nombres,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   NombreCiudad = c.Nombre,
                                   FechaRegisro = a.FechaRegisro
                               };

                    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    //return View(db.Alumno.ToList());
                    //return View(db.Database.SqlQuery<AlumnoCE>(sql).ToList());
                    return View(alumnos.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener los alumnos" + ex.Message);
                return View();
            }
        }
       
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new AlumnosContex())
                {
                    a.FechaRegisro = DateTime.Now;

                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("" , "Error al registrar alumno" + ex.Message);
                return View();
            }  
        }

        public ActionResult ListCiudad()
        {
            try
            {
                using (var db = new AlumnosContex())
                {
                    return PartialView(db.Ciudad.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar alumno" + ex.Message);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new AlumnosContex())
                {
                    //Alumno al = db.Alumno.Where(a => a.Id == id).FirstOrDefault();
                    Alumno alumno = db.Alumno.Find(id);
                    return View(alumno);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Regcuperar alumno" + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new AlumnosContex())
                {
                    Alumno alumno = db.Alumno.Find(a.Id);
                    alumno.Nombres = a.Nombres;
                    alumno.Apellidos = a.Apellidos;
                    alumno.Edad = a.Edad;
                    alumno.Sexo = a.Sexo;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Editar alumno" + ex.Message);
                return View();
            }
        }

        public ActionResult Detalles(int id)
        {
            try
            {
                using (var db = new AlumnosContex())
                {
                    Alumno alumno = db.Alumno.Find(id);
                    return View(alumno);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al recuperar el detalle alumno" + ex.Message);
                return View();
            }
        }


        public ActionResult Eliminar(int id)
        {
            try
            {
                using (var db = new AlumnosContex())
                {
                    Alumno alumno = db.Alumno.Find(id);
                    db.Alumno.Remove(alumno);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Elinimar el alumno" + ex.Message);
                return View();
            }
        }

        public static string NombreCiudad(int CodCiudad)
        {
            using (var db = new AlumnosContex())
            {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }
    }
}