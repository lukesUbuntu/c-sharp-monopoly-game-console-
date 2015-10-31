using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
    [TestFixture]
    class _PlayerTest
    {

        private Banker testbanker = new Banker();
        private Player testPlayer = new Player();
        private NewDice die1 = new NewDice();
        private NewDice die2 = new NewDice();

        [Test]
        public void GetName()
        {
            //check that the name returned is the same as the name set    
            Board.access().addPlayer(new Player("Player1"));
            Board.access().getPlayer(0).setName("Player1");
            Assert.AreSame("Player1", Board.access().getPlayer(0).getName());


        }
        [Test]
        public void GetJailStatus()
        {
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));
            Board.access().getPlayer(0).setIsInJail();

            Assert.IsTrue(Board.access().getPlayer(0).getJailStatis());


        }

        [Test]
        public void PayJailFine()
        {
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));

            Board.access().getPlayer(0).setBalance(100);
            Board.access().getPlayer(0).payJailFine();
            decimal bal = Board.access().getPlayer(0).getBalance();

            Assert.AreSame(50, bal);


        }

        [Test]
        public void SetNotInJail()
        {
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));

            Board.access().getPlayer(0).setIsInJail();
            Board.access().getPlayer(0).setIsNotInJail();


            Assert.IsFalse(Board.access().getPlayer(0).getJailStatis());


        }

        [Test]

        public void testMove()
        {

            //~~ This will test that the player will not move if they are in jail

            //Set the player as in jail
            testPlayer.setIsInJail();
            //get the players current location, this sould be 10
            int currentLocation = testPlayer.getLocation();
            //set the roll for this turn, it can not be a double or the player will no longer be in jail
            die1.setRole(1);
            die2.setRole(3);
            //attempt to move the player
            testPlayer.move();
            
            
            //assert that the player has not moved
            Assert.AreSame(currentLocation, 10);




            //~~ This will test that the player will move if they are not in jail

            //Set player is not in jail
            testPlayer.setIsNotInJail();
            //get the players current location, they should be on go
             currentLocation = testPlayer.getLocation();
            //set the roll for this turn, it can not be a double or the player will no longer be in jail
            die1.setRole(1);
            die2.setRole(3);
            //attempt to move the player
            testPlayer.move();


            //assert that the player has moved 4 places as set in the dice above
            Assert.AreSame(currentLocation, 4);


        }

        




        public class NewDice : Die
        {

            //private int numberRolled;

            public void setRole(int theRoll)
            {
                this.numberRolled = theRoll;
            }



        }
    }
}
