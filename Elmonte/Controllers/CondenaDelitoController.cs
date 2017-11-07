using Elmonte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elmonte.Controllers
{
    public class CondenaDelitoController : ApiController
    {
        private CarcelDbContext context;


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
        public CondenaDelitoController()
        {
            this.context = new CarcelDbContext();
        }
        //api/CondenaDelito/{id}
        public IHttpActionResult get(int id)
        {
            CondenaDelito CondenaDelito = context.CondenaDelitos.Find(id);

            if (CondenaDelito == null)//404 notfound
            {
                return NotFound();
            }
            return Ok(CondenaDelito);//retornamos codigo 200 junto con el CondenaDelito buscado
        }

        public IHttpActionResult get()
        {
            List<CondenaDelito> CondenaDelito = context.CondenaDelitos.ToList();
            return Ok(CondenaDelito);//retornamos codigo 200 junto con el CondenaDelito buscado
        }

        //api/CondenaDelito
        public IHttpActionResult post(CondenaDelito CondenaDelito)
        {

            context.CondenaDelitos.Add(CondenaDelito);
            int filasAfectadas = context.SaveChanges();

            if (filasAfectadas == 0)
            {
                return InternalServerError();//500
            }

            return Ok(new { Estado = "SUCCESS", Mensaje = "Agregado correctamente" });

        }
        //api/CondenaDelito/{id}
        public IHttpActionResult delete(int id)
        {
            //buscamos la CondenaDelito a eliminar
            CondenaDelito CondenaDelito = context.CondenaDelitos.Find(id);

            if (CondenaDelito == null) return NotFound();//404

            context.CondenaDelitos.Remove(CondenaDelito);

            if (context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Estado = "SUCCESS", Mensaje = "Eliminado correctamente" });
            }
            return InternalServerError();//500
        }

        public IHttpActionResult put(CondenaDelito CondenaDelito)
        {
            context.Entry(CondenaDelito).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();
        }
    }
}
