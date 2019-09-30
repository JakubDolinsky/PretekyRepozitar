using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preteky
{
    public class Pretekar : IComparable, INotifyPropertyChanged
    {
        public int id { get; set; }
        private static int idcko = 1;
        public string meno { get; set; }
        public int vek { get; set; }
        public string pohlavie { get; set; }
        public string krajina { get; set; }
        public int pocetKol { get; set; }
        public TimeSpan casPoslednehoKola { get; set; }
        public string casPoslednehoKolaStr { get; set; } 
        public double priemernaRychlost { get; set; }
        public int poradie { get; set; }
        public TimeSpan casovePenale { get; set; }
        public string casovePenaleStr { get; set; }

        public Pretekar(string Meno, string Vek, string Pohlavie, string Krajina)
        {
            if (Meno == string.Empty)
            {
                throw new Exception("Pole pre meno pretekara je prazdne!");
            }
            if (Vek == string.Empty)
            {
                throw new Exception("Prazdne pole pre vek pretekara!");
            }
            if (!int.TryParse(Vek, out int Vek1) || Vek1 < 0)
            {
                throw new Exception("Zly ciselny format pre vek!");
            }

            if (Krajina == string.Empty)
            {
                throw new Exception("Nie je vybrata krajina!");
            }
            else
            {
                id = idcko;
                meno = Meno;
                vek = Vek1;
                pohlavie = Pohlavie;
                krajina = Krajina;
                idcko++;
                pocetKol = 0;
                casPoslednehoKola = new TimeSpan(0, 0, 0, 0,0);
                casPoslednehoKolaStr = $"{casPoslednehoKola.Hours}:{casPoslednehoKola.Minutes}:{casPoslednehoKola.Seconds},{casPoslednehoKola.Milliseconds / 10}";
                casovePenale = new TimeSpan(0, 0, 0, 0, 0);
                casovePenaleStr = $"{casovePenale.Hours}:{casovePenale.Minutes}:{casovePenale.Seconds},{casovePenale.Milliseconds/10}";
                priemernaRychlost = 0;
                poradie = 0;
            }
        }
        public Pretekar()
        { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string vlastnost)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(vlastnost));
            }
        }
        public int CompareTo(object obj)
        {
            Pretekar dalsi = (Pretekar)obj;
            if (this.pocetKol > dalsi.pocetKol)
            { return -1; }
            if (this.pocetKol < dalsi.pocetKol)
            { return 1; }
            else
            {
                if (this.casPoslednehoKola < dalsi.casPoslednehoKola)
                { return -1; }
                if (this.casPoslednehoKola > dalsi.casPoslednehoKola)
                { return 1; }
                else
                { return 0; }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  ");
            sb.Append(id);
            sb.Append("           ");
            sb.Append(meno);
            sb.Append("                                ");
            sb.Append(krajina);
            return sb.ToString();
        }
    }
}
