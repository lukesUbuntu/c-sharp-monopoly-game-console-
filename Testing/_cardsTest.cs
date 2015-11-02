using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
    [TestFixture]
    class _cardsTest
    {
        Community_Chest testccCards = new Community_Chest();
        Chance_Cards testChanceCards = new Chance_Cards();
        Player testplayer = new Player();
        Banker testBank = new Banker();
       
        [Test]
        public void testCommunityChestCardList()
        {
            //get the card list
            testccCards.CardList();
            //check that the list is not empty
            Assert.IsNotEmpty(testccCards.CardList());

            foreach (var i in testccCards.CardList())
            {
                Assert.IsNotNull(i.Action);
                Assert.IsNotEmpty(i.Name);

            }

        }
        [Test]
        public void testChanceList()
        {
            //get the card list
            testChanceCards.CardList();
            //check that the list is not empty
            Assert.IsNotEmpty(testChanceCards.CardList());

            foreach (var i in testChanceCards.CardList())
            {
                Assert.IsNotNull(i.Action);
                Assert.IsNotEmpty(i.Name);

            }

        }

        [Test]
        public void testShuffleCards()
        {
            testccCards.the_bank = testBank;
            testplayer.setBalance(1000);
            var firstDraw = testccCards.draw_card(testplayer);
            var secondDraw = testccCards.draw_card(testplayer);
            Assert.AreNotSame(firstDraw, secondDraw);

        }

        [Test]
        public void testShuffleCardsChance()
        {
            testccCards.the_bank = testBank;
            testplayer.setBalance(1000);
            var firstDraw = testChanceCards.draw_card(testplayer);
            var secondDraw = testChanceCards.draw_card(testplayer);
            Assert.AreNotSame(firstDraw, secondDraw);

        }


    }
}
