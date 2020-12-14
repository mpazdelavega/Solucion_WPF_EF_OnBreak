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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tlAdministracionDeClientes_Click(object sender, RoutedEventArgs e)
        {
            WPF_AdminClientes wa = new WPF_AdminClientes();
            wa.Owner = Window.GetWindow(this);
            wa.Owner.Name = "wpf_menu";
            wa.ShowDialog();

        }

        private void tlListadoClientes_Click(object sender, RoutedEventArgs e)
        {
            WPF_ListaClientes wa = new WPF_ListaClientes();
            wa.Owner = Window.GetWindow(this);
            wa.Owner.Name = "wpf_menu";
            wa.ShowDialog();
        }

        private void tlAdminContratos_Click(object sender, RoutedEventArgs e)
        {
            WPF_AdminContrato wa = new WPF_AdminContrato();
            wa.ShowDialog();

        }

        private void tlListadoContratos_Click(object sender, RoutedEventArgs e)
        {
            WPF_ListadoContrato wa = new WPF_ListadoContrato();
            wa.Owner = Window.GetWindow(this);
            wa.Owner.Name = "wpf_menu";
            wa.ShowDialog();
            
        }
    }
}
