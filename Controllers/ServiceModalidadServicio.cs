using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;

namespace Controllers
{
    public class ServiceModalidadServicio : AbstractService<ModalidadServicio>
    {
        public override int AddEntity(ModalidadServicio entity)
        {
            throw new NotImplementedException();
        }

        public override int DeleteEntity(object key)
        {
            throw new NotImplementedException();
        }

        public override List<ModalidadServicio> GetEntities()
        {
            return em.ModalidadServicio.ToList<ModalidadServicio>();

        }

        public override ModalidadServicio GetEntity(object key)
        {
            return em.ModalidadServicio.Where(ModalidadServicio => ModalidadServicio.IdModalidad == (string)key).FirstOrDefault<ModalidadServicio>();
        }

        public override int UpdateEntity(ModalidadServicio entity)
        {
            throw new NotImplementedException();
        }
    }
}
