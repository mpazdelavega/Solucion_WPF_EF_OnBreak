using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenciaBD;
using Controllers;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class ListadoContrato
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Numero de Contrato*/
            ServiceContrato sc = new ServiceContrato();
            string FiltroNumeroContrato = "202007121932";
            List<Contrato> contratos = new List<Contrato>();
            foreach (Contrato c in sc.GetEntities())
            {
                if (c.Numero.ToLower().Contains(FiltroNumeroContrato.ToLower()))
                {
                    contratos.Add(c);
                }
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Rut de Cliente*/
            ServiceContrato sc = new ServiceContrato();
            string FiltroRutCliente = "11111111-1";
            List<Contrato> contratos = new List<Contrato>();
            foreach (Contrato c in sc.GetEntities())
            {
                if (c.RutCliente.ToLower().Contains(FiltroRutCliente.ToLower()))
                {
                    contratos.Add(c);
                }
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            /*Prueba Satisfactoria => probaremos que podemos filtrar datos por Tipo de Contrato*/
            ServiceContrato sc = new ServiceContrato();
            int FiltroTipoContrato = 10;
            List<Contrato> contratos = new List<Contrato>();
            foreach (Contrato c in sc.GetEntities())
            {
                if (c.IdTipoEvento.Equals(FiltroTipoContrato))
                {
                    contratos.Add(c);
                }
            }
        }
    }
}
