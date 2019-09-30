using System;
using System.Collections.Generic;
using System.IO;
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

namespace Preteky
{
    /// <summary>
    /// Interaction logic for Registracia.xaml
    /// </summary>
    public partial class Registracia : Window
    {
        public Krajiny krajiny { get; set; }
        public SpravcaPretekarov spravcaPretekarov1{ get; set; }
        public Registracia(SpravcaPretekarov spravcaPretekarov )
        {
            spravcaPretekarov1 = spravcaPretekarov;
            krajiny = new Krajiny();
            InitializeComponent();
            DataContext = krajiny;
        }
        
        private void OKButton2_Click(object sender, RoutedEventArgs e)
        {
            string pohlavie;

            if (muzRadioButton.IsChecked == true)
            {
                pohlavie = "Muz";
            }
            else
            {
                pohlavie = "Zena";
            }
            try
            {
                Pretekar pretekar = new Pretekar(menoTextBox.Text, vekTextBox.Text, pohlavie, krajinaComboBox.Text);
                if (pretekar.pohlavie == "Muz")
                {
                    spravcaPretekarov1.PridatMuza(pretekar);
                    spravcaPretekarov1.Uloz(spravcaPretekarov1.zoznamMuzov, spravcaPretekarov1.cestaMuzi);
                    //spravcaPretekarov1.UlozObservable(spravcaPretekarov1.premennyZoznamMuzov, spravcaPretekarov1.cestaPremenniMuzi);
                }
                else
                {
                    spravcaPretekarov1.PridatZenu(pretekar);
                    spravcaPretekarov1.Uloz(spravcaPretekarov1.zoznamZien, spravcaPretekarov1.cestaZeny);
                    //spravcaPretekarov1.UlozObservable(spravcaPretekarov1.premennyZoznamZien, spravcaPretekarov1.cestaPremenneZeny); 
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }
}
