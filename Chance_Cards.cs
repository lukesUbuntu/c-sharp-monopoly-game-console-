using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame
{
    class Chance_Cards : Property
    {


         public Player current_player;
        public Banker the_bank;
        public string sName;
        private List<Actionable> Chance_Cards_Actions;
        private Random rng = new Random();
        private int cardPulled = 0;
        
        public Chance_Cards() : this("Chance Cards") {}


        public Chance_Cards(string sName)
        {
            this.sName = sName;
            this.Chance_Cards_Actions = Shuffle(CardList());
        }

        public override string landOn(ref Player player)
        {
            //if we have pulled the complete deck we need to reshuffle
            if (this.cardPulled >= this.Chance_Cards_Actions.Count) this.ShuffleCards();

            the_bank = Banker.access();
            string drawedCord = draw_card(player);
            return base.landOn(ref player) + String.Format(drawedCord);

        }

        public void ShuffleCards()
        {
            Console.Write(String.Format("Shuffling {0} cards  ", this.sName));
            List<Actionable> Chance_Cards_Actions = Shuffle(CardList());
            Console.Write("Shuffled");
        }
        //info for creating a list of actions was taken from http://stackoverflow.com/questions/4910775/can-a-list-hold-multiple-void-methods

        //Deck of comunity chest cards



        public List<Actionable> CardList()
        {

            List<Actionable> Chance_Cards_Actions = new List<Actionable>
          
            {
                new Actionable
                    {
                          Name = "Take a ride on the Picton Ferry If you pass go collect $200 ",
                          Action = Picton_Ferry

                    },
                new Actionable
                {
                    Name = "Bank pays you dividend of $50",
                    Action = dividend
                },
                
                new Actionable
                {
                    Name = "Advance to Cathedral Square ",
                    Action = Cathedral_Square
                },
                 new Actionable
                {
                    Name = "Your building and loan matures Collect $150  ",
                    Action = loan_matures
                },
                 new Actionable
                {
                    Name = "Get out of jail free card",
                    Action = Jail_Free
                },
                
                 new Actionable
                {
                    Name = "Advance token to the nearest railroad and pay the owner twice the rental to which he is otherwise entitled. If railroad is unowned, you may buy it from the bank.",
                    Action = nearest_railroad
                },
                 new Actionable
                {
                    Name = "Pay poor tax of $15",
                    Action = poor_tax
                },
                 new Actionable
                {
                    Name = "You have been elected chairman of the board Pay each player $50 ",
                    Action = chairman
                },
                
                 new Actionable
                {
                    Name = "Go back 3 spaces",
                    Action = Go_back
                },
                 new Actionable
                {
                    Name = "Advance to Go Collect $200 dollars",
                    Action = Advance_to_Go
                },
                 new Actionable
                {
                    Name = "Go directly to jail",
                    Action = Go_to_Jail
                }
                
            };


            return Chance_Cards_Actions;
        }
        //confirmed that this action runs

        public string draw_card(Player player)
        {
  
            ///Community_Cards_Actions
            current_player = player; //Current Player
            Actionable cardPull = Chance_Cards_Actions[this.cardPulled++];
            cardPull.Action.Invoke();
            return cardPull.Name.ToString();
            //return ("need to figure out how to not bother returning anything");
        }

        

        private List<Actionable> Shuffle(List<Actionable> list)
        {

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Actionable value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }


        public void Picton_Ferry()
        {
            int x = 0;
            foreach (Property theProp in Board.access().getProperties())
            {
                if (theProp.getName() == "Picton Ferry")
                {
                    current_player.setLocation(x, false);
                    break;

                }
                    
                x++;
            }

        }
        /**
         * Card Methods below here 
         */
        public void dividend()
        {

            //Console.WriteLine("Doctor's fees – Pay $50 ");
            current_player.pay(50);
            the_bank.receive(50);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
            Console.ReadLine();

        }
        public void Cathedral_Square()
        {
            int x = 0;
            foreach (Property theProp in Board.access().getProperties())
            {
                if (theProp.getName() == "Cathedral Square")
                {
                    current_player.setLocation(x, false);
                    break;

                }

                x++;
            }
        }
        public void loan_matures()
        {
            current_player.pay(150);
            the_bank.receive(150);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
            Console.ReadLine();
        }
        public void Jail_Free()
        {
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }


        public void nearest_railroad()
        {
            int x = 0;
            foreach (Property theProp in Board.access().getProperties())
            {
                if (theProp.GetType() == (new Transport().GetType()))
                {
                    current_player.setLocation(x, false);
                    break;

                }

                x++;
            }
        }
        public void poor_tax()
        {
            current_player.pay(15);
            the_bank.receive(15);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
            Console.ReadLine();
        }
        public void Advance_to_Go()
        {

           
            current_player.setLocation(0, false);
            //Console.WriteLine("Advance straight to GO");
        }

        public void chairman()
        {
            //pay everyone 50 from all players on board
            foreach (Player otherPlayer in Board.access().getPlayers())
            {
                if (otherPlayer != current_player)
                {
                    otherPlayer.receive(50);
                    Console.WriteLine(String.Format("{0} paid you $50 ", otherPlayer.getName()));
                    current_player.pay(50);
                }

            }

        }
        public void Go_back()
        {
            current_player.setLocation(current_player.getLocation() - 3, false);
        }
        public void Opera_Night()
        {
            foreach (Player otherPlayer in Board.access().getPlayers())
            {
                if (otherPlayer != current_player)
                {
                    otherPlayer.pay(50);
                    Console.WriteLine(String.Format("{0} paid you $50 ", otherPlayer.getName()));
                    current_player.receive(50);
                }

            }
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }


        public void Go_to_Jail() 
        {
            current_player.setIsInJail();
            Console.WriteLine("Your in jail");
        }













    }
}
