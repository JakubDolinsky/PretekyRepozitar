using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Preteky
{
    public class SpravcaPretekov
    {
        public string cestaPretek { get; set; }
        public SpravaPretekov[] zoznamPretekov { get; set; }


        public SpravcaPretekov()
        {
            cestaPretek = "preteky.xml";
            zoznamPretekov = new SpravaPretekov[1];

        }
        public void Uloz()
        {
            XmlSerializer serializer = new XmlSerializer(zoznamPretekov.GetType());
            using (StreamWriter sw = new StreamWriter(cestaPretek))
            {
                serializer.Serialize(sw, zoznamPretekov);
            }
            
        }
        public void Nacitaj()
        {
            XmlSerializer serializer = new XmlSerializer(zoznamPretekov.GetType());
            if (File.Exists(cestaPretek))
            {
                using (StreamReader sr = new StreamReader(cestaPretek))
                {
                    zoznamPretekov = (SpravaPretekov[])serializer.Deserialize(sr);
                }
            }
            else
            {
                zoznamPretekov = new SpravaPretekov[1];
            }
        }
        
        public void PridajPreteky(SpravaPretekov preteky)
        {
            zoznamPretekov[0] = preteky;
        }


    }
}
