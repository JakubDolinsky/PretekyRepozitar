using Microsoft.Win32;
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

namespace Preteky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public PretekyForm pretekyForm { get; set; }
        public SpravcaPretekov spravcaPretekov { get; set; }
        public SpravcaPretekarov spravcaPretekarov { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            spravcaPretekov = new SpravcaPretekov();
            spravcaPretekarov = new SpravcaPretekarov(spravcaPretekov );
            try
            {
                spravcaPretekarov.NacitajZeny();
                

                spravcaPretekarov.ZoradZoznam(spravcaPretekarov.zoznamZien, spravcaPretekarov.premennyZoznamZien);
                spravcaPretekarov.NacitajMuzi();
                //spravcaPretekarov.NacitajObservableMuzi();
                spravcaPretekarov.ZoradZoznam(spravcaPretekarov.zoznamMuzov, spravcaPretekarov.premennyZoznamMuzov);
                spravcaPretekov.Nacitaj();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void NastavenieMenu_Click(object sender, RoutedEventArgs e)
        {

            NastaveniePretekov nastaveniePretekov = new NastaveniePretekov(spravcaPretekov);
            nastaveniePretekov.ShowDialog();
        }

        private void PriebehPretekovMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //spravcaPretekov.Nacitaj();
                if (spravcaPretekov.zoznamPretekov[0] == null)
                {
                    throw new ArgumentException("Chybajuce parametre pretekov!");
                }
                else
                {
                    pretekyForm = new PretekyForm(spravcaPretekov, spravcaPretekarov);
                    pretekyForm.ShowDialog();
                    
                    //foreach (Pretekar i in spravcaPretekarov.zoznamPretekarov)
                    //{
                    //    spravcaPretekarov.premennyZoznamPretekarov.Add(i);
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RegistraciaMenu_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia(spravcaPretekarov);
            registracia.ShowDialog();
        }

        private void ExportVysledok_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    spravcaPretekarov.Export(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Subor sa nepodarilo ulozit.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void AnulujMenu_Click(object sender, RoutedEventArgs e)
        {
            spravcaPretekarov.Reset();
            spravcaPretekarov.Uloz(spravcaPretekarov.zoznamMuzov, spravcaPretekarov.cestaMuzi);
            spravcaPretekarov.Uloz(spravcaPretekarov.zoznamZien, spravcaPretekarov.cestaZeny);
            spravcaPretekov.Uloz();
        }
    }
}
