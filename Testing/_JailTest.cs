using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
     [TestFixture]
   public class _JailTest
    {
         JailFactory testJailFac = new JailFactory();
         Jail testjail = new Jail();
         Player testPlayer = new Player();
         [Test]
         public void RollDoubleGoToJail() {

             Board.access().addPlayer(new Player("Player1"));
             Board.access().getPlayer(0).setName("Player1");

             Board.access().getPlayer(0).rollDoubleCount = 3;
             Board.access().getPlayer(0).CheckForDouble();

             Assert.IsTrue(Board.access().getPlayer(0).getJailStatis());
            
         
         }
         [Test]
         public void RollDoubleDontGoToJail()
         {

             Board.access().addPlayer(new Player("Player1"));
             Board.access().getPlayer(0).setName("Player1");

             Board.access().getPlayer(0).rollDoubleCount = 0;
             Board.access().getPlayer(0).CheckForDouble();

             Assert.IsFalse(Board.access().getPlayer(0).getJailStatis());


         }

         [Test]
         public void testisjail()
         {
             testJailFac.create("Jail",true);

             testjail.isjailproperty();

             Board.access().addProperty(testJailFac.create("Jail", false));
             Board.access().addProperty(testJailFac.create("Jail", true));
             
             testjail.isjailproperty();

             

         }

          [Test]
         public void testLandOnJailJustVisit()
         {
             Jail jailtesting = new Jail("Jail", false);
             Board.access().addPlayer(testPlayer);

             Assert.IsFalse(testPlayer.getJailStatis());
             jailtesting.landOn(ref testPlayer);

         }

          [Test]
          public void testLandOnJailInJail()
          {
              Jail jailtesting = new Jail("Jail", true);
              Board.access().addPlayer(testPlayer);

              Assert.IsTrue(testPlayer.getJailStatis() == false);


          }

    }
}
