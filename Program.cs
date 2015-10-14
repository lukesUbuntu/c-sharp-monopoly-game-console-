using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
     /// <summary>
    /// Main class for the program
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            Community_Chest theCom = new Community_Chest();
            Player thenPlayer = new Player();

           // theCom.draw_card(ref thenPlayer);


            

            Game game = new Monopoly();

            game.initializeGame();
        }

     
    }
}
