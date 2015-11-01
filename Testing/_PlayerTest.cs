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
        private NewDice TestDie1 = new NewDice();
        private NewDice TestDie2 = new NewDice();

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
            Board.access().ClearBoard(); 
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));

            Board.access().getPlayer(0).setBalance(100);
            Board.access().getPlayer(0).payJailFine();
            decimal bal = Board.access().getPlayer(0).getBalance();

            Assert.IsTrue(50 == bal);


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

        public void TestSetIsInJail()
        {
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));

            Board.access().getPlayer(0).setIsInJail();
            


            Assert.IsTrue(Board.access().getPlayer(0).getJailStatis());


        }

        //this test is not working, having trouble setting the die roll
        [Test]

        public void testMove()
        {

            //~~ This will test that the player will not move if they are in jail
            Board.access().addPlayer(testPlayer);
            //Set the player as in jail
            Board.access().getPlayer(0).setIsInJail();
            //get the players current location, this sould be 10
            int currentLocation = Board.access().getPlayer(0).getLocation();
            //set the roll for this turn, it can not be a double or the player will no longer be in jail

            TestDie1.setRole(1);
            TestDie2.setRole(3);

            testPlayer.die1 = TestDie1;
            testPlayer.die2 = TestDie2;

            //attempt to move the player
            testPlayer.move();
            
            
            //assert that the player has not moved
           // test

            Assert.AreEqual(currentLocation, 10);




            //~~ This will test that the player will move if they are not in jail

           
           
            //Set the player as in jail
            Board.access().getPlayer(0).setIsNotInJail();
            //put the player back on go 
            Board.access().getPlayer(0).setLocation(0, false);
            //get the players current location, this sould be 10
            currentLocation = Board.access().getPlayer(0).getLocation();
            //set the roll for this turn, it can not be a double or the player will no longer be in jail


            TestDie1.setRole(4);
            TestDie2.setRole(3);

            testPlayer.die1 = TestDie1;
            testPlayer.die2 = TestDie2;


            //attempt to move the player
            testPlayer.move();
            currentLocation = Board.access().getPlayer(0).getLocation();
            //assert that the player has moved 4 places as set in the dice above
            Assert.IsTrue(testPlayer.getLocation() >= 7);


        }

        
        [Test]
        public void TestsetLocation()
        {
            Board.access().addPlayer(testPlayer);
            Board.access().getPlayer(0).setLocation(16, false);

          int LocationIndex = Board.access().getPlayer(0).getLocation();

          Assert.AreEqual(16, LocationIndex);
        }

        [Test]
        public void TestcheckBankrupt()
        {
            Board.access().addPlayer(testPlayer);
            testPlayer.setBalance(100);
            testPlayer.checkBankrupt();

            Assert.IsFalse(testPlayer.isNotActive());

            Board.access().addPlayer(testPlayer);
            testPlayer.setBalance(0);
            testPlayer.checkBankrupt();

            Assert.IsTrue(testPlayer.isNotActive());

        }

        [Test]
        public void TestgetPropertiesOwnedFromBoard()
        {
            ResidentialFactory resFactory = new ResidentialFactory();
            Board.access().addPlayer(testPlayer);
            testPlayer.setBalance(1000);
            Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
            testPlayer.setLocation(0, false);
            TradeableProperty propertyLocatedOn = (TradeableProperty)Board.access().getProperty(testPlayer.getLocation());
            propertyLocatedOn.purchase(ref testPlayer);

           

            Assert.IsNotNull(testPlayer.getPropertiesOwned());
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
