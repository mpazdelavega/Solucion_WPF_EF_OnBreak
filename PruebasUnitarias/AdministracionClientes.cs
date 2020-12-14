using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenciaBD;
using Controllers;

namespace PruebasUnitarias
{
    [TestClass]
    public class AdministracionClientes
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*Prueba Satisfactoria => probaremos que podemos registrar un cliente*/
            ServiceCliente sc = new ServiceCliente();
            Cliente cliente = new Cliente
            {
                RutCliente = "11111111-2",
                RazonSocial = "prueba",
                NombreContacto = "prueba",
                MailContacto = "prueba@gmail.com",
                Direccion = "prueba123",
                Telefono = "12345678",
                IdActividadEmpresa = 1,
                IdTipoEmpresa = 10
            };
            var resultado = sc.AddEntity(cliente);
            var esperado = 1;
            Assert.AreEqual(resultado, esperado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod2()
        {
            /*Prueba NO Satisfactoria => probaremos que podemos actualizar un cliente no registrado*/
            ServiceCliente sc = new ServiceCliente();
            Cliente cliente = new Cliente
            {
                RutCliente = "11111111-2",
                RazonSocial = "pruebaUpdate",
                NombreContacto = "pruebaUpdate",
                MailContacto = "pruebaUpdate@gmail.com",
                Direccion = "pruebaUpdate123",
                Telefono = "12345678",
                IdActividadEmpresa = 1,
                IdTipoEmpresa = 10
            };
            var resultado = sc.UpdateEntity(cliente);
            var esperado = 1;
            Assert.AreEqual(resultado, esperado);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void TestMethod3()
        {
            /*Prueba NO Satisfactoria => probaremos que no se puede eliminar un cliente con contratos asociados*/
            ServiceCliente sc = new ServiceCliente();
            string rut_cliente = "11111111-1";
            sc.DeleteEntity(rut_cliente);
        }

        [TestMethod]
        public void TestMethod4()
        {
            /*Prueba Satisfactoria => probaremos que podemos buscar un cliente*/
            ServiceCliente sc = new ServiceCliente();
            string rutCliente = "11111111-1";
            sc.GetEntity(rutCliente);
        }
    }
}
