using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenciaBD;
using Controllers;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class ListaClientes
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Rut*/
            ServiceCliente sc = new ServiceCliente();
            string FiltroRut = "11111111-1";
            List<Cliente> clientes = new List<Cliente>();
            foreach (Cliente c in sc.GetEntities())
            {
                if (c.RutCliente.ToLower().Contains(FiltroRut.ToLower()))
                {
                    clientes.Add(c);
                }
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Tipo Empresa*/
            ServiceCliente sc = new ServiceCliente();
            int FiltroTipoEmpresa = 10;
            List<Cliente> clientes = new List<Cliente>();
            foreach (Cliente c in sc.GetEntities())
            {
                if (c.IdTipoEmpresa.Equals(FiltroTipoEmpresa))
                {
                    clientes.Add(c);
                }
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Actividad*/
            ServiceCliente sc = new ServiceCliente();
            int FiltroActividad = 1;
            List<Cliente> clientes = new List<Cliente>();
            foreach (Cliente c in sc.GetEntities())
            {
                if (c.IdActividadEmpresa.Equals(FiltroActividad))
                {
                    clientes.Add(c);
                }
            }
        }
    }
}
