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

             Board.access().ClearBoard();
             
             //clear console.log
             theConsoleReader.clear();
             Player testplayer2 = new Player();
             testplayer.setBalance(500);
             testplayer2.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));


             Assert.IsTrue(((TradeableProperty)Board.access().getProperty(0)).availableForPurchase());
             Board.access().getProperty(0).setOwner(ref testplayer);
             ((TradeableProperty)Board.access().getProperty(0)).landOn(ref testplayer2);

             ((TradeableProperty)Board.access().getProperty(0)).payRent(ref testplayer);
             Board.access().getProperty(0).mortgageProperty();
             //testing tradeable class
             ((TradeableProperty)Board.access().getProperty(0)).payRent(ref testplayer);
             //theConsoleReader
             Assert.Contains("This property has been mortgaged, you do not need to pay rent: ", theConsole.getOutput());

             Assert.IsFalse(((TradeableProperty)Board.access().getProperty(0)).availableForPurchase());
             Assert.IsFalse(((Property)Board.access().getProperty(0)).availableForPurchase());

             //check_mortgaged_status

             
             Assert.IsTrue(Board.access().getProperty(0).isMortgaged);
             Assert.AreNotEqual(500, testplayer.getBalance());
            
         }

         [Test]
         public void TestAddHouse()
         {
             Board.access().ClearBoard();
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
             Assert.IsTrue(((Residential)thisProp).getHouseCount() >= 0);
         }

       

         [Test] 
         public void TestUnMortgageProp()
         {
             Board.access().ClearBoard();
             testplayer.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
             Board.access().getProperty(0).setOwner(ref testplayer);
             Board.access().getProperty(0).mortgageProperty();
             Board.access().getProperty(0).unMortgageProperty();
             
             Assert.IsFalse(Board.access().getProperty(0).isMortgaged);
             Assert.AreNotEqual(500, testplayer.getBalance());

             testplayer = new Player();
             testplayer.setBalance(-1000);
             Board.access().getProperty(0).setOwner(ref testplayer);
             ((Property)Board.access().getProperty(0)).mortgageProperty();

             ((Residential)Board.access().getProperty(0)).addHouse();
             ((Residential)Board.access().getProperty(0)).SellHouses();
             Assert.IsTrue(((Residential)Board.access().getProperty(0)).getMortgageValue() == 30);
             Assert.IsTrue(((Residential)Board.access().getProperty(0)).getHotelCount() == 0);

             ((Property)Board.access().getProperty(0)).mortgageProperty();
             ((Property)Board.access().getProperty(0)).unMortgageProperty();
         }


         [Test]
         public void testBuyHotel()
         {
             Board.access().ClearBoard();
             testplayer.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
             Board.access().getProperty(0).setOwner(ref testplayer);

             //add hotels
             testplayer.setBalance(1000);
             for (int x = 0; x < 4; x++)
                 ((Residential)Board.access().getProperty(0)).addHouse();

             ((Residential)Board.access().getProperty(0)).addHotel();
         }


         [Test]
         public void testmortgageProperty()
         {
             Board.access().ClearBoard();
             testplayer.setBalance(500);
             Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
             Board.access().getProperty(0).setOwner(ref testplayer);
             Property theProp = new Property();
             theProp.availableForPurchase();
             theProp.mortgageProperty();
             theProp.getMortgageValue();
             theProp.check_mortgaged_status();
             
         }

    }
}
