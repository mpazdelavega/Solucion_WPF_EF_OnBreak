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
using PersistenciaBD;
using Controllers;


namespace GUI
{
    /// <summary>
    /// Lógica de interacción para WPF_AdminContrato.xaml
    /// </summary>
    public partial class WPF_AdminContrato : MetroWindow
    {

        ServiceContrato sc = new ServiceContrato();
        ServiceCliente scl = new ServiceCliente();
        ServiceTipoEvento st = new ServiceTipoEvento();
        ServiceModalidadServicio sm = new ServiceModalidadServicio();
        Valorizador val = new Valorizador();

        public WPF_AdminContrato()
        {
            InitializeComponent();

        }

        private async void RegistrarContrato()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutCliente.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un Rut válido");
                    txtRutCliente.Focus();
                    return;
                }
                if (cmbTipoEvento.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes seleccionar un Tipo de Evento");
                    cmbTipoEvento.Focus();
                    return;
                }
                if (cmbModalidad.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una Modalidad");
                    cmbModalidad.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(Convert.ToString(dtpFechaInicio.SelectedDate)))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una Fecha de inicio");
                    dtpFechaInicio.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(Convert.ToString(dtpFechaTermino1.SelectedDate)))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar una Fecha de Termino");
                    dtpFechaTermino1.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtObservaciones.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar Observaciones");
                    txtObservaciones.Focus();
                    return;
                }
                if (cmbTipoEvento.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un tipo de evento");
                    cmbTipoEvento.Focus();
                    return;
                }

                string txtAs = txtAsistentes.Text.Trim();
                string txtPer = txtPersonalAdicional.Text.Trim();

                if (string.IsNullOrEmpty(txtAs) && string.IsNullOrEmpty(txtPer))
                {
                    txtAsistentes.Text = "0";
                    txtPersonalAdicional.Text = "0";
                }
                else if (string.IsNullOrEmpty(txtPer))
                {
                    txtPersonalAdicional.Text = "0";
                }
                else if (string.IsNullOrEmpty(txtAs))
                {
                    txtAsistentes.Text = "0";
                }
                double valorBaseTipoEvento = 0;
                int personalBase = 0;
                if (cmbTipoEvento.SelectedIndex > -1)
                {
                    ModalidadServicio m = (ModalidadServicio)cmbModalidad.SelectedItem;
                    valorBaseTipoEvento = m.ValorBase;
                    personalBase = m.PersonalBase;
                    cmbModalidad.SelectedValue = m.IdModalidad;
                }
                double recargoAsistentes = double.Parse(txtAsistentes.Text);
                double recargoPersonal = double.Parse(txtPersonalAdicional.Text);

                DateTime FechaActual = DateTime.Now;
                string numeroContrato = FechaActual.ToString("yyyyMMddHHmm");
                DateTime Ftermino = Convert.ToDateTime("01-01-1900");
                string rutCliente = txtRutCliente.Text;
                string Modalidad = (string)cmbModalidad.SelectedValue;
                int TipoEvento = (int)cmbTipoEvento.SelectedValue;
                DateTime Finicio = (DateTime)dtpFechaInicio.SelectedDate;
                DateTime Ftermino2 = (DateTime)dtpFechaTermino1.SelectedDate;
                int asistentes = int.Parse(txtAsistentes.Text);
                int personalAd = int.Parse(txtPersonalAdicional.Text);
                bool realizado = false;
                if (cbRealizado.IsChecked == true)
                {
                    realizado = true;
                }
                else
                {
                    realizado = false;
                }
                double valor = val.calcularValorEvento(valorBaseTipoEvento, recargoAsistentes, recargoPersonal, personalBase);
                string observaciones = txtObservaciones.Text;
                Contrato contrato = new Contrato
                {
                    Numero = numeroContrato,
                    Creacion = FechaActual,
                    Termino = Ftermino,
                    RutCliente = rutCliente,
                    IdModalidad = Modalidad,
                    IdTipoEvento = TipoEvento,
                    FechaHoraInicio = Finicio,
                    FechaHoraTermino = Ftermino2,
                    Asistentes = asistentes,
                    PersonalAdicional = personalAd,
                    Realizado = realizado,
                    ValorTotalContrato = valor,
                    Observaciones = observaciones

                };
                sc.AddEntity(contrato);
                await this.ShowMessageAsync("Correcto:", "Contrato registrado satifactoriamente");
                LimpiarControles();
            }
            catch (FormatException)
            {
                await this.ShowMessageAsync("Error:", "El numero de asistentes y/o personal adicional ingresado no contiene un formato válido");
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }

        private async void ActualizarContrato()
        {
            try
            {
                if (cmbTipoEvento.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un tipo de evento");
                    cmbTipoEvento.Focus();
                    return;
                }

                string txtAs = txtAsistentes.Text.Trim();
                string txtPer = txtPersonalAdicional.Text.Trim();

                if (string.IsNullOrEmpty(txtAs) && string.IsNullOrEmpty(txtPer))
                {
                    txtAsistentes.Text = "0";
                    txtPersonalAdicional.Text = "0";
                    
                }
                else if (string.IsNullOrEmpty(txtPer))
                {
                    txtPersonalAdicional.Text = "0";
                    
                }
                else if (string.IsNullOrEmpty(txtAs))
                {
                    txtAsistentes.Text = "0";
                    
                }
                double valorBaseTipoEvento = 0;
                int personalBase = 0;
                if (cmbTipoEvento.SelectedIndex > -1)
                {
                    ModalidadServicio m = (ModalidadServicio)cmbModalidad.SelectedItem;
                    valorBaseTipoEvento = m.ValorBase;
                    personalBase = m.PersonalBase;
                    cmbModalidad.SelectedValue = m.IdModalidad;
                }
                double recargoAsistentes = double.Parse(txtAsistentes.Text);
                double recargoPersonal = double.Parse(txtPersonalAdicional.Text) + personalBase;
                
                DateTime Fcreacion = (DateTime)dtpFechaCreacion.SelectedDate;
                string numeroContrato = Fcreacion.ToString("yyyyMMddHHmm");
                DateTime Ftermino = (DateTime)dtpFechaTermino.SelectedDate;
                string rutCliente = txtRutCliente.Text;
                string Modalidad = (string)cmbModalidad.SelectedValue;
                int TipoEvento = (int)cmbTipoEvento.SelectedValue;
                DateTime Finicio = (DateTime)dtpFechaInicio.SelectedDate;
                DateTime Ftermino2 = (DateTime)dtpFechaTermino1.SelectedDate;
                int asistentes = int.Parse(txtAsistentes.Text);
                int personalAd = int.Parse(txtPersonalAdicional.Text);
                bool realizado = false;
                if (cbRealizado.IsChecked == true)
                {
                    realizado = true;
                }
                else
                {
                    realizado = false;
                }
                double valor = val.calcularValorEvento(valorBaseTipoEvento, recargoAsistentes, recargoPersonal, personalBase);
                string observaciones = txtObservaciones.Text;
                Contrato contrato = new Contrato
                {
                    Numero = numeroContrato,
                    Creacion = Fcreacion,
                    Termino = Ftermino,
                    RutCliente = rutCliente,
                    IdModalidad = Modalidad,
                    IdTipoEvento = TipoEvento,
                    FechaHoraInicio = Finicio,
                    FechaHoraTermino = Ftermino2,
                    Asistentes = asistentes,
                    PersonalAdicional = personalAd,
                    Realizado = realizado,
                    ValorTotalContrato = valor,
                    Observaciones = observaciones

                };
                sc.UpdateEntity(contrato);
                await this.ShowMessageAsync("Correcto:", "Contrato actualizado satifactoriamente");
                LimpiarControles();
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

        private async void CargarListadoModalidad()
        {
            try
            {
                cmbModalidad.ItemsSource = sm.GetEntities();
                cmbModalidad.SelectedValuePath = "IdModalidad";
                cmbModalidad.DisplayMemberPath = "Nombre";
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void FiltrarTipoEvento()
        {
            try
            {
                List<ModalidadServicio> modalidad = new List<ModalidadServicio>();
                int filtro = Convert.ToInt32(cmbTipoEvento.SelectedValue);
                foreach (ModalidadServicio m in sm.GetEntities())
                {
                    if (m.IdTipoEvento.Equals(filtro))
                    {
                        modalidad.Add(m);
                    }
                }
                cmbModalidad.ItemsSource = modalidad;
                cmbModalidad.SelectedValuePath = "IdModalidad";
                cmbModalidad.DisplayMemberPath = "Nombre";
                cmbModalidad.Items.Refresh();
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
                Cliente cliente = scl.GetEntity(rutCliente);
                if (cliente != null)
                {
                    txtRutCliente.Text = cliente.RutCliente;
                    txtNombreCliente.Text = cliente.NombreContacto;
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
        private async void BuscarContrato()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text))
                {
                    await this.ShowMessageAsync("Error:", "Debes ingresar un Numero de Contrato valido.");
                    txtRutCliente.Focus();
                    return;
                }
                string numeroContrato = txtNumeroContrato.Text;
                Contrato contrato = sc.GetEntity(numeroContrato);
                if (contrato != null)
                {
                    dtpFechaCreacion.SelectedDate = contrato.Creacion;
                    dtpFechaTermino.SelectedDate = contrato.Termino;
                    txtRutCliente.Text = contrato.RutCliente;
                    cmbModalidad.SelectedValue = contrato.IdModalidad;
                    cmbTipoEvento.SelectedValue = contrato.IdTipoEvento;
                    dtpFechaInicio.SelectedDate = contrato.FechaHoraInicio;
                    dtpFechaTermino1.SelectedDate = contrato.FechaHoraTermino;
                    txtAsistentes.Text = contrato.Asistentes.ToString();
                    txtPersonalAdicional.Text = contrato.PersonalAdicional.ToString();
                    cbRealizado.IsChecked = contrato.Realizado;
                    txtObservaciones.Text = contrato.Observaciones;
                    BuscarCliente();
                }
                else
                {
                    await this.ShowMessageAsync("Error:", "No se encuentra el Numero de Contrato buscado.");
                    txtRutCliente.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private void LimpiarControles()
        {
            dtpFechaCreacion.SelectedDate = null;
            dtpFechaTermino.SelectedDate = null;
            txtRutCliente.Text = string.Empty;
            cmbModalidad.SelectedValue = -1;
            cmbTipoEvento.SelectedValue = -1;
            dtpFechaInicio.Text = string.Empty;
            dtpFechaTermino1.Text = string.Empty;
            txtAsistentes.Text = string.Empty;
            txtPersonalAdicional.Text = string.Empty;
            cbRealizado.IsChecked = false;
            txtObservaciones.Text = string.Empty;
            txtNombreCliente.Text = string.Empty;
            txtNumeroContrato.Text = string.Empty;
        }
        public async void CargarDatosContrato(Contrato contrato)
        {
            try
            {
                if (contrato != null)
                {
                    txtNumeroContrato.Text = contrato.Numero;
                    dtpFechaCreacion.SelectedDate = contrato.Creacion;
                    dtpFechaTermino.SelectedDate = contrato.Termino;
                    txtRutCliente.Text = contrato.RutCliente;
                    cmbModalidad.SelectedValue = contrato.IdModalidad;
                    cmbTipoEvento.SelectedValue = contrato.IdTipoEvento;
                    dtpFechaInicio.SelectedDate = contrato.FechaHoraInicio;
                    dtpFechaTermino1.SelectedDate = contrato.FechaHoraTermino;
                    txtAsistentes.Text = contrato.Asistentes.ToString();
                    txtPersonalAdicional.Text = contrato.PersonalAdicional.ToString();
                    cbRealizado.IsChecked = contrato.Realizado;
                    txtObservaciones.Text = contrato.Observaciones;
                    txtNombreCliente.Text = contrato.Cliente.NombreContacto;
                    FiltrarTipoEvento();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
        private async void ValidarFecha()
        {
            if (dtpFechaTermino1.SelectedDate != null)
            {
                DateTime inicio = (DateTime)dtpFechaInicio.SelectedDate;
                DateTime fin = (DateTime)dtpFechaTermino1.SelectedDate;
                if (inicio > fin)
                {
                    await this.ShowMessageAsync("Error:", "Fecha de termino debe ser mayor que la fecha de Inicio");
                    dtpFechaTermino1.Focus();
                    dtpFechaTermino1.SelectedDate = null;
                    return;
                }
            }          
        }
        private async void TerminarContrato()
        {
            try
            {
                BuscarContrato();
                if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text))
                {
                    txtNumeroContrato.Focus();
                    return;
                }
                else
                {
                    MessageDialogResult result = await this.ShowMessageAsync("Pregunta:", "Estas seguro que deseas eliminar el contrato seleccionado?", MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Affirmative)
                    {
                        if (cmbTipoEvento.SelectedIndex == -1)
                        {
                            await this.ShowMessageAsync("Error:", "Debes ingresar un tipo de evento");
                            cmbTipoEvento.Focus();
                            return;
                        }

                        string txtAs = txtAsistentes.Text.Trim();
                        string txtPer = txtPersonalAdicional.Text.Trim();

                        if (string.IsNullOrEmpty(txtAs) && string.IsNullOrEmpty(txtPer))
                        {
                            txtAsistentes.Text = "0";
                            txtPersonalAdicional.Text = "0";
                        }
                        else if (string.IsNullOrEmpty(txtPer))
                        {
                            txtPersonalAdicional.Text = "0";
                        }
                        else if (string.IsNullOrEmpty(txtAs))
                        {
                            txtAsistentes.Text = "0";
                        }
                        double valorBaseTipoEvento = 0;
                        int personalBase = 0;
                        if (cmbTipoEvento.SelectedIndex > -1)
                        {
                            ModalidadServicio m = (ModalidadServicio)cmbModalidad.SelectedItem;

                            valorBaseTipoEvento = m.ValorBase;
                            personalBase = m.PersonalBase;
                            cmbModalidad.SelectedValue = m.IdModalidad;
                        }

                        double recargoAsistentes = double.Parse(txtAsistentes.Text);
                        double recargoPersonal = double.Parse(txtPersonalAdicional.Text) + personalBase;

                        DateTime fechaTermino = DateTime.Now;
                        DateTime fActual = (DateTime)dtpFechaCreacion.SelectedDate;
                        string numeroContrato = txtNumeroContrato.Text;
                        string rutCliente = txtRutCliente.Text;
                        string Modalidad = (string)cmbModalidad.SelectedValue;
                        int TipoEvento = (int)cmbTipoEvento.SelectedValue;
                        DateTime Finicio = (DateTime)dtpFechaInicio.SelectedDate;
                        DateTime Ftermino2 = (DateTime)dtpFechaTermino1.SelectedDate;
                        int asistentes = int.Parse(txtAsistentes.Text);
                        int personalAd = int.Parse(txtPersonalAdicional.Text);
                        bool realizado = false;
                        realizado = true;

                        double valor = val.calcularValorEvento(valorBaseTipoEvento, recargoAsistentes, recargoPersonal, personalBase); ;
                        string observaciones = txtObservaciones.Text;
                        Contrato contrato = new Contrato
                        {
                            Numero = numeroContrato,
                            Creacion = fActual,
                            Termino = fechaTermino,
                            RutCliente = rutCliente,
                            IdModalidad = Modalidad,
                            IdTipoEvento = TipoEvento,
                            FechaHoraInicio = Finicio,
                            FechaHoraTermino = Ftermino2,
                            Asistentes = asistentes,
                            PersonalAdicional = personalAd,
                            Realizado = realizado,
                            ValorTotalContrato = valor,
                            Observaciones = observaciones

                        };
                        sc.UpdateEntity(contrato);
                        await this.ShowMessageAsync("Correcto:", "Contrato terminado satifactoriamente");
                        LimpiarControles();
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error:", "Se ha producido un error inesperado.\n" + ex.Message);
            }
        }
    
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrarContrato();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListadoTipoEvento();
            CargarListadoModalidad();
        }

        private void cmbTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoEvento.IsMouseCaptured)
            {
                FiltrarTipoEvento();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BuscarCliente();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BuscarContrato();
            FiltrarTipoEvento();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ActualizarContrato();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WPF_ListadoContrato wa = new WPF_ListadoContrato();
            wa.Owner = Window.GetWindow(this);
            wa.Owner.Name = "wpf_contratos";
            wa.ShowDialog();
        }

        private void dtpFechaTermino1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarFecha();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            TerminarContrato();
        }

        private void txtAsistentes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtPersonalAdicional_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

       
    }
}
