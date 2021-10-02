using Benner.Microondas.Controle.Controles;
using Benner.Microondas.Interfaces.ViewModel;
using System.Windows;

namespace Benner.Microondas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Start(object sender, StartupEventArgs e)
        {
            var controle = new ProgramasControle();
            controle.IniciarProgramas();

            var principal = new Interfaces.Microondas();
            principal.DataContext = new MicroondasVm(); 
            principal.Show();
        }

        
    }
}
