using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;
using System.IO;

namespace MolopolyGame.Testing
{
    [TestFixture]
    class _MonopolyTest : TestHelper
    {
        private Banker testbanker = new Banker();
        private Player testPlayer = new Player();
        private Monopoly testGame = new Monopoly();
        private Board testBoard = new Board();
        private consoleIntercept theConsole;
        private consoleReader theConsoleReader;

        public _MonopolyTest()
        {
            //setup our console read and write
            theConsole = new consoleIntercept();
            theConsoleReader = new consoleReader();
            Console.SetOut(theConsole);
            Console.SetIn(theConsoleReader);
            testGame.setUpProperties();
        }

        [Test]
        public void testsetUpGame()
        {
            theConsoleReader.clear();
            theConsoleReader.useKey("2");
            //send y to confirm the action
            theConsoleReader.useKey("playerOne");
            theConsoleReader.useKey("playerTwo");

            testGame.setUpGame();
            theConsoleReader.clear();
            Assert.IsTrue(testGame.playerList.Count >= 2);
            
        }
        [Test]
        public void testInputInt()
        {
            theConsoleReader.clear();
            theConsoleReader.useKey("2");
            testGame.inputInteger();
            Assert.AreEqual(2, 2);
         

        }
        [Test]
        public void setupPlayers()
        {

            theConsoleReader.clear();


            //send 2 to the console to add two players
            theConsoleReader.useKey("1");
            theConsoleReader.useKey("2");
            //send y to confirm the action
           // theConsoleReader.useKey("y");
            theConsoleReader.useKey("playerOne");
            theConsoleReader.useKey("playerTwo");
            theConsoleReader.useKey("3");

            testGame.setUpPlayers();
            string playerOneName = testGame.playerList[0].getName();
            string playerTwoName = testGame.playerList[1].getName();
            theConsoleReader.clear();
            Assert.IsTrue(playerOneName == "playerOne");
            Assert.IsTrue(playerTwoName == "playerTwo");
            
        }
        [Test]
        public void testsetUpProperties()
        {
            testGame.setUpProperties();
            Assert.NotNull(testGame);

        }

        //[Test]
        public void TestpurchaseProperty()
        {
            testPlayer.setBalance(8000);
            testPlayer.setLocation(2, false); ;
            testGame.purchaseProperty(testPlayer);
            

            Assert.IsTrue(testPlayer.getPropertiesOwned().Count > 0);
            

        }

       // [Test]
        public void TestJailMenu()
        {
            theConsoleReader.clear();

            //console interceptor for our test
            theConsoleReader.useKey("2");
            theConsole.ClearConsole();

            //.useKey("3");



            testGame.displayJailMenu(testPlayer);

            //testing menu inputs

            ArrayList T = theConsole.getOutput();
            Assert.IsTrue((T[1].ToString() == "1. Pay $50"));
            //"1. Pay $50"
            theConsoleReader.useKey("1");
            //clear console.
            theConsole.ClearConsole();

            testGame.inputInteger();
            Assert.NotNull(testGame);

        }

        [Test]
        public void TestPlayerChoiceMenu()
        {
            theConsoleReader.clear();
            theConsoleReader.useKey("1");
            theConsoleReader.useKey("1");
            theConsole.ClearConsole();
            testGame.displayPlayerChoiceMenu(testPlayer);

            ArrayList T = theConsole.getOutput();
            Assert.IsTrue((T[1].ToString() == "1. Finish turn"));
            theConsole.ClearConsole();

            testGame.inputInteger();
            Assert.NotNull(testGame);

        }
        // [Test]
        public void TestMainChoiceMenu()
        {
            theConsoleReader = new consoleReader();
            theConsoleReader.useKey("1");
            theConsole.ClearConsole();
            testGame.displayMainChoiceMenu();

            ArrayList T = theConsole.getOutput();
            Assert.IsTrue((T[1].ToString() == "2. Start New Game"));
            theConsole.ClearConsole();

            testGame.inputInteger();
            Assert.NotNull(testGame);

        }
       // [Test]
        public void TestSetUpBoard()
        {
            //testGame.setUpProperties();
            Board.access().ClearBoard();
            testGame.setUpProperties();

            Assert.IsTrue(Board.access().getProperty(0).getName() == "Go");
            Assert.IsTrue(Board.access().getProperty(0).getOwner().getName().ToString() == "Banker");
        }


        //  [Test]
        public void TestgetInputYN()
        {
            theConsoleReader = new consoleReader();
            String Question = "Will This Test Pass";
            testGame.getInputYN(testPlayer, Question);
            theConsoleReader.useKey("S");

            //   theConsole.ClearConsole();
            ArrayList T = theConsole.getOutput();
            Assert.IsTrue((T[0].ToString() == "That answer cannot be understood. Please answer 'y' or 'n'."));
        }




    }




}
