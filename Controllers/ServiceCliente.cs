using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;


namespace Controllers
{
    public class ServiceCliente : AbstractService<Cliente>
    { 
        public override int AddEntity(Cliente entity)
        {
            Cliente cliente = GetEntity(entity.RutCliente);
            if (cliente == null)
            {
                em.Cliente.Add(entity);
                return em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("No se puede registrar al cliente ya que existe el RUT en nuestros registros");
            }

        }

        public override int DeleteEntity(object key)
        {
            Cliente cliente = GetEntity(key);
            if (cliente != null)
            {
                em.Cliente.Remove(cliente);
                return em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El Cliente no se encuentra en nuestros registros");

            }
        }

        public override List<Cliente> GetEntities()
        {
            return em.Cliente.ToList<Cliente>();
        }

        public override Cliente GetEntity(object key)
        {
            return em.Cliente.Where(cliente => cliente.RutCliente == (String)key).FirstOrDefault<Cliente>();
        }

        public override int UpdateEntity(Cliente entity)
        {
            Cliente cliente = GetEntity(entity.RutCliente);
            if (cliente != null)
            {

            cliente.RazonSocial = entity.RazonSocial;
            cliente.NombreContacto = entity.NombreContacto;
            cliente.MailContacto = entity.MailContacto;
            cliente.Direccion = entity.Direccion;
            cliente.Telefono = entity.Telefono;
            cliente.IdActividadEmpresa = entity.IdActividadEmpresa;
            cliente.IdTipoEmpresa = entity.IdTipoEmpresa;
            return em.SaveChanges();

            }
            else
            {
                throw new ArgumentException("El cliente no se encuentra en nuestros registros");
            }
        }

        
    }
}

