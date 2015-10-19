using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
    [TestFixture]
    class _LandOnTest
    {
        [Test]
        /// <summary>
        /// This test has been created to test that you will not have to pay rent if you land on a mortgaged property 
        /// </summary>
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

        public void TestLandOnJail() {
            Player playerone = new Player();
            
            Board theboard = new Board();

            Jail Visting = new Jail("visitng", false);
            Board.access().addPlayer(playerone);
           
            Board.access().addProperty(Visting);

            playerone.setLocation(0, false);
            Visting.landOn(ref playerone);
        }
        
    }
}
