using Elmonte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elmonte.Controllers
{
    public class JuezController : ApiController
    {
        private CarcelDbContext context;
        private int largoString;

        //public IEnumerable<Object> get()
        //{
        //    return context.Clientes.Include("Categoria").Select(c => new
        //    {
        //        Id = c.ID,
        //        Nombre = c.Nombre,
        //        Apellido = c.Apellido,
        //        Edad = c.Edad,
        //        Categoria = new
        //        {
        //            Nombre = c.Categoria.Nombre
        //        },
        //        Gustos = c.Gustos.Select(g => new
        //        {
        //            ID = g.ID,
        //            Nombre = g.Nombre
        //        })
        //    });
        //}
        public JuezController()
        {
            this.context = new CarcelDbContext();
        }
        //api/Juez/{id}
        public IHttpActionResult get(int id)
        {
            Juez juez = context.Juezes.Find(id);

            if (juez == null)//404 notfound
            {
                return NotFound();
            }


            return Ok(juez);//retornamos codigo 200 junto con el cliente buscado
        }
        public IHttpActionResult get()
        {
            List<Juez> juez = context.Juezes.ToList();
            return Ok(juez);//retornamos codigo 200 junto con el cliente buscado
        }

        //api/Juez
        public IHttpActionResult post(Juez juez)
        {
            if (juez == null) return NotFound();//404
            largoString = Int32.Parse(juez.Nombre.Length.ToString());
            if (largoString < 2 || juez.Nombre == null)
            {
                return Ok(new { Estado = "ERROR", Mensaje ="Debe ingresar un nombre valido."});
            }
            largoString = Int32.Parse(juez.Rut.Length.ToString());
            if (largoString < 2 || juez.Rut== null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar un rut valido." });
            }
            if (juez.Domicilio == null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "Debe ingresar una Direccíón valida." });
            }
            context.Juezes.Add(juez);
            int filasAfectadas = context.SaveChanges();

            if (filasAfectadas == 0)
            {
                return InternalServerError();//500
            }

            return Ok(new { Estado ="SUCCESS", Mensaje = "Agregado correctamente" });

        }
        //api/Juez/{id}
        public IHttpActionResult delete(int id)
        {
            //buscamos el Juez a eliminar
            Juez juez = context.Juezes.Find(id);

            if (juez == null) return NotFound();//404

            context.Juezes.Remove(juez);

            if (context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Mensaje = "Eliminado correctamente" });
            }

            return InternalServerError();//500

        }

        public IHttpActionResult put(Juez juez)
        {
            context.Entry(juez).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();



        }
    }
}
