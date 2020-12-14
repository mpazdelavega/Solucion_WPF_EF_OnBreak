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
using Controllers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PersistenciaBD;

namespace GUI
{
    /// <summary>
    /// Lógica de interacción para WPF_ListaClientes.xaml
    /// </summary>
    public partial class WPF_ListaClientes : MetroWindow
    {
        ServiceCliente sc = new ServiceCliente();
        ServiceActividadEmpresa ae = new ServiceActividadEmpresa();
        ServiceTipoEmpresa te = new ServiceTipoEmpresa();

        public WPF_ListaClientes()
        {
            InitializeComponent();
        }
    

        private void dtgListadoClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void CargarListadoClientes()
        {
            try
            {
                dtgListadoClientes.ItemsSource = sc.GetEntities();
                dtgListadoClientes.Items.Refresh();
                

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
      


        private async void FiltrarDatosCliente()
        {
            try
            {

                string filtro = txtTextoFiltro.Text;
                List<Cliente> clientes = new List<Cliente>();

                foreach (Cliente c in sc.GetEntities())
                {
                    if (c.RutCliente.ToLower().Contains(filtro.ToLower()))
                    {
                        clientes.Add(c);
                    }
                }

                dtgListadoClientes.ItemsSource = clientes;
                dtgListadoClientes.Items.Refresh();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void CargarDatosCliente()
        {
            try
            {
                if (dtgListadoClientes.SelectedIndex > -1)
                {
                    string parent = this.Owner.Name;
                    int index = dtgListadoClientes.SelectedIndex;
                    Cliente cliente = (Cliente)dtgListadoClientes.SelectedItem;
                    if (parent == "wpf_menu")
                    {
                        WPF_AdminClientes ac = new WPF_AdminClientes();
                        ac.Show();
                        ac.CargarDatosCliente(cliente);
                    }
                    else if (parent == "wpf_clientes")
                    {
                        WPF_AdminClientes ac = (WPF_AdminClientes)this.Owner;
                        ac.Show();
                        ac.CargarDatosCliente(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarDatosCliente();
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

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            CargarListadoClientes();
            
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListadoClientes();
            CargarListadoTipo();
            CargarListadoActividad();
            
        }
        private async void FiltrarDatosClienteTipoEmp()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                if (cmbTipoEmpresa.SelectedItem != null)
                {
                    int filtro = (int)cmbTipoEmpresa.SelectedValue;
                    foreach (Cliente c in sc.GetEntities())
                    {

                        if (c.IdTipoEmpresa.Equals(filtro))
                        {
                            clientes.Add(c);
                        }
                    }
                    dtgListadoClientes.ItemsSource = clientes;
                    dtgListadoClientes.Items.Refresh();

                }

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void FiltrarDatosActividadEmpresa()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                if (cmbActEmpresa.SelectedItem != null)
                {
                    int filtro = (int)cmbActEmpresa.SelectedValue;
                    foreach (Cliente c in sc.GetEntities())
                    {
                        if (c.IdActividadEmpresa.Equals(filtro))
                        {
                            clientes.Add(c);

                        }
                    }
                }
                dtgListadoClientes.ItemsSource = clientes;
                dtgListadoClientes.Items.Refresh();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarDatosActividadEmpresa();
            RefreshActEmpresa();



        }
        private async void RefreshTipoEmpresa()
        {
            try
            {
                if (cmbTipoEmpresa.SelectedItem == null || cmbTipoEmpresa.SelectedIndex <= -1)
                {
                    CargarListadoClientes();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private async void RefreshActEmpresa()
        {
            try
            {
                if (cmbActEmpresa.SelectedItem == null || cmbActEmpresa.SelectedIndex <= -1)
                {
                    CargarListadoClientes();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            FiltrarDatosClienteTipoEmp();
            RefreshTipoEmpresa();

        }

        private void MetroWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //CargarDatosCliente();
            
        }

        private void dtgListadoClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CargarDatosCliente();
            dtgListadoClientes.SelectedIndex = -1;
        }
    }
}
