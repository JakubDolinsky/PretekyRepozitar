using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Preteky.xaml
    /// </summary>
    public partial class PretekyForm : Window
    {

        public CasovacView casovacView { get; set; }
        public UzivateliaCasovacData uzivateliaCasovacData { get; set; }
        public SpravcaPretekarov spravca { get; set; }
        public SpravcaPretekov spravcaPretekov1 { get; set; }
        private TimeSpan _limit;


        public PretekyForm(SpravcaPretekov spravcaPretekov, SpravcaPretekarov spravcaPretekarov)
        {
            //uzivateliaCasovacData = null;
            InitializeComponent();
            spravca = spravcaPretekarov;
            spravcaPretekov1 = spravcaPretekov;
            casovacView = new CasovacView(spravcaPretekov1);
            uzivateliaCasovacData = new UzivateliaCasovacData(casovacView, spravca, spravcaPretekov.zoznamPretekov[0]);
            DataContext = uzivateliaCasovacData;
            casovacView.VynulovanyCasovac += HlasenieOVynulovani;
            _limit  = TimeSpan.Parse(spravcaPretekov1.zoznamPretekov[0].casovyLimit); 
            koniecButton.IsEnabled = false;
            pridajKoloMuzoviButton.IsEnabled = false;
            pridajKoloZenaButton.IsEnabled = false;
            if (_limit == new TimeSpan (0,0,0,0))
            {
                startButton.IsEnabled = false; 
            }
        }

        private void HlasenieOVynulovani(object sender, EventArgs e)
        {
            casovac.Text = "0:0:0,0";
            spravcaPretekov1.zoznamPretekov[0].zadajKoniecPretekov();
            spravcaPretekov1.Uloz();
            koniecButton.IsEnabled = false;
            pridajKoloMuzoviButton.IsEnabled = false;
            pridajKoloZenaButton.IsEnabled = false;
            startButton.IsEnabled = false;
            MessageBox.Show("Casovy limit uplynul!");

        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            casovacView.Spust();
            spravcaPretekov1.zoznamPretekov[0].zadajZaciatokPretekov();
            spravcaPretekov1.Uloz();
            if (casovac.Text != "0:0:0,0")
            {
                koniecButton.IsEnabled = true;
                pridajKoloMuzoviButton.IsEnabled = true;
                pridajKoloZenaButton.IsEnabled = true;
            }
            else
            {
                startButton.IsEnabled = false;
            }
        }

        private void KoniecButton_Click(object sender, RoutedEventArgs e)
        {
            if (casovac.Text != "0:0:0,0")
            {
                casovacView.Zastav();
                spravcaPretekov1.zoznamPretekov[0].zadajKoniecPretekov();
                spravcaPretekov1.Uloz();
                koniecButton.IsEnabled = false;
                pridajKoloMuzoviButton.IsEnabled = false;
                pridajKoloZenaButton.IsEnabled = false;
                MessageBox.Show("Preteky boli ukoncene!");

            }
        }

        private void PretekyForm_FormClosing(object sender, CancelEventArgs e)
        {
            if (casovacView.stopwatch.IsRunning)
            {
                string varovanie = "Zatvorenim okna sa zastavi casomiera pretekov. Chcete zavriet okno?";
                MessageBoxResult result = MessageBox.Show(varovanie, "Varovanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                casovacView.Zastav();
                spravcaPretekov1.zoznamPretekov[0].zadajKoniecPretekov();
                
            }
            if (casovac.Text == "0:0:0,0")
            {
                _limit = new TimeSpan(0, 0, 0, 0, 0);
                spravcaPretekov1.zoznamPretekov[0].casovyLimit = _limit.ToString();
                
            }
            else
            {
                _limit = _limit - casovacView.stopwatch.Elapsed;
                spravcaPretekov1.zoznamPretekov[0].casovyLimit = _limit.ToString();
            }
            spravcaPretekov1.Uloz();
        }
        private void PridajKoloMuzoviButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pretekar bezec = (Pretekar)muziListBox.SelectedItem;
                spravcaPretekov1.zoznamPretekov[0].PridatKolo(bezec);
                spravcaPretekov1.zoznamPretekov[0].UrcenieCasuPoslednehoKola(bezec, casovacView.stopwatch.Elapsed);
                spravcaPretekov1.zoznamPretekov[0].PriemernaRychlost(bezec);
                spravca.ZoradZoznam(spravca.zoznamMuzov, spravca.premennyZoznamMuzov);
                spravcaPretekov1.zoznamPretekov[0].UrciPoradie(bezec, spravca.premennyZoznamMuzov);
                muziListBox.SelectedItem = bezec;
                spravca.Uloz(spravca.zoznamMuzov, spravca.cestaMuzi);
                //spravca.UlozObservable(spravca.premennyZoznamMuzov, spravca.cestaPremenniMuzi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void PridajKoloZenaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pretekar bezec = (Pretekar)zenyListBox.SelectedItem;
                spravcaPretekov1.zoznamPretekov[0].PridatKolo(bezec);
                spravcaPretekov1.zoznamPretekov[0].UrcenieCasuPoslednehoKola(bezec, casovacView.stopwatch.Elapsed);
                spravcaPretekov1.zoznamPretekov[0].PriemernaRychlost(bezec);
                spravca.ZoradZoznam(spravca.zoznamZien, spravca.premennyZoznamZien);
                spravcaPretekov1.zoznamPretekov[0].UrciPoradie(bezec, spravca.premennyZoznamZien);
                zenyListBox.SelectedItem = bezec;
                spravca.Uloz(spravca.zoznamZien, spravca.cestaZeny);
                //spravca.UlozObservable(spravca.premennyZoznamZien, spravca.cestaPremenneZeny);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void DiskvalifikaciaMuzaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spravca.DiskvalifikovatPretekara((Pretekar)muziListBox.SelectedItem, spravca.zoznamMuzov, spravca.premennyZoznamMuzov);
                spravca.Uloz(spravca.zoznamMuzov, spravca.cestaMuzi);
                //spravca.UlozObservable(spravca.premennyZoznamMuzov, spravca.cestaPremenniMuzi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prazdny zoznam pretekarov", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void DiskvalifikaciaZenaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spravca.DiskvalifikovatPretekara((Pretekar)zenyListBox.SelectedItem, spravca.zoznamZien, spravca.premennyZoznamZien);
                spravca.Uloz(spravca.zoznamZien, spravca.cestaZeny);
                //spravca.UlozObservable(spravca.premennyZoznamZien, spravca.cestaPremenneZeny);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prazdny zoznam pretekarov", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void PenalizaciaMuzaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pretekar bezec = (Pretekar)muziListBox.SelectedItem;
                spravcaPretekov1.zoznamPretekov[0].Penalizacia(bezec, trestneSekundyMuziTetxBox.Text);
                spravcaPretekov1.zoznamPretekov[0].PriemernaRychlost(bezec);
                spravca.ZoradZoznam(spravca.zoznamMuzov, spravca.premennyZoznamMuzov);
                spravcaPretekov1.zoznamPretekov[0].UrciPoradie(bezec, spravca.premennyZoznamMuzov);
                muziListBox.SelectedItem = bezec;
                spravca.Uloz(spravca.zoznamMuzov, spravca.cestaMuzi);
                //spravca.UlozObservable(spravca.premennyZoznamMuzov, spravca.cestaPremenniMuzi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void PenalizaciaZenaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pretekar bezec = (Pretekar)zenyListBox.SelectedItem;
                spravcaPretekov1.zoznamPretekov[0].Penalizacia(bezec, trestneSekundyZenaTetxBox.Text);
                spravcaPretekov1.zoznamPretekov[0].PriemernaRychlost(bezec);
                spravca.ZoradZoznam(spravca.zoznamZien, spravca.premennyZoznamZien);
                spravcaPretekov1.zoznamPretekov[0].UrciPoradie(bezec, spravca.premennyZoznamZien);
                muziListBox.SelectedItem = bezec;
                spravca.Uloz(spravca.zoznamZien, spravca.cestaZeny);
                //spravca.UlozObservable(spravca.premennyZoznamZien, spravca.cestaPremenneZeny);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
