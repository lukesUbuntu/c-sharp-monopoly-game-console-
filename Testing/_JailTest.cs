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
    }
}
