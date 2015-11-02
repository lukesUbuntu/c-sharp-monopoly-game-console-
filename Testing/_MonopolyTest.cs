using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;
using System.IO;

namespace MolopolyGame.Testing
{
     [TestFixture]
    class _MonopolyTest
    {
       private Banker testbanker = new Banker();
        private Player testPlayer = new Player();
        private Monopoly testGame = new Monopoly();
        private consoleIntercept theConsole;
        private consoleReader theConsoleReader;

        public _MonopolyTest()
        {
            //setup our console read and write
            theConsole = new consoleIntercept();
            theConsoleReader = new consoleReader();
            Console.SetOut(theConsole);
            Console.SetIn(theConsoleReader);
        }
     [Test]
         public void testsetUpProperties()
         {
             testGame.setUpProperties();
             Assert.NotNull(testGame);

         }
         
         [Test]
         public void TestpurchaseProperty()
         {
             testPlayer.setBalance(8000);
             testPlayer.setLocation(2, false);;
             testGame.purchaseProperty(testPlayer);
             

             Assert.IsNotEmpty(testPlayer.getPropertiesOwned());


         }

         [Test]
         public void TestJailMenu()
         {
             //console interceptor for our test
             theConsoleReader.useKey("2");
             theConsole.ClearConsole();

            //.useKey("3");


          
              testGame.displayJailMenu(testPlayer);
           
             //testing menu inputs
          
             ArrayList T = theConsole.getOutput();
             Assert.IsTrue((T[1].ToString() == "1. Pay $50"));
             //"1. Pay $50"
           
             //clear console.
             theConsole.ClearConsole();

             testGame.inputInteger();
             Assert.NotNull(testGame);

         }

         [Test]
         public void TestPlayerMenu()
         {
             testGame.displayPlayerChoiceMenu(testPlayer);
             Assert.NotNull(testGame);


         }

         [Test]
         public void TestMakePlay()
         {
             testGame.makePlay(0);

         }
    }

     public class consoleReader : TextReader
     {
         string key;
         public override string ReadLine()
         {
             return this.key;
         }

         public void useKey(string theKey)
         {
             this.key = theKey;
         }
     }


     public class consoleIntercept : TextWriter
     {
         ArrayList intercetped;
         
         public consoleIntercept()
         {
             this.intercetped = new ArrayList();
         }
         public override void WriteLine(string consoleMessage)
         {
             this.intercetped.Add(consoleMessage);
             //this.intercetped += consoleMessage;
         }
         public override void Write(string consoleMessage)
         {
             this.intercetped.Add(consoleMessage);
             //this.intercetped += consoleMessage;
         }
         public void ClearConsole()
         {
             this.intercetped.Clear();
             //this.intercetped = "";
         }
        
         public override Encoding Encoding
         {
             get { throw new Exception("The method or operation is not implemented."); }
         }
         
         public ArrayList getOutput()
         {
             return this.intercetped;
         }



     }
}
