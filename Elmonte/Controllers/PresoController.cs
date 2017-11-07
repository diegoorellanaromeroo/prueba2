using Elmonte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elmonte.Controllers
{
    public class PresoController : ApiController
    {
        public int largoString;
        private CarcelDbContext context;

        public PresoController()
        {
            this.context = new CarcelDbContext();
        }

        public IHttpActionResult get(int id)
        {
            Preso preso = context.Presos.Find(id);
            if (preso == null)//404 notfound
            {
                return NotFound();
            }


            return Ok(preso);//retornamos codigo 200 junto con el Preso buscado
        }

        public IEnumerable<Object> get()
        {
            return context.Presos.Include("Condena").Include("CondenaDelito").Include("Delito").Select(c => new
            {
                Id = c.Id,
                Rut = c.Rut,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                FechaNacimiento = c.FechaNacimiento,
                Domicilio = c.Domicilio,
                Sexo = c.Sexo,
                Condena = new
                {
                    Condena = c.Condena
                }
                //Juez = new
                //{
                //    Rut = c.Juez.Rut,
                //    Nombre = c.Juez.Nombre,
                //    Sexo = c.Juez.Sexo,
                //    Domicilio = c.Juez.Domicilio
                //}
            });
        }

        public IHttpActionResult post(Preso preso)
        {
            if (preso == null) return NotFound();//404
            largoString = Int32.Parse(preso.Rut.Length.ToString());
            if (preso.Rut == null || largoString < 9)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "El Rut ingresado es invalido." }); 
            }
            largoString = Int32.Parse(preso.Nombre.Length.ToString());
            if (preso.Nombre == null || largoString < 2)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar un nombre valido." });
            }
            largoString = Int32.Parse(preso.Apellido.Length.ToString());
            if (preso.Apellido == null || largoString < 2)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar un apellido valido." });
            }
            if (preso.FechaNacimiento == null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar una fecha valida." });
            }

            if (preso.Domicilio== null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar una direccion valida." });
            }
            context.Presos.Add(preso);
            int filasAfectadas = context.SaveChanges();
            if (filasAfectadas == 0)
            {
                return InternalServerError();//500
            }
            return Ok(new { Estado = "SUCCESS" , Mensaje = "Agregado correctamente" });

        }

        public IHttpActionResult delete(int id)
        {
            //buscamos el cliente a eliminar
            Preso preso = context.Presos.Find(id);

            if (preso == null) return NotFound();//404

            context.Presos.Remove(preso);

            if (context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Mensaje = "Eliminado correctamente" });
            }

            return InternalServerError();//500

        }
        public IHttpActionResult put(Preso preso)
        {
            context.Entry(preso).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new {Estado = "SUCCESS", Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();



        }
    }
}
