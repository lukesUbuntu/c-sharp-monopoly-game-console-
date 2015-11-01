using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MolopolyGame
{
   
    /// <summary>
    /// Class for players playing monopoly
    /// </summary>
    
    public class Player : Trader
    {
        private int location;
        private int lastMove;
        private bool inJail;
        public int rollCount;
        public int rollDoubleCount;
        public bool sendMsg;
        public bool first = false;

        //each player has two dice
        public Die die1 = new Die();
        public Die die2 = new Die();
        bool isInactive = false;

        //event for playerBankrupt
        public event EventHandler playerBankrupt;
        public event EventHandler playerPassGo;

        public Player()
        {
            this.sName = "Player";
            this.dBalance = InitialValuesAccessor.getPlayerStartingBalance();
            this.location = 0;
            this.inJail = false;
        }

        public Player(string sName)
        {
            this.sName = sName;
            this.dBalance = InitialValuesAccessor.getPlayerStartingBalance();
            this.location = 0;
            this.inJail = false;
        }


        public Player(string sName, decimal dBalance) : base(sName, dBalance)
        {
            this.location = 0;
        }
  
        public void move()
        {
            die1.roll();
            die2.roll();
            this.first = false;
            if (this.getJailStatis() == true)
            {
                Console.WriteLine("in jail so didnt move");
                diceRollingToString();
            }
            else
            {
             
                //move distance is total of both throws
                int iMoveDistance = die1.numberLastRolled() + die2.numberLastRolled();
                
                //increase location
                this.setLocation(this.getLocation() + iMoveDistance, false);
                this.lastMove = iMoveDistance;
            }
           
        }

        public int getLastMove()
        {
            return this.lastMove;
        }

        public bool getJailStatis() {
        
        return this.inJail;
        }

        public string BriefDetailsToString()
        {
            return String.Format("You are on {0}.\tYou have ${1}.", Board.access().getProperty(this.getLocation()).getName(), this.getBalance());
        }

        public override string ToString()
        {
            return this.getName();
        }

        public string FullDetailsToString()
        {
            return String.Format("Player:{0}.\nBalance: ${1}\nLocation: {2} (Square {3}) \nProperties Owned:\n{4} \nIn Jail? {5}", this.getName(), this.getBalance(), Board.access().getProperty(this.getLocation()), this.getLocation(), this.PropertiesOwnedToString(), this.getJailStatis().ToString());
        }

        public string PropertiesOwnedToString()
        {
            string owned = "";
            //if none return none
            if (getPropertiesOwnedFromBoard().Count == 0)
                return "None";
            //for each property owned add to string owned
            for (int i = 0; i < getPropertiesOwnedFromBoard().Count; i++)
            {
                owned = getPropertiesOwnedFromBoard()[i].ToString() + "\n";
            }
            return owned;
        }

        public void setLocation(int location, bool playerpaPassGo)
        {
           
            //if set location is greater than number of squares then move back to beginning
            if (location >= Board.access().getSquares())
            {
                location = (location - Board.access().getSquares());
                //raise the pass go event if subscribers
                if(playerPassGo != null)
                    this.playerPassGo(this, new EventArgs());
                //add 200 for passing go
                this.receive(200);
            }

            this.location = location;
        }
        public void attemptRollDouble()
        {
            die1.roll();
            die2.roll();

            if (die1.numberRolled != die2.numberRolled)
            {
                this.setIsNotInJail();
                Console.WriteLine("You have rolled doubles and are no longer in jail!");
            }
            else
            {
                Console.WriteLine("You did not roll a double you are still in jail!");


            }
           
        }

        public int getLocation()
        {
            return this.location;
        }

        //check the dice roll for doubles 
        public bool CheckForDouble() {

            
            int dice_1 = Int32.Parse(die1.ToString());
            int dice_2 = Int32.Parse(die2.ToString());

           

            if (dice_1 == dice_2 && dice_2 == dice_1)
            {
                
                if (this.getJailStatis() == true)
                {
                    this.setIsNotInJail();
                    Console.WriteLine("You have rolled doubles and are no longer in jail!");
                    
                    
                }

                rollDoubleCount++;

                if (rollDoubleCount >= 3)
                {
                    this.first = true;
                    this.setIsInJail();
                    this.setLocation(10, false);
                    sendMsg = false;
                    Console.WriteLine("You have rolled doubles 3 times in a row and have been sent to jail!");
                    
                    rollDoubleCount = 0;

                }
                return true;
            }

            else
            {
                return false;
            }
        }

        public string diceRollingToString()
        {
            return String.Format("Rolling Dice:\tDice 1: {0}\tDice 2: {1}", die1, die2); 
        }
      

        public ArrayList getPropertiesOwnedFromBoard()
        {
            ArrayList propertiesOwned = new ArrayList();
            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                //owned by this player
                if (Board.access().getProperty(i).getOwner() == this)
                {
                    //add to arraylist
                    propertiesOwned.Add(Board.access().getProperty(i));
                }
            }
            return propertiesOwned;
        }
        // get a list of all properties that are owned by the player and are mortgaged 
        public ArrayList getPropertiesOwnedAndMortgaged()
        {
            ArrayList propertiesMortgaged = new ArrayList();
            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                //owned by this player and mortgaged
                if (Board.access().getProperty(i).getOwner() == this && Board.access().getProperty(i).isMortgaged == true)
                {
                    //add to arraylist
                    propertiesMortgaged.Add(Board.access().getProperty(i));
                }
            }
            return propertiesMortgaged;
        }

        public ArrayList getPropertiesOwnedFromBoardWithHouses()
        {
            ArrayList propertiesOwned = new ArrayList();
            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                //owned by this player
                if (Board.access().getProperty(i).getOwner() == this )
                {
                    
                    //add to arraylist
                    propertiesOwned.Add(Board.access().getProperty(i));
                }
            }
            return propertiesOwned;
        }
        public void payJailFine()
        {
            this.pay(50);
            Banker.access().receive(50);
            this.setIsNotInJail();
            Console.WriteLine("you not in jail");


        }

        public override void checkBankrupt()
        {
            if (this.getBalance() <= 0)
            {
                //raise the player bankrupt event if there are subscribers
                if (playerBankrupt != null)
                    this.playerBankrupt(this, new EventArgs());

                //return all the properties to the bank
                Banker b = Banker.access();
                foreach (Property p in this.getPropertiesOwnedFromBoard())
                {
                    p.setOwner(ref b);
                }
                //set isInactive to true
                this.isInactive = true;


            }
        }

        //send player to jail
        public void setIsInJail() {
           this.first = true;
           this.inJail = true;
           this.setLocation(10, true);
        }
        public void setIsNotInJail()
        {
            this.setLocation(10, false);
            this.inJail = false;
        }

        

        public bool isNotActive()
        {
            return this.isInactive;
        }

       

    }
}