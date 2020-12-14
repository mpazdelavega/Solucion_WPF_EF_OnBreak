using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using PersistenciaBD;

namespace Controllers
{
    public class ServiceContrato : AbstractService<Contrato>
    {
        public override int AddEntity(Contrato entity)
        {
            Contrato contrato = GetEntity(entity.Numero);
            if(contrato == null)
            {
                em.Contrato.Add(entity);
                return em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El Contrato ya se encuentra registrado");
            }
            
        }

        public override int DeleteEntity(object key)
        {
            Contrato contrato = GetEntity(key);
            if(contrato != null)
            {
                em.Contrato.Remove(contrato);
                return em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El Contrato no se encuentra registrado");

            }
        }

        public override List<Contrato> GetEntities()
        {
            return em.Contrato.ToList<Contrato>();
        }

        public override Contrato GetEntity(object key)
        {
            return em.Contrato.Where(contrato => contrato.Numero == (String)key).FirstOrDefault<Contrato>();
        }

        public override int UpdateEntity(Contrato entity)
        {
            Contrato contrato = GetEntity(entity.Numero);
            if (contrato != null)
            {
                contrato.Numero = entity.Numero;
                contrato.Creacion = entity.Creacion;
                contrato.Termino = entity.Termino;
                contrato.RutCliente = entity.RutCliente;
                contrato.IdModalidad = entity.IdModalidad;
                contrato.IdTipoEvento = entity.IdTipoEvento;
                contrato.FechaHoraInicio = entity.FechaHoraInicio;
                contrato.FechaHoraTermino = entity.FechaHoraTermino;
                contrato.Asistentes = entity.Asistentes;
                contrato.PersonalAdicional = entity.PersonalAdicional;
                contrato.Realizado = entity.Realizado;
                contrato.ValorTotalContrato = entity.ValorTotalContrato;
                contrato.Observaciones = entity.Observaciones;
                return em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("El Contrato no se encuentra en nuestros registros");
            }
        }
    }
}

