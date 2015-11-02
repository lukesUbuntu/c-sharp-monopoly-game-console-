using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
     [TestFixture]
    class _propertyTest
    {
         Player testplayer = new Player();
         Property testProp = new Property();
         Banker testBank = new Banker();
         ResidentialFactory resFactory = new ResidentialFactory();
         
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
