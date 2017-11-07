using Elmonte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elmonte.Controllers
{
    public class CondenaController : ApiController
    {
        private CarcelDbContext context;


        public IEnumerable<Object> get()
        {
            return context.Condenas.Include("Preso").Include("Juez").Select(c => new
            {
                Id = c.Id,
                FechaInicioCondena = c.FechaInicioCondena,
                FechaCondena = c.FechaCondena,
                Preso = new
                {
                    Rut = c.Preso.Rut,
                    Nombre = c.Preso.Nombre,
                    Apellido = c.Preso.Apellido,
                    FechaNacimiento = c.Preso.FechaNacimiento,
                    Domicilio = c.Preso.Domicilio,
                    Sexo = c.Preso.Sexo
                },
                Juez = new
                {
                    Rut = c.Juez.Rut,
                    Nombre = c.Juez.Nombre,
                    Sexo = c.Juez.Sexo,
                    Domicilio = c.Juez.Domicilio
                }
            });
        }

        public CondenaController()
        {
            this.context = new CarcelDbContext();
        }
        //api/Condena/{id}
        public IHttpActionResult get(int id)
        {
            Condena Condena = context.Condenas.Find(id);

            if (Condena == null)//404 notfound
            {
                return NotFound();
            }
            return Ok(Condena);//retornamos codigo 200 junto con el cliente buscado
        }

        //public IHttpActionResult get()
        //{
        //    List<Condena> Condena = context.Condenas.ToList();
        //    return Ok(Condena);//retornamos codigo 200 junto con el cliente buscado
        //}

        //api/Condena
        public IHttpActionResult post(Condena Condena)
        {
            Condena.CondenaDelito.Cast<CondenaDelito>().ToArray();
            return Ok(new { Condena });
            Preso preso = context.Presos.Find(Condena.PresoId);
            Juez juez = context.Juezes.Find(Condena.JuezId);
            if (Condena == null) return NotFound();//404
            if (preso == null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "El Preso ingresado no existe, vuelva a intentarlo." });
            }
            if (juez == null)
            {
                return Ok(new { Estado = "ERROR", Mensaje = "El Juez ingresado no existe, vuelva a intentarlo." });
            }
            
            //context.CondenaDelitos.Add(Condena.CondenaDelito);
            context.Condenas.Add(Condena);
            int filasAfectadas = context.SaveChanges();
            if (filasAfectadas == 0)
            {
                return InternalServerError();//500
            }
            return Ok(new { mensaje = "Agregado correctamente" });
        }
        //api/Condenas/{id}
        public IHttpActionResult delete(int id)
        {
            //buscamos la Condena a eliminar
            Condena Condena = context.Condenas.Find(id);
            if (Condena == null) return NotFound();//404
            context.Condenas.Remove(Condena);

            if (context.SaveChanges() > 0)
            {
                //retornamos codigo 200
                return Ok(new { Mensaje = "Eliminado correctamente" });
            }
            return InternalServerError();//500
        }

        public IHttpActionResult put(Condena Condena)
        {
            context.Entry(Condena).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Modificado correctamente" });
            }

            return InternalServerError();
        }
    }
}
