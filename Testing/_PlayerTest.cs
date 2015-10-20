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
        public void stringTest()
        {
            //Check that the player is in jail if setIsInJail is called  
            Board.access().addPlayer(new Player("Player1"));

            //Assert.IsInstanceOf(string , string );
            

        }



    }
}
