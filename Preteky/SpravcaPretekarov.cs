using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Preteky
{
    public class SpravcaPretekarov : INotifyPropertyChanged
    {
        public string cestaMuzi { get; set; }
        public string cestaZeny { get; set; }
        public string cestaPremenniMuzi { get; set; }
        public string cestaPremenneZeny { get; set; }
        public SpravcaPretekov spravcaPretekov { get; set; }
        public List<Pretekar> zoznamMuzov { get; set; }
        public ObservableCollection<Pretekar> premennyZoznamMuzov { get; set; }
        public List<Pretekar> zoznamZien { get; set; }
        public ObservableCollection<Pretekar> premennyZoznamZien { get; set; }

        public SpravcaPretekarov(SpravcaPretekov spravcaPretekov1)
        {
            cestaMuzi = "muzi.xml";
            cestaZeny = "zeny.xml";
            cestaPremenniMuzi = "premenniMuzi.xml";
            cestaPremenneZeny = "premenneZeny.xml";
            spravcaPretekov = spravcaPretekov1;
            zoznamMuzov = new List<Pretekar>();
            premennyZoznamMuzov = new ObservableCollection<Pretekar>();
            zoznamZien = new List<Pretekar>();
            premennyZoznamZien = new ObservableCollection<Pretekar>();

        }
        public void Export(string cesta)
        {
            using (StreamWriter sw = new StreamWriter(cesta))
            {
                sw.WriteLine("Muzi");
                sw.WriteLine("poradie, id, meno,     vek,   krajina,      pocet kol,  cas posledneho kola, penalizacia");
                foreach (var i in zoznamMuzov)
                { 
                    StringBuilder sb = new StringBuilder();
                    sb.Append(i.poradie);
                    sb.Append(",      ");
                    sb.Append(i.id);
                    sb.Append(", ");
                    sb.Append(i.meno);
                    sb.Append(",     ");
                    sb.Append(i.vek);
                    sb.Append(",   ");
                    sb.Append(i.krajina);
                    sb.Append(",       ");
                    sb.Append(i.pocetKol);
                    sb.Append(",            ");
                    sb.Append(i.casPoslednehoKolaStr);
                    sb.Append(",          ");
                    sb.Append(i.casovePenaleStr);
                    sw.WriteLine(sb.ToString());
                }
                sw.WriteLine();
                sw.WriteLine("Zeny");
                sw.WriteLine("poradie, id, meno,     vek,   krajina,      pocet kol,  cas posledneho kola, penalizacia");
                foreach (var i in zoznamZien)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(i.poradie);
                    sb.Append(",      ");
                    sb.Append(i.id);
                    sb.Append(", ");
                    sb.Append(i.meno);
                    sb.Append(",     ");
                    sb.Append(i.vek);
                    sb.Append(",   ");
                    sb.Append(i.krajina);
                    sb.Append(",       ");
                    sb.Append(i.pocetKol);
                    sb.Append(",            ");
                    sb.Append(i.casPoslednehoKolaStr);
                    sb.Append(",          ");
                    sb.Append(i.casovePenaleStr);
                    sw.WriteLine(sb.ToString());
                }
                sw.WriteLine();
                sw.WriteLine($"Zaciatok pretekov: {spravcaPretekov.zoznamPretekov[0].zaciatokPretekovStr}");
                sw.WriteLine($"Zaciatok pretekov: {spravcaPretekov.zoznamPretekov[0].koniecPretekovStr}");
            }
        }
        public void Reset()
        {
            spravcaPretekov.zoznamPretekov[0]= null;
            zoznamMuzov.Clear ();
            zoznamZien.Clear();
            premennyZoznamMuzov.Clear();
            premennyZoznamZien.Clear();

        }
        public void Uloz(List<Pretekar> zoznamOsob, string cesta)
        {
            XmlSerializer serializer = new XmlSerializer(zoznamOsob.GetType());
            using (StreamWriter sw = new StreamWriter(cesta))
            {
                serializer.Serialize(sw, zoznamOsob);
            }

        }
        //public void UlozObservable(ObservableCollection<Pretekar> zoznamOsob, string cesta)
        //{
        //    XmlSerializer serializer = new XmlSerializer(zoznamOsob.GetType());
        //    using (StreamWriter sw = new StreamWriter(cesta))
        //    {
        //        serializer.Serialize(sw, zoznamOsob);
        //    }

        //}
        public void NacitajZeny()
        {
            XmlSerializer serializer = new XmlSerializer(zoznamZien.GetType());
            if (File.Exists(cestaZeny))
            {
                using (StreamReader sr = new StreamReader(cestaZeny))
                {
                    zoznamZien = (List<Pretekar>)serializer.Deserialize(sr);
                }
            }
            else
            {
                zoznamZien = new List<Pretekar>();
            }

        }
        //public void NacitajObservableZeny()
        //{
        //    XmlSerializer serializer = new XmlSerializer(premennyZoznamZien.GetType());
        //    if (File.Exists(cestaPremenneZeny))
        //    {
        //        using (StreamReader sr = new StreamReader(cestaPremenneZeny))
        //        {
        //            premennyZoznamZien = (ObservableCollection<Pretekar>)serializer.Deserialize(sr);
        //        }
        //    }
        //    else
        //    {
        //        premennyZoznamZien = new ObservableCollection<Pretekar>();
        //    }
        //}
        public void NacitajMuzi()
        {
            XmlSerializer serializer = new XmlSerializer(zoznamMuzov.GetType());
            if (File.Exists(cestaMuzi))
            { 
                using (StreamReader sr = new StreamReader(cestaMuzi))
                {
                    zoznamMuzov = (List<Pretekar>)serializer.Deserialize(sr);
                }
            }
            else
            {
                zoznamMuzov = new List<Pretekar>();
            }
             
        }
        //public void NacitajObservableMuzi()
        //{
        //    XmlSerializer serializer = new XmlSerializer(premennyZoznamMuzov.GetType());
        //    if (File.Exists(cestaPremenniMuzi ))
        //    {
        //        using (StreamReader sr = new StreamReader(cestaPremenniMuzi))
        //        {
        //            premennyZoznamMuzov = (ObservableCollection<Pretekar>)serializer.Deserialize(sr);
        //        }
        //    }
        //    else
        //    {
        //        premennyZoznamMuzov = new ObservableCollection<Pretekar>();
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string vlastnost)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(vlastnost));
            }
        }
        public void ZoradZoznam(List<Pretekar> zoznam, ObservableCollection<Pretekar> premennyZoznam)
        {
            
            zoznam.Sort();
            premennyZoznam.Clear();
            for (int i = 0; i < zoznam.Count; i++)
            {
                premennyZoznam.Add(zoznam[i]);
            }
            OnPropertyChanged("premennyZoznam");
        }
        public void PridatMuza(Pretekar pretekar)
        {
            zoznamMuzov.Add(pretekar);
            //premennyZoznamMuzov.Add(pretekar);
            ZoradZoznam(zoznamMuzov, premennyZoznamMuzov);

        }
        public void PridatZenu(Pretekar pretekar)
        {
            zoznamZien.Add(pretekar);
            premennyZoznamZien.Add(pretekar);
            ZoradZoznam(zoznamZien, premennyZoznamZien);
        }

        public void DiskvalifikovatPretekara(Pretekar pretekar, List<Pretekar> pretekars, ObservableCollection<Pretekar> pretekaris)
        {
            if (pretekar == null)
            {
                throw new ArgumentException("Nie je zvoleny pretekar!");
            }
            else if (pretekars == null)
            {
                throw new ArgumentException("Zoznam pretekarov je prazdny!");
            }
            else
            {
                pretekars.Remove(pretekar);
                ZoradZoznam(pretekars, pretekaris);
            }

        }


    }
}
