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
    /// Lógica de interacción para WPF_ListadoContrato.xaml
    /// </summary>
    public partial class WPF_ListadoContrato : MetroWindow
    {

        ServiceContrato sc = new ServiceContrato();
        ServiceTipoEvento st = new ServiceTipoEvento();
        public WPF_ListadoContrato()
        {
            InitializeComponent();
        }
        private async void FiltrarDatosContrato()
        {
            try
            {

                string filtro = txtTextoFiltro.Text;
                List<Contrato> contratos = new List<Contrato>();

                foreach (Contrato c in sc.GetEntities())
                {
                    if (c.Numero.ToLower().Contains(filtro.ToLower()))
                    {
                        contratos.Add(c);
                    }
                }

                dtgListadoContratos.ItemsSource = contratos;
                dtgListadoContratos.Items.Refresh();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void CargarListadoContratos()
        {
            try
            {
                dtgListadoContratos.ItemsSource = sc.GetEntities();
                dtgListadoContratos.Items.Refresh();


            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void FiltrarDatosTipoEvento()
        {
            try
            {
                List<Contrato> contratos = new List<Contrato>();
                if (cmbTipoEvento.SelectedItem != null)
                {
                    int filtro = (int)cmbTipoEvento.SelectedValue;
                    foreach (Contrato c in sc.GetEntities())
                    {

                        if (c.IdTipoEvento.Equals(filtro))
                        {
                            contratos.Add(c);
                        }
                    }
                    dtgListadoContratos.ItemsSource = contratos;
                    dtgListadoContratos.Items.Refresh();

                }

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void RefreshTipoEvento()
        {
            try
            {
                if (cmbTipoEvento.SelectedItem == null || cmbTipoEvento.SelectedIndex <= -1)
                {
                    CargarListadoContratos();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void CargarListadoTipoEvento()
        {
            try
            {
                cmbTipoEvento.ItemsSource = st.GetEntities();
                cmbTipoEvento.SelectedValuePath = "IdTipoEvento";
                cmbTipoEvento.DisplayMemberPath = "Descripcion";
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void FiltrarDatosRutEmpresa()
        {
            try
            {
                string filtroPorRut = txtFiltroRut.Text;
                List<Contrato> contratos = new List<Contrato>();
                if (txtFiltroRut.Text.Equals(filtroPorRut))
                {
                    foreach (Contrato c in sc.GetEntities())
                    {
                        if (c.RutCliente.ToLower().Contains(filtroPorRut.ToLower()))
                        {
                            contratos.Add(c);
                        }
                    }
                }
                dtgListadoContratos.ItemsSource = contratos;
                dtgListadoContratos.Items.Refresh();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void CargarDatosContrato()
        {
            try
            {
                if (dtgListadoContratos.SelectedIndex > -1)
                {
                    string parent = this.Owner.Name;
                    int index = dtgListadoContratos.SelectedIndex;
                    Contrato contrato = (Contrato)dtgListadoContratos.SelectedItem;
                    if (parent == "wpf_menu")
                    {
                        WPF_AdminContrato ac = new WPF_AdminContrato();
                        ac.Show();
                        ac.CargarDatosContrato(contrato);
                    }
                    else if (parent == "wpf_contratos")
                    {
                        WPF_AdminContrato ac = (WPF_AdminContrato)this.Owner;
                        ac.Show();
                        ac.CargarDatosContrato(contrato);
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
            FiltrarDatosContrato();
        }

   
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListadoContratos();
            CargarListadoTipoEvento();
            
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            RefreshTipoEvento();
            FiltrarDatosTipoEvento();
        }

        private void txtFiltroRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarDatosRutEmpresa();
        }

        private void MetroWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //CargarListadoContratos();
            //CargarDatosContrato();
        }

        private void dtgListadoContratos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CargarListadoContratos();
            CargarDatosContrato();
            dtgListadoContratos.SelectedIndex = -1;
        }
    }
}
