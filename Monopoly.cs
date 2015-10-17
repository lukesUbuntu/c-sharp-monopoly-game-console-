using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MolopolyGame
{

    /// <summary>
    /// Main class for monoploy game that implements abstract class game
    /// </summary>

    public class Monopoly : Game
    {
        ConsoleColor[] colors = new ConsoleColor[8] { ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.DarkYellow };
        bool gameSetUp = false;
        int rollCount;
        public override void initializeGame()
        {

            displayMainChoiceMenu();

        }

        public override void makePlay(int iPlayerIndex)
        {
            Console.Clear();
            //make variable for player
            Player player = Board.access().getPlayer(iPlayerIndex);
            //Change the colour for the player
            Console.ForegroundColor = this.colors[iPlayerIndex];

            //if inactive skip turn
            if (player.isNotActive())
            {
                Console.WriteLine("\n{0} is inactive.\n", player.getName());
                //check players to continue
                //check that there are still two players to continue
                int activePlayerCount = 0;
                foreach (Player p in Board.access().getPlayers())
                {
                    //if player is active
                    if (!p.isNotActive())
                        //increment activePlayerCount
                        activePlayerCount++;
                }

                //if less than two active players display winner
                if (activePlayerCount < 2)
                {
                    this.printWinner();
                }

                return;
            }



            rollCount++;


            player.rollCount++;
            // if player is not in jail and has rolled  doubles incriment the doubles counter.
            if (player.getJailStatis() == false && player.CheckForDouble() == true) {


                player.rollDoubleCount++;

                if (player.rollDoubleCount == 3)
                {
                    //Send player to jail

                    player.setIsInJail();

                    //set the location as jail and do not pass go
                    player.setLocation(11, false);

                    Console.WriteLine("You have rolled doubles 3 times in a row and have been sent to jail! you did not pass Go, you did not collect $200!");
                    
                }
            }


           
            if (player.getJailStatis() == true) {



                //just for testing 
                Console.WriteLine("You are in jail!");
              

                //if roll is doubles
            if (player.CheckForDouble())
            {
                //set jail status to false
                player.setIsNotInJail();

                Console.WriteLine("Congradulations you have rolled doubles and have been released from jail!");


            }

            if (rollCount == 3)
            {
                Console.WriteLine("You have now made your 3rd roll, you must pay $50 to leave jail!");

            }
           
          
            }


       
           
                
            
         

            //prompt player to make move
            Console.WriteLine("{0}Your turn. Press Enter to make move", playerPrompt(iPlayerIndex));
            Console.ReadLine();
            //move player
            player.move();

            //Display making move
            Console.WriteLine("*****Move for {0}:*****", player.getName());
            //Display rolling
            Console.WriteLine("{0}{1}\n", playerPrompt(iPlayerIndex), player.diceRollingToString());

            Property propertyLandedOn = Board.access().getProperty(player.getLocation());
            //landon property and output to console
            Console.WriteLine(propertyLandedOn.landOn(ref player));
            //Display player details
            Console.WriteLine("\n{0}{1}", playerPrompt(iPlayerIndex), player.BriefDetailsToString());
            //display player choice menu
            displayPlayerChoiceMenu(player);


        }

        public override bool endOfGame()
        {
            //display message
            Console.WriteLine("The game is now over. Please press enter to exit.");
            Console.ReadLine();
            //exit the program
            Environment.Exit(0);
            return true;
        }

        public override void printWinner()
        {
            Player winner = null;
            //get winner who is last active player
            foreach (Player p in Board.access().getPlayers())
            {
                //if player is active
                if (!p.isNotActive())
                    winner = p;
            }
            //display winner
            Console.WriteLine("\n\n{0} has won the game!\n\n", winner.getName());
            //end the game
            this.endOfGame();
        }

        public void displayMainChoiceMenu()
        {
            int resp = 0;
            Console.WriteLine("Please make a selection:\n");
            Console.WriteLine("1. Setup Monopoly Game");
            Console.WriteLine("2. Start New Game");
            Console.WriteLine("3. Exit");
            Console.Write("(1-3)>");
            //read response
            resp = inputInteger();
            //if response is invalid redisplay menu
            if (resp == 0)
                this.displayMainChoiceMenu();

            //perform choice according to number input
            try
            {
                switch (resp)
                {
                    case 1:
                        this.setUpGame();
                        this.gameSetUp = true;
                        this.displayMainChoiceMenu();
                        break;
                    case 2:
                        if (this.gameSetUp)
                            this.playGame();
                        else
                        {
                            Console.WriteLine("The Game has not been set up yet.\n");
                            this.displayMainChoiceMenu();
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new ApplicationException("That option is not avaliable. Please try again.");
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void setUpGame()
        {
            //setup properties
            this.setUpProperties();

            //add players
            this.setUpPlayers();

        }

        public void playGame()
        {
            while (Board.access().getPlayerCount() >= 2)
            {
                for (int i = 0; i < Board.access().getPlayerCount(); i++)
                {
                    this.makePlay(i);
                }
            }
        }

        public int inputInteger() //0 is invalid input
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a number such as 1 or 2. Please try again.");
                return 0;
            }
        }

        public decimal inputDecimal() //0 is invalid input
        {
            try
            {
                return decimal.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a decimal number such as 25.54 or 300. Please try again.");
                return 0;
            }
        }

        public decimal inputDecimal(string msg)
        {
            Console.WriteLine(msg);
            Console.Write(">");
            decimal amount = this.inputDecimal();

            //if response is invalid redisplay 
            if (amount == 0)
            {
                Console.WriteLine("That was not a valid amount. Please try again");
                this.inputDecimal(msg);
            }
            return amount;
        }

        public void setUpProperties()
        {
            //Create instances of property factories
            LuckFactory luckFactory = new LuckFactory();
            ResidentialFactory resFactory = new ResidentialFactory();
            TransportFactory transFactory = new TransportFactory();
            UtilityFactory utilFactory = new UtilityFactory();
            PropertyFactory genericFactory = new PropertyFactory();
            JailFactory jailFactory = new JailFactory();
            Community_Chest_Factory ChestFactory = new Community_Chest_Factory();

            //Create properties and add them to the board
            //Loading property details from file has not been implemented
            //Property names are taken from the "Here and Now" New Zealand version of monopoly
            //Rents are tenth of cost of property
            //Colours have not been implemented
            Board.access().addProperty(luckFactory.create("Go", false, 200));
            Board.access().addProperty(resFactory.create("Ohakune Carrot", 60, 6, 50));
            
            Board.access().addProperty(luckFactory.create("Community Chest", false, 50)); // not properly implemented just 50 benefit
            Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50));
            Board.access().addProperty(luckFactory.create("Income Tax", true, 200));
             Board.access().addProperty(transFactory.create("Auckland International Airport"));
             Board.access().addProperty(resFactory.create("Te Papa", 100, 10, 50));
             Board.access().addProperty(luckFactory.create("Chance", true, 50)); // not properly implemented just 50 penalty
             Board.access().addProperty(resFactory.create("Waitangi Treaty Grounds", 100, 10, 50));
             Board.access().addProperty(resFactory.create("Larnach Castle", 120, 12, 50));
             Board.access().addProperty(jailFactory.create("Jail", false)); //not properly implemented just a property that does nothing
             Board.access().addProperty(resFactory.create("Cape Reinga Lighthouse", 140, 14, 100));
             Board.access().addProperty(utilFactory.create("Mobile Phone Company"));
             Board.access().addProperty(resFactory.create("Lake Taupo", 140, 14, 100));
             Board.access().addProperty(resFactory.create("Queenstown Ski Fields", 160, 16, 100));
             Board.access().addProperty(transFactory.create("Dunedin Railway Station"));
             Board.access().addProperty(resFactory.create("Fox Glacier", 180, 18, 100));
             Board.access().addProperty(luckFactory.create("Community Chest", false, 50)); // not properly implemented just 50 benefit
             Board.access().addProperty(resFactory.create("Milford Sound", 180, 18, 100));
             Board.access().addProperty(resFactory.create("Mt Cook", 200, 20, 100));
             Board.access().addProperty(genericFactory.create("Free Parking")); //not properly implemented just a property that does nothing
             Board.access().addProperty(resFactory.create("Ninety Mile Beach", 220, 22, 150));
             Board.access().addProperty(luckFactory.create("Chance", true, 50)); // not properly implemented just 50 penalty
             Board.access().addProperty(resFactory.create("Golden Bay", 220, 22, 150));
             Board.access().addProperty(resFactory.create("Moeraki Boulders, Oamaru", 240, 24, 150));
             Board.access().addProperty(transFactory.create("Port Tauranga"));
             Board.access().addProperty(resFactory.create("Waitomo Caves", 260, 26, 150));
             Board.access().addProperty(resFactory.create("Mt Maunganui", 260, 26, 150));
             Board.access().addProperty(utilFactory.create("Internet Service Provider"));
             Board.access().addProperty(resFactory.create("Art Deco Buildings, Napier", 280, 28, 150));
             Board.access().addProperty(jailFactory.create("Go to Jail", true)); //not properly implemented just a property that does nothing
             Board.access().addProperty(resFactory.create("Cable Cars Wellington", 300, 30, 200));
             Board.access().addProperty(resFactory.create("Cathedral Square", 300, 30, 200));
 //old            //Board.access().addProperty(luckFactory.create("Community Chest", false, 50)); // not properly implemented just 50 benefit
          //   Board.access().addProperty(Community_Chest_Factory.create("Community Chest"));
             Board.access().addProperty(resFactory.create("The Square, Palmerston North", 320, 32, 200));
             Board.access().addProperty(transFactory.create("Picton Ferry"));
             Board.access().addProperty(luckFactory.create("Chance", true, 50)); // not properly implemented just 50 penalty
             Board.access().addProperty(resFactory.create("Pukekura Park, Festival of Lights", 350, 35, 200));
             Board.access().addProperty(luckFactory.create("Super Tax", true, 100));
             Board.access().addProperty(resFactory.create("Rangitoto", 400, 40, 200));
             
            Console.WriteLine("Properties have been setup");
        }

        public void setUpPlayers()
        {
            //Add players to the board
            Console.WriteLine("How many players are playing?");
            Console.Write("(2-8)>");
            int playerCount = this.inputInteger();

            //if it is out of range then display msg and redo this method
            if ((playerCount < 2) || (playerCount > 8))
            {
                Console.WriteLine("That is an invalid amount. Please try again.");
                this.setUpPlayers();
            }

            //Ask for players names
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Please enter the name for Player {0}:", i + 1);
                Console.Write(">");
                string sPlayerName = Console.ReadLine();
                Player player = new Player(sPlayerName);
                //subscribe to events
                player.playerBankrupt += playerBankruptHandler;
                player.playerPassGo += playerPassGoHandler;
                //add player 
                Board.access().addPlayer(player);
                Console.WriteLine("{0} has been added to the game.", Board.access().getPlayer(i).getName());
            }

            Console.WriteLine("Players have been setup");
        }

        public string playerPrompt(int playerIndex)
        {
            return string.Format("{0}:\t", Board.access().getPlayer(playerIndex).getName());
        }

        public string playerPrompt(Player player)
        {
            return string.Format("{0}:\t", player.getName());
        }

        public bool getInputYN(Player player, string sQuestion)
        {
            Console.WriteLine(playerPrompt(player) + sQuestion);
            Console.Write("y/n>");
            string resp = Console.ReadLine().ToUpper();

            switch (resp)
            {
                case "Y":
                    return true;
                case "N":
                    return false;
                default:
                    Console.WriteLine("That answer cannot be understood. Please answer 'y' or 'n'.");
                    this.getInputYN(player, sQuestion);
                    return false;
            }
        }

        public void displayPlayerChoiceMenu(Player player)
        {
            int resp = 0;
            Console.WriteLine("\n{0}Please make a selection:\n", playerPrompt(player));
            Console.WriteLine("1. Finish turn");
            Console.WriteLine("2. View your details");
            Console.WriteLine("3. Purchase This Property");
            Console.WriteLine("4. Buy House for Property");
            Console.WriteLine("5. Trade Property with Player");
            Console.WriteLine("6. Mortgage Property");
            Console.Write("(1-6)>");
            //read response
            resp = inputInteger();
            //if response is invalid redisplay menu
            if (resp == 0)
                this.displayPlayerChoiceMenu(player);

            //perform choice according to number input
            switch (resp)
            {
                case 1:
                    break;
                case 2:
                    Console.WriteLine("==================================");
                    Console.WriteLine(player.FullDetailsToString());
                    Console.WriteLine("==================================");
                    this.displayPlayerChoiceMenu(player);
                    break;
                case 3:
                    this.purchaseProperty(player);
                    this.displayPlayerChoiceMenu(player);
                    break;
                case 4:
                    this.buyHouse(player);
                    this.displayPlayerChoiceMenu(player);
                    break;
                case 5:
                    this.tradeProperty(player);
                    this.displayPlayerChoiceMenu(player);
                    break;
                case 6:
                    this.mortgage_property(player);
                    this.displayPlayerChoiceMenu(player);
                    break;
                default:
                    Console.WriteLine("That option is not avaliable. Please try again.");
                    this.displayPlayerChoiceMenu(player);
                    break;
            }
        }

        public void purchaseProperty(Player player)
        {
            //if property available give option to purchase else so not available
            if (Board.access().getProperty(player.getLocation()).availableForPurchase())
            {
                TradeableProperty propertyLocatedOn = (TradeableProperty)Board.access().getProperty(player.getLocation());
                bool respYN = getInputYN(player, string.Format("'{0}' is available to purchase for ${1}. Are you sure you want to purchase it?", propertyLocatedOn.getName(), propertyLocatedOn.getPrice()));
                if (respYN)
                {
                    propertyLocatedOn.purchase(ref player);//purchase property
                    Console.WriteLine("{0}You have successfully purchased {1}.", playerPrompt(player), propertyLocatedOn.getName());
                }
            }
            else
            {
                Console.WriteLine("{0}{1} is not available for purchase.", playerPrompt(player), Board.access().getProperty(player.getLocation()).getName());
            }
        }

        public void buyHouse(Player player)
        {
            //create prompt
            string sPrompt = String.Format("{0}Please select a property to buy a house for:", this.playerPrompt(player));
            //create variable for propertyToBuy
            Residential propertyToBuyFor = null;
            if (player.getPropertiesOwnedFromBoard().Count == 0)
            {
                //write message
                Console.WriteLine("{0}You do not own any properties.", playerPrompt(player));
                //return from method
                return;
            }
            //get the property to buy house for
            Property property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);
            //if dont own any properties

            //check that it is a residential
            if (property.GetType() == (new Residential().GetType()))
            {
                //cast to residential property
                propertyToBuyFor = (Residential)property;
            }
            else //else display msg 
            {
                Console.WriteLine("{0}A house can no be bought for {1} because it is not a Residential Property.", this.playerPrompt(player), propertyToBuyFor.getName());
                return;
            }

            //check that max houses has not been reached
            if (propertyToBuyFor.getHouseCount() >= Residential.getMaxHouses())
            {
                Console.WriteLine("{0}The maximum house limit for {1} of {2} houses has been reached.", playerPrompt(player), propertyToBuyFor.getName(), Residential.getMaxHouses());
            }
            else
            {
                //confirm
                bool doBuyHouse = this.getInputYN(player, String.Format("You chose to buy a house for {0}. Are you sure you want to purchase a house for ${1}?", propertyToBuyFor.getName(), propertyToBuyFor.getHouseCost()));
                //if confirmed
                if (doBuyHouse)
                {
                    //buy the house
                    propertyToBuyFor.addHouse();
                    Console.WriteLine("{0}A new house for {1} has been bought successfully", playerPrompt(player), propertyToBuyFor.getName());
                }
            }
        }

        public void tradeProperty(Player player)
        {
            //create prompt
            string sPropPrompt = String.Format("{0}Please select a property to trade:", this.playerPrompt(player));
            //create prompt
            string sPlayerPrompt = String.Format("{0}Please select a player to trade with:", this.playerPrompt(player));

            //get the property to trade
            TradeableProperty propertyToTrade = (TradeableProperty)this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPropPrompt);

            //if dont own any properties
            if (propertyToTrade == null)
            {
                //write message
                Console.WriteLine("{0}You do not own any properties.", playerPrompt(player));
                //return from method
                return;
            }

            //get the player wishing to trade with
            Player playerToTradeWith = this.displayPlayerChooser(Board.access().getPlayers(), player, sPlayerPrompt);

            //get the amount wanted

            string inputAmtMsg = string.Format("{0}How much do you want for this property?", playerPrompt(player));

            decimal amountWanted = inputDecimal(inputAmtMsg);

            //confirm with playerToTradeWith
            //set console color
            ConsoleColor origColor = Console.ForegroundColor;
            int i = Board.access().getPlayers().IndexOf(playerToTradeWith);
            Console.ForegroundColor = this.colors[i];
            //get player response
            bool agreesToTrade = getInputYN(playerToTradeWith, string.Format("{0} wants to trade '{1}' with you for ${2}. Do you agree to pay {2} for '{1}'", player.getName(), propertyToTrade.getName(), amountWanted));
            //resent console color
            Console.ForegroundColor = origColor;
            if (agreesToTrade)
            {
                Player playerFromBoard = Board.access().getPlayer(playerToTradeWith.getName());
                //player trades property

                player.tradeProperty(ref propertyToTrade, ref playerFromBoard, amountWanted);
                Console.WriteLine("{0} has been traded successfully. {0} is now owned by {1}", propertyToTrade.getName(), playerFromBoard.getName());
            }
            else
            {
                //display rejection message
                Console.WriteLine("{0}{1} does not agree to trade {2} for ${3}", playerPrompt(player), playerToTradeWith.getName(), propertyToTrade.getName(), amountWanted);
            }
        }

        public Property displayPropertyChooser(ArrayList properties, String sPrompt)
        {
            //if no properties return null
            if (properties.Count == 0)
                return null;
            Console.WriteLine(sPrompt);
            for (int i = 0; i < properties.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, properties[i].ToString());
            }
            //display prompt
            Console.Write("({0}-{1})>", 1, properties.Count);
            //get input
            int resp = this.inputInteger();

            //if outside of range
            if ((resp < 1) || (resp > properties.Count))
            {
                Console.WriteLine("That option is not avaliable. Please try again.");
                this.displayPropertyChooser(properties, sPrompt);
                return null;
            }
            else
            {
                //return the appropriate property
                return (Property)properties[resp - 1];
            }
        }

        public Player displayPlayerChooser(ArrayList players, Player playerToExclude, String sPrompt)
        {
            //if no players return null
            if (players.Count == 0)
                return null;
            Console.WriteLine(sPrompt);
            //Create a new arraylist to display
            ArrayList displayList = new ArrayList(players);

            //remove the player to exlude
            displayList.Remove(playerToExclude);

            //go through and display each
            for (int i = 0; i < displayList.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, displayList[i].ToString());
            }
            //display prompt
            Console.Write("({0}-{1})>", 1, displayList.Count);
            //get input
            int resp = this.inputInteger();

            //if outside of range
            if ((resp < 1) || (resp > displayList.Count))
            {
                Console.WriteLine("That option is not avaliable. Please try again.");
                this.displayPlayerChooser(players, playerToExclude, sPrompt);
                return null;
            }
            else
            {
                Player chosenPlayer = (Player)displayList[resp - 1];
                //find the player to return
                foreach (Player p in players)
                {
                    if (p.getName() == chosenPlayer.getName())
                        return p;
                }
                return null;
            }
        }

        public static void playerBankruptHandler(object obj, EventArgs args)
        {
            //cast to player
            Player p = (Player)obj;
            //display bankrupt msg
            Console.WriteLine("{0} IS BANKRUPT!", p.getName().ToUpper());

        }

        public static void playerPassGoHandler(object obj, EventArgs args)
        {
            Player p = (Player)obj;
            Console.WriteLine("{0} has passed go.{0} has received $200", p.getName());
        }

        //Allow player to mortgage property
        public void mortgage_property(Player player)
        {

            string sPrompt = String.Format("{0}Please select a property to mortgage:", this.playerPrompt(player));
            
            //Get the selected property to mortgage
            Property selected_property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);
          
            if (player.getPropertiesOwnedFromBoard().Count == 0)
            {
                //write message
                Console.WriteLine("{0}You do not have any properties to mortgage! ", playerPrompt(player));
                //return from method
                return;
            }
            if (selected_property.isMortgaged == true)
            {

                Console.WriteLine(selected_property.getName().ToString() + " has already been mortgaged! ");
            }
                
            else
            {

            
              decimal mortgage = selected_property.getMortgageValue();
              selected_property.mortgage_Property();

          
           Console.WriteLine("You have mortgaged" + selected_property.getName() + "and have been paid" + mortgage);
            }
        }
     
        public void un_mortgage_property(Player player)
        {

            string sprompt = string.Format("Please select a property to un-mortgage:", this.playerPrompt(player));
            // getPropertiesOwnedAndMortgaged() had to be created to allow us to get a list of all mortgaged properties
            //owned by the current player
            Property selected_property = this.displayPropertyChooser(player.getPropertiesOwnedAndMortgaged(), sprompt);

            //check that the player has mortgaged properties

            if (player.getPropertiesOwnedAndMortgaged().Count == 0)
            {

                Console.WriteLine("You have not mortgaged any properties  ", playerPrompt(player));
            }

            selected_property.un_mortgage_Property();
        }


    }
}

