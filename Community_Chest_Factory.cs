using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame
{
   public class Community_Chest_Factory : PropertyFactory
    {
        
            public Community_Chest create(string sName)
            {
                return new Community_Chest(sName);
            }
        
    }
}
