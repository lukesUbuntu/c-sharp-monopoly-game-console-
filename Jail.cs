using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Speech.Synthesis;

namespace MolopolyGame
{
  public class Jail : Property
    {
      // This is used to set if this is the set if this is the jail property or the send to jail property
        private bool setJail;
        public Jail() : this("Jail") { }

        public Jail(string sName, bool setJail = false)
        {
            this.sName = sName;
            this.setJail = setJail;
        }
        public bool isjailproperty()
        {
            return this.setJail;
        }

        public override string ToString()
        {
            return base.ToString();
        }


        public override string landOn(ref Player player)
        {
            
            if (this.setJail == true)
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.Speak("You have gone to jail, you do not pass go you can not collect $200");
                player.setIsInJail();
                player.setLocation(10, false);
                return base.landOn(ref player) + String.Format(player.getName().ToString() + " has gone to jail!");
            }
            else
            {
                return base.landOn(ref player) + String.Format(player.getName().ToString() + " is just visiting!.");

            }

        }
       
        /*
           public override string landOn(ref Player player)
        {
            //if this is a jail property and user has past go.
            //&& player.passGo()
            if (this.isJail)
            {
                //player landed on jail
                player.setIsInJail();
                //if is a benefit player receives amount else pay amount
                return base.landOn(ref player) + String.Format("{0} has gone to jail.", player.getName());

            }
            else
            {
                //if is a benefit player receives amount else pay amount
                return base.landOn(ref player) + String.Format("{0} just visting jail.", player.getName());

            }
           
                    
        }*/


    }
}
