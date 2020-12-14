using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;

namespace Controllers
{
    public abstract class AbstractService<T>
    {

        ///plantillas de metodos CRUD para el modelo

        protected OnBreak_Entities em = new OnBreak_Entities();

        public abstract int AddEntity(T entity);

        public abstract int UpdateEntity(T entity);

        public abstract int DeleteEntity(object key);

        public abstract T GetEntity(object key);

        public abstract List<T> GetEntities();



    }
}
