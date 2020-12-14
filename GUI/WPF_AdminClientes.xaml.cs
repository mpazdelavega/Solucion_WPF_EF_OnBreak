using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Controllers;
using PersistenciaBD;
using System.Text.RegularExpressions;

namespace GUI
{
    /// <summary>
    /// Lógica de interacción para WPF_AdminClientes.xaml
    /// </summary>
    public partial class WPF_AdminClientes : MetroWindow

    {
        ServiceCliente sc = new ServiceCliente();
        ServiceActividadEmpresa ae = new ServiceActividadEmpresa();
        ServiceTipoEmpresa te = new ServiceTipoEmpresa();

        public WPF_AdminClientes()
        {
            InitializeComponent();
        }

        private async void CargarListadoActividad()
        {
            try
            {
                cmbActEmpresa.ItemsSource = ae.GetEntities();
                cmbActEmpresa.SelectedValuePath = "IdActividadEmpresa";
                cmbActEmpresa.DisplayMemberPath = "Descripcion";
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" +ex.Message);
            }
        }
        private void LimpiarControles()
        {
            txtRutCliente.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            CargarListadoActividad();
            CargarListadoTipo();
            cmbActEmpresa.SelectedIndex = -1;
            cmbTipoEmpresa.SelectedIndex = -1;
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        public static bool ValidaRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        private async void RegistrarCliente()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutCliente.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un Rut válido");
                    txtRutCliente.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes rellenar el campo razon social");
                    txtRazonSocial.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombreContacto.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un nombre valido");
                    txtNombreContacto.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un email valido");
                    txtEmail.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una dirección valida");
                    txtDireccion.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un numero de telefono válido");
                    txtTelefono.Focus();
                    return;
                }
                if (cmbActEmpresa.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una actividad");
                    cmbActEmpresa.Focus();
                    return;
                }
                if (cmbTipoEmpresa.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un tipo de empresa");
                    cmbTipoEmpresa.Focus();
                    return;
                }
                string rutCliente = txtRutCliente.Text;
                string razonSocial = txtRazonSocial.Text;
                string nombreContacto = txtNombreContacto.Text;
                string EmailContacto = txtEmail.Text;
                string direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;
                int Idactividadempresa = (int)cmbActEmpresa.SelectedValue;
                int Idtipoempresa = (int)cmbTipoEmpresa.SelectedValue;
                if (ValidaRut(rutCliente) == true)
                {
                    Cliente cliente = new Cliente
                    {
                        RutCliente = rutCliente,
                        RazonSocial = razonSocial,
                        NombreContacto = nombreContacto,
                        MailContacto = EmailContacto,
                        Direccion = direccion,
                        Telefono = telefono,
                        IdActividadEmpresa = Idactividadempresa,
                        IdTipoEmpresa = Idtipoempresa
                    };

                    sc.AddEntity(cliente);
                    await this.ShowMessageAsync("Correcto:", "Cliente registrado satifactoriamente");
                    LimpiarControles();
                    txtRutCliente.Focus();
                }
                else
                {
                    await this.ShowMessageAsync("Error:", "Rut ingresado NO valido.\nFormato valido: 12345678-9");
                    txtRutCliente.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                await this.ShowMessageAsync("Error:", ex.Message);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }

        }

