using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenciaBD;
using Controllers;

namespace PruebasUnitarias
{
    [TestClass]
    public class AdministracionContrato
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*Prueba Satisfactoria => probaremos que podemos crear un contrato*/
            ServiceContrato sc = new ServiceContrato();
            Contrato contrato = new Contrato
            {
                Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                Creacion = DateTime.Now,
                Termino = Convert.ToDateTime("01-01-1900"),
                RutCliente = "11111111-1",
                IdModalidad = "CB001",
                IdTipoEvento = 10,
                FechaHoraInicio = Convert.ToDateTime("01-01-2020"),
                FechaHoraTermino = Convert.ToDateTime("02-02-2020"),
                Asistentes = 1,
                PersonalAdicional = 1,
                Realizado = false,
                ValorTotalContrato = 20,
                Observaciones = "Prueba Contrato"

            };
            var resultado = sc.AddEntity(contrato);
            var esperado = 1;
            Assert.AreEqual(resultado, esperado);
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*Prueba Satisfactoria => probaremos que podemos buscar un contrato*/
            ServiceContrato sc = new ServiceContrato();
            string NumeroCotrato = "202007131849";
            sc.GetEntity(NumeroCotrato);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod3()
        {
            /*Prueba NO Satisfactoria => probaremos que podemos no actualizar un contrato no registrado*/
            ServiceContrato sc = new ServiceContrato();
            Contrato contrato = new Contrato
            {
                Numero = "102007131849",
                Creacion = DateTime.Now,
                Termino = Convert.ToDateTime("01-01-1900"),
                RutCliente = "11111111-1",
                IdModalidad = "CB001",
                IdTipoEvento = 10,
                FechaHoraInicio = Convert.ToDateTime("01-01-2020"),
                FechaHoraTermino = Convert.ToDateTime("02-02-2020"),
                Asistentes = 20,
                PersonalAdicional = 20,
                Realizado = false,
                ValorTotalContrato = 20,
                Observaciones = "Prueba Contrato Update"

            };
            var resultado = sc.UpdateEntity(contrato);
            var esperado = 1;
            Assert.AreEqual(resultado, esperado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod4()
        {
            /*Prueba NO Satisfactoria => probaremos que podemos no terminar un contrato no registrado*/
            ServiceContrato sc = new ServiceContrato();
            string numContrato = "10101010";
            Contrato contrato = new Contrato
            {
                Numero = numContrato,
                Creacion = DateTime.Now,
                Termino = DateTime.Now,
                RutCliente = "11111111-1",
                IdModalidad = "CB001",
                IdTipoEvento = 10,
                FechaHoraInicio = Convert.ToDateTime("01-01-2020"),
                FechaHoraTermino = Convert.ToDateTime("02-02-2020"),
                Asistentes = 20,
                PersonalAdicional = 20,
                Realizado = true,
                ValorTotalContrato = 20,
                Observaciones = "Prueba Contrato Update"

            };
            var resultado = sc.UpdateEntity(contrato);
            var esperado = 1;
            Assert.AreEqual(resultado, esperado);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestMethod5()
        {
            /*Prueba NO Satisfactoria => probaremos que no podemos calcular el valor del contrato ingresando un formato invalido*/
            Valorizador val = new Valorizador();
            double valorBase = 3;
            double personalBase = 3;
            double recargoAsistentes = Convert.ToDouble("h");
            double recargoPersonal = 10;
            val.calcularValorEvento(valorBase, recargoAsistentes, recargoPersonal, personalBase);

        }
    }
}
