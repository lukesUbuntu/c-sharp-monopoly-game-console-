using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
     /// <summary>
    /// This is class for a die that generates random number 1-6 inclusive
    /// </summary>
    
    public class Die
    {
        private static Random numGenerator = new Random();
       public int numberRolled;
        
        public int roll()
        {
            numberRolled = numGenerator.Next(1, 7);

<<<<<<< HEAD
            
=======
            return 1;
>>>>>>> 4a554dffb33d359b468d1d23c42edc2c2008b596
            
            return numberRolled;
        }

        public int numberLastRolled()
        {
            return numberRolled;
        }
         
        public override string ToString()
        {
            return numberRolled.ToString();
        }
    }
}
