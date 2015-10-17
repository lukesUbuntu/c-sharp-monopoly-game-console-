using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
    //[TestFixture]
    class _LandOnTest
    {
        //[Test]
        public void TestLandOn()
        {

            Player playerone = new Player();
            Player playertwo = new Player();
            Board theboard = new Board();

            Residential theproperty = new Residential("Test Property",100,20,30);
            theproperty.setOwner(ref playerone);

            theproperty.isMortgaged = true;
            Board.access().addPlayer(playerone);
            Board.access().addPlayer(playertwo);
            Board.access().addProperty(theproperty);

            playertwo.setLocation(0, false);

            theproperty.landOn(ref playertwo);

        }
        
    }
}
