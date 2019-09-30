using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preteky
{
    public class SpravaPretekov : INotifyPropertyChanged
    {
        private int pocetKliknuti;
        public TimeSpan zaciatokPretekov { get; set; }
        public TimeSpan koniecPretekov { get; set; }
        public string zaciatokPretekovStr { get; set; }
        public string koniecPretekovStr { get; set; }
        public double dlzkaKola { get; set; }
        public int pocetKol { get; set; }
        public string casovyLimit { get; set; }
        //public CasovacView casomiera { get; set; }
        public SpravaPretekov(string DlzkaKola, string PocetKol, string CasovyLimit)
        {
            
            pocetKliknuti = 0;
            zaciatokPretekov = new TimeSpan(0, 0, 0, 0, 0);
            zaciatokPretekovStr = $"{zaciatokPretekov.Hours }:{zaciatokPretekov.Minutes}:{zaciatokPretekov.Seconds},{zaciatokPretekov.Milliseconds / 10}";
            koniecPretekov = new TimeSpan(0, 0, 0, 0, 0);
            koniecPretekovStr = $"{koniecPretekov.Hours }:{koniecPretekov.Minutes}:{koniecPretekov.Seconds},{koniecPretekov.Milliseconds / 10}";
            if (DlzkaKola == string.Empty)
            {
                throw new ArgumentException("Prazdne pole pre dlzku kola!");
            }
            if (PocetKol == string.Empty)
            {
                throw new ArgumentException("Prazdne pole pre pocet kol!");
            }
            if (CasovyLimit == string.Empty)
            {
                throw new ArgumentException("Prazdne pole pre casovy limit!");
            }
            if (!double.TryParse(DlzkaKola, out double DlzkaKola1) || DlzkaKola1 < 0)
            {
                throw new ArgumentException("Zly ciselny format pre dlzku kola!");
            }
            if (!int.TryParse(PocetKol, out int PocetKol1) || PocetKol1 < 0)
            {
                throw new ArgumentException("Zly ciselny format pre pocet kol!");
            }
            if (!TimeSpan.TryParse(CasovyLimit, out TimeSpan CasovyLimit1) || CasovyLimit1.Days > 0 || (CasovyLimit1.Hours < 0 || CasovyLimit1.Minutes < 0 || CasovyLimit1.Seconds < 0 || CasovyLimit1.Milliseconds < 0))
            {
                throw new ArgumentException("Zly ciselny format pre casovy limit!");
            }

            else
            {
                dlzkaKola = DlzkaKola1;
                pocetKol = PocetKol1;
                casovyLimit = CasovyLimit1.ToString();
                //OnPropertyChanged("zaciatokPretekovStr");
                OnPropertyChanged("koniecPretekovStr");
            }

        }
        public SpravaPretekov()
        { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string vlastnost)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(vlastnost));
            }
        }
        public void zadajZaciatokPretekov()
        {
            pocetKliknuti++;
            if (pocetKliknuti == 1)
            {
                zaciatokPretekov = DateTime.Now - new DateTime(0);
                zaciatokPretekovStr = $"{zaciatokPretekov.Hours }:{zaciatokPretekov.Minutes}:{zaciatokPretekov.Seconds},{zaciatokPretekov.Milliseconds / 10}";
                OnPropertyChanged("zaciatokPretekovStr");
            }
        }
        public void zadajKoniecPretekov()
        {
            koniecPretekov = DateTime.Now - new DateTime(0);
            koniecPretekovStr = $"{koniecPretekov.Hours }:{koniecPretekov.Minutes}:{koniecPretekov.Seconds},{koniecPretekov.Milliseconds / 10}";
            OnPropertyChanged("koniecPretekovStr");
        }
        public void PridatKolo(Pretekar pretekar)
        {
            if (pretekar == null)
            {
                throw new ArgumentException("Nie je zvoleny pretekar!");
            }
            else if (pretekar.pocetKol == pocetKol)
            {
                throw new ArgumentException("Pretekar uz odbehol posledne kolo pretekov. Nemozno mu pridat kolo!");
            }
            else
            {
                pretekar.pocetKol++;
                pretekar.OnPropertyChanged("pocetKol");
            }
        }
        public void Penalizacia(Pretekar pretekar, string casovePenale)
        {
            
             if (pretekar == null)
            {
                throw new ArgumentException("Nie je zvoleny pretekar!");
            }
           else if (casovePenale == string.Empty)
            {
                throw new ArgumentException("Prazdne pole pre penalizaciu!");
            }
            else if (!TimeSpan.TryParse(casovePenale, out TimeSpan casovePenale1) || casovePenale1.Days > 0 || (casovePenale1.Hours < 0 || casovePenale1.Minutes < 0 || casovePenale1.Seconds < 0 || casovePenale1.Milliseconds < 0))
            {
                throw new ArgumentException("Zly ciselny format pre penalizaciu!");
            }
            
            else
            {
                pretekar.casPoslednehoKola += casovePenale1;
                pretekar.casPoslednehoKolaStr = $"{pretekar.casPoslednehoKola.Hours }:{pretekar.casPoslednehoKola.Minutes}:{pretekar.casPoslednehoKola.Seconds},{pretekar.casPoslednehoKola.Milliseconds / 10}";
                pretekar.casovePenale += casovePenale1;
                pretekar.casovePenaleStr = $"{pretekar.casovePenale.Hours }:{pretekar.casovePenale.Minutes}:{pretekar.casovePenale.Seconds},{pretekar.casovePenale.Milliseconds / 10}";
                PriemernaRychlost(pretekar);
                pretekar.OnPropertyChanged("casovePenaleStr");
            }
        }
        public void UrcenieCasuPoslednehoKola(Pretekar pretekar, TimeSpan CasPoslednehoKola)
        {
            pretekar.casPoslednehoKola = CasPoslednehoKola;
            pretekar.casPoslednehoKolaStr = $"{pretekar.casPoslednehoKola.Hours }:{pretekar.casPoslednehoKola.Minutes}:{pretekar.casPoslednehoKola.Seconds},{pretekar.casPoslednehoKola.Milliseconds / 10}";
            pretekar.OnPropertyChanged("casPoslednehoKolaStr");
        }
        public void PriemernaRychlost(Pretekar pretekar)
        {
            if (pretekar.casPoslednehoKola == new TimeSpan(0, 0, 0, 0, 0))
            {
                pretekar.priemernaRychlost = 0;
            }
            else
            {
                pretekar.priemernaRychlost =Math.Round(((pretekar.pocetKol * dlzkaKola) / ((pretekar.casPoslednehoKola.TotalHours * 3600) + (pretekar.casPoslednehoKola.TotalMinutes * 60) + (pretekar.casPoslednehoKola.TotalSeconds) + (pretekar.casPoslednehoKola.TotalMilliseconds / 1000))),2);
                pretekar.OnPropertyChanged("priemernaRychlost");
            }
        }
        public void UrciPoradie(Pretekar pretekar, ObservableCollection<Pretekar> pretekari)
        {
            pretekar.poradie = pretekari.IndexOf(pretekar) + 1;
            pretekar.OnPropertyChanged("poradie");
        }



    }
}