        private async void ActualizarCliente()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutCliente.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un Rut válido");
                    txtRutCliente.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes rellenar el campo razon social");
                    txtRazonSocial.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombreContacto.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un nombre valido");
                    txtNombreContacto.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un email valido");
                    txtEmail.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una dirección valida");
                    txtDireccion.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un numero de telefono válido");
                    txtTelefono.Focus();
                    return;
                }
                if (cmbActEmpresa.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una actividad");
                    cmbActEmpresa.Focus();
                    return;
                }
                if (cmbTipoEmpresa.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un tipo de empresa");
                    cmbTipoEmpresa.Focus();
                    return;
                }
                string rutCliente = txtRutCliente.Text;
                string razonSocial = txtRazonSocial.Text;
                string nombreContacto = txtNombreContacto.Text;
                string EmailContacto = txtEmail.Text;
                string direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;
                int Idactividadempresa = (int)cmbActEmpresa.SelectedValue;
                int Idtipoempresa = (int)cmbTipoEmpresa.SelectedValue;
                Cliente cliente = new Cliente
                {
                    RutCliente = rutCliente,
                    RazonSocial = razonSocial,
                    NombreContacto = nombreContacto,
                    MailContacto = EmailContacto,
                    Direccion = direccion,
                    Telefono = telefono,
                    IdActividadEmpresa = Idactividadempresa,
                    IdTipoEmpresa = Idtipoempresa
                };
                sc.UpdateEntity(cliente);
                await this.ShowMessageAsync("Correcto:", "Cliente actualizado satifactoriamente");
                LimpiarControles();
                txtRutCliente.Focus();
            }
            catch (ArgumentException ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private async void CargarListadoTipo()
        {

            try
            {
                cmbTipoEmpresa.ItemsSource = te.GetEntities();
                cmbTipoEmpresa.SelectedValuePath = "IdTipoEmpresa";
                cmbTipoEmpresa.DisplayMemberPath = "Descripcion";
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private async void BuscarCliente()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutCliente.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un rut valido.");
                    txtRutCliente.Focus();
                    return;
                }
                string rutCliente = txtRutCliente.Text;
                Cliente cliente = sc.GetEntity(rutCliente);
                if (cliente != null)
                {
                    txtRutCliente.Text = cliente.RutCliente;
                    txtRazonSocial.Text = cliente.RazonSocial;
                    txtNombreContacto.Text = cliente.NombreContacto;
                    txtEmail.Text = cliente.MailContacto;
                    txtDireccion.Text = cliente.Direccion;
                    txtTelefono.Text = cliente.Telefono;
                    cmbActEmpresa.SelectedValue = cliente.IdActividadEmpresa;
                    cmbTipoEmpresa.SelectedValue = cliente.IdTipoEmpresa;
                }
                else
                {
                    await this.ShowMessageAsync("Error:", "No se encuentra el rut buscado.");
                    txtRutCliente.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private async void EliminarCliente()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutCliente.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un rut valido.");
                    txtRutCliente.Focus();
                    return;
                }
                MessageDialogResult result = await this.ShowMessageAsync("Pregunta:", "Estas seguro que deseas eliminar el cliente seleccionado?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    string rutCliente = txtRutCliente.Text;
                    sc.DeleteEntity(rutCliente);
                    await this.ShowMessageAsync("Exito:", "Cliente eliminado correctamente.");
                    LimpiarControles();
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                await this.ShowMessageAsync("Error:", "No se puede eliminar al cliente ya que tiene contratos asociados.");
            }
            catch (ArgumentException ex)
            {
                await this.ShowMessageAsync("Error:", ex.Message);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error desconocido.\n" + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BuscarCliente();
            
        }

        private void Administrador_clientes_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListadoActividad();
            CargarListadoTipo();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ActualizarCliente();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EliminarCliente();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WPF_ListaClientes wa = new WPF_ListaClientes();
            wa.Owner = Window.GetWindow(this);
            wa.Owner.Name = "wpf_clientes";
            wa.Show();
        }

        public async void CargarDatosCliente(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    txtRutCliente.Text = cliente.RutCliente;
                    txtRazonSocial.Text = cliente.RazonSocial;
                    txtNombreContacto.Text = cliente.NombreContacto;
                    txtEmail.Text = cliente.MailContacto;
                    txtDireccion.Text = cliente.Direccion;
                    txtTelefono.Text = cliente.Telefono;
                    cmbActEmpresa.SelectedValue = cliente.IdActividadEmpresa;
                    cmbTipoEmpresa.SelectedValue = cliente.IdTipoEmpresa;
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        //Este codigo sirve para validar que se ingresen solo numeros
        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        //Este codigo sirve para validar que se ingresen solo letras
        private void txtNombreContacto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;
        }
    }
}
