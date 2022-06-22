using OrdenesDeServicio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace OrdenesDeServicio.DataAccess
{
    public class OrdenRepository
    {
        private dbOrdenesEntities ordenContext = null;

        public OrdenRepository()
        {
            ordenContext = new dbOrdenesEntities();
        }

        public ordenServicio Get(int id)
        {
            return ordenContext.ordenServicio.Find(id);
        }

        public List<ordenServicio> GetAll()
        {
            return ordenContext.ordenServicio.ToList();
        }

        public void AddOrden(ordenServicio orden)
        {
            if (orden != null)
            {
                ordenContext.ordenServicio.Add(orden);
                ordenContext.SaveChanges();
            }
        }

        public void UpdateOrden(ordenServicio orden)
        {
            var ordenFind = this.Get(orden.id);
            if (ordenFind != null)
            {
                ordenFind.nombre = orden.nombre;
                ordenFind.fecha = orden.fecha;
                ordenFind.descripcion = orden.descripcion;
                ordenFind.estatus = orden.estatus;
                ordenContext.SaveChanges();
            }
        }

        public void RemoveOrden(int id)
        {
            var ordenObj = ordenContext.ordenServicio.Find(id);
            if (ordenObj != null)
            {
                ordenContext.ordenServicio.Remove(ordenObj);
                ordenContext.SaveChanges();
            }
        }
    }
}
