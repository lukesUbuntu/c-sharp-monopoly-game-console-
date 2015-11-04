using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
     [TestFixture]
    class _propertyTest : TestHelper
    {
         Player testplayer = new Player();
         Property testProp = new Property();
         Banker testBank = new Banker();
         Monopoly testGame = new Monopoly();
         ResidentialFactory resFactory = new ResidentialFactory();
         consoleIntercept theConsole = new consoleIntercept();
         consoleReader theConsoleReader = new consoleReader();

         public _propertyTest()
         {
             Console.SetIn(theConsoleReader);
             Console.SetOut(theConsole);
         }
         [Test]
         public void SetandGetOwnerTest()
         {
              Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
              Board.access().getProperty(0).setOwner(ref testplayer);

              Assert.AreEqual(Board.access().getProperty(0).getOwner(), testplayer);

             
         }

         [Test]
         public void TestMortgageProp()
         {
             testplayer.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
             Board.access().getProperty(0).setOwner(ref testplayer);
             Board.access().getProperty(0).mortgageProperty();

             Assert.IsTrue(Board.access().getProperty(0).isMortgaged);
             Assert.AreNotEqual(500, testplayer.getBalance());

         }
         [Test]
         public void TestAddHouse()
         {
             //set the players balance
             testplayer.setBalance(1000);
             //set yep propertyies on the board
             testGame.setUpProperties();
           //set the owner of property 3 to the test player
             Board.access().getProperty(3).setOwner(ref testplayer);
             //make sure no house on property
             Property thisProp = Board.access().getProperty(3);
             //check that the property does not currently have any 
             //houses on it
             Assert.IsTrue(((Residential)thisProp).getHouseCount() == 0);
             //send 1 to the console to select the property
             theConsoleReader.useKey("1");
             //send y to confirm the action
             theConsoleReader.useKey("y");
            

             //buy the house with the above console inputs 
             testGame.buyHouse(testplayer);
             theConsole.ClearConsole();
             //asert test that now we have a house
             Assert.IsTrue(((Residential)thisProp).getHouseCount() == 1);
         }



         [Test] 
         public void TestUnMortgageProp()
         {
             testplayer.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
             Board.access().getProperty(0).setOwner(ref testplayer);
             Board.access().getProperty(0).mortgageProperty();
             Board.access().getProperty(0).unMortgageProperty();

             Assert.IsFalse(Board.access().getProperty(0).isMortgaged);
             Assert.AreNotEqual(500, testplayer.getBalance());

         }

    }
}
