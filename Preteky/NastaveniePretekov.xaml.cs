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

namespace Preteky
{
    /// <summary>
    /// Interaction logic for NastaveniePretekov.xaml
    /// </summary>
    public partial class NastaveniePretekov : Window
    {
        public CasovacView casovacView { get; set; }
        public PretekyForm pretekyForm { get; set; }
        public SpravcaPretekov spravca { get; set; }
        public NastaveniePretekov(SpravcaPretekov spravcaPretekov )
        {
            InitializeComponent();
            spravca = spravcaPretekov;

        }

        private void OKButton1_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                spravca.PridajPreteky(new SpravaPretekov(dlzkaKolaTextBox.Text, pocetKolTextBox.Text, casovyLimitTextBox.Text));
                spravca.Uloz();
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }

        }
    }
}
