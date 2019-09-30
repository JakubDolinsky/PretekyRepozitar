using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preteky
{
   public class UzivateliaCasovacData
    {
        public CasovacView casovac { get; set; }
        public SpravcaPretekarov spravcaPretekarov { get; set; }
        public SpravaPretekov spravaPretekov { get; set; }
        public UzivateliaCasovacData(CasovacView casovacView, SpravcaPretekarov spravca, SpravaPretekov spravaPreteky)
        {
            casovac = casovacView;
            spravcaPretekarov = spravca;
            spravaPretekov = spravaPreteky; 
        }
    }
    
}
