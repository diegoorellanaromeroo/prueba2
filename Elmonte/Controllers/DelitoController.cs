using Elmonte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elmonte.Controllers
{
    public class DelitoController : ApiController
    {
        private int largoString;
        private CarcelDbContext context;

        public DelitoController()
        {
            this.context = new CarcelDbContext();
        }
        public IHttpActionResult get(int id)
        {
            Delito delito = context.Delitos.Find(id);

            if (delito == null)//404 notfound
            {
                return NotFound();
            }


            return Ok(delito);//retornamos codigo 200 junto con el Delito buscado
        }
        public IHttpActionResult get()
        {
            List<Delito> delito = context.Delitos.ToList();
            return Ok(delito);//retornamos codigo 200 junto con el Delito buscado
        }

        public IHttpActionResult post(Delito delito)
        {
            if (delito == null) { return NotFound(); }//404 notfound
            largoString = Int32.Parse(delito.Nombre.Length.ToString());
            if (delito.Nombre == null || largoString < 2)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar un nombre valido." });
            }
            if (delito.CondenaMinima <= 0)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar una condena Minima." });
            }
            if (delito.CondenaMaxima<= 0)
            {
                return Ok(new { Estado = "ERROR",  Mensaje = "Debe ingresar una condena Maxima." });
            }
            context.Delitos.Add(delito);
            int filasAfectadas = context.SaveChanges();
            if (filasAfectadas == 0)
            {
                return InternalServerError();
            }
            return Ok(new { Estado = "ERROR", Mensaje = "Agregado Correctamente" });
        }
        public IHttpActionResult delete(int id)
        {
            //buscamos el Delito a eliminar
            Delito delito = context.Delitos.Find(id);

            if (delito == null) return NotFound();//404

            context.Delitos.Remove(delito);

            if (context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Mensaje = "Eliminado correctamente" });
            }

            return InternalServerError();//500

        }

        public IHttpActionResult put(Delito delito)
        {
            context.Entry(delito).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Estado = "SUCCESS", Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();



        }
    }
}
