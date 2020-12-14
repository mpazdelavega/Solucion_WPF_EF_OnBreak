using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;

namespace Controllers
{
    public class ServiceTipoEmpresa : AbstractService<TipoEmpresa>
    {
        public override int AddEntity(TipoEmpresa entity)
        {
            throw new NotImplementedException();
        }

        public override int DeleteEntity(object key)
        {
            throw new NotImplementedException();
        }

        public override List<TipoEmpresa> GetEntities()
        {
            return em.TipoEmpresa.ToList<TipoEmpresa>();
        }

        public override TipoEmpresa GetEntity(object key)
        {
            return em.TipoEmpresa.Where(te => te.IdTipoEmpresa == (int)key).FirstOrDefault<TipoEmpresa>();
        }

        public override int UpdateEntity(TipoEmpresa entity)
        {
            throw new NotImplementedException();
        }
    }
}
