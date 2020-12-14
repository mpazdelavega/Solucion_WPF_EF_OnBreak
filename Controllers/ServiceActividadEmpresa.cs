using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;



namespace Controllers
{
    public class ServiceActividadEmpresa : AbstractService<ActividadEmpresa>
    {
        public override int AddEntity(ActividadEmpresa entity)
        {
            throw new NotImplementedException();
        }

        public override int DeleteEntity(object key)
        {
            throw new NotImplementedException();
        }

        public override List<ActividadEmpresa> GetEntities()
        {
            return em.ActividadEmpresa.ToList<ActividadEmpresa>();
        }

        public override ActividadEmpresa GetEntity(object key)
        {
            return em.ActividadEmpresa.Where(act => act.IdActividadEmpresa == (int)key).FirstOrDefault<ActividadEmpresa>();
        }

        public override int UpdateEntity(ActividadEmpresa entity)
        {
            throw new NotImplementedException();
        }
    }
}