using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;

namespace Controllers
{
    public class ServiceTipoEvento : AbstractService<TipoEvento>
    {
        public override int AddEntity(TipoEvento entity)
        {
            throw new NotImplementedException();
        }

        public override int DeleteEntity(object key)
        {
            throw new NotImplementedException();
        }

        public override List<TipoEvento> GetEntities()
        {
            return em.TipoEvento.ToList<TipoEvento>();
        }

        public override TipoEvento GetEntity(object key)
        {
            return em.TipoEvento.Where(TipoEvento => TipoEvento.IdTipoEvento == (int)key).FirstOrDefault<TipoEvento>();
        }

        public override int UpdateEntity(TipoEvento entity)
        {
            throw new NotImplementedException();
        }
    }
}
