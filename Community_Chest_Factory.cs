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

     public class Chance_Factory : PropertyFactory
    {
        
            public Chance_Cards create(string sName)
            {
                return new Chance_Cards(sName);
            }
        
    }
}
