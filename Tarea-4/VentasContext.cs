using System;
using System.Collections.Generic;

namespace Tarea_4
{
    internal class VentasContext
    {
        public IEnumerable<object> Productos { get; internal set; }
        public object Ventas { get; internal set; }
        public IEnumerable<object> Categorias { get; internal set; }

        internal int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}