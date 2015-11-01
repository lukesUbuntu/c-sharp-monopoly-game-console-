using System;
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
                          Name = "Take a ride on the Reading Railroad If you pass go collect $200 ",
                          Action = Rail_Ride

                    },
                new Actionable
                {
                    Name = "Bank pays you dividend of $50",
                    Action = dividend
                },
                
                new Actionable
                {
                    Name = "Advance to Illinois Avenue ",
                    Action = Advance_To
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
                    Name = "Make general repairs on all your property Pay $25 for each house Pay $100 for each hotel ",
                    Action = property_repairs
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
                    Name = "Take a walk on the Boardwalk ",
                    Action = walk_Boardwalk
                },
                 new Actionable
                {
                    Name = "Advance to St. Charles Place ",
                    Action = Advance_To_Two
                },
                 new Actionable
                {
                    Name = "You have been elected chairman of the board Pay each player $50 ",
                    Action = chairman
                },
                 new Actionable
                {
                    Name = "Advance token to nearest utility If unowned, you may buy it from the bank If owned, throw the dice and pay owner a total of 10 times the amount thrown ",
                    Action = nearest_utility
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
        

   
        /**
         * Card Methods below here 
         */
        public void Doctor_fees()
        {

            //Console.WriteLine("Doctor's fees – Pay $50 ");
            current_player.pay(50);
            the_bank.receive(50);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
            Console.ReadLine();

        }

        public void advance_to_go()
        {
            current_player.setLocation(0, false);
            //Console.WriteLine("Advance straight to GO");
        }

        public void your_birthday()
        {
            //collect $10 from all players on board
            foreach (Player otherPlayer in Board.access().getPlayers())
            {
                if (otherPlayer != current_player)
                {
                    otherPlayer.pay(10);
                    Console.WriteLine(String.Format("{0} paid you $10 ", otherPlayer.getName()));
                    current_player.receive(10);
                }

            }

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

        public void Tax_refund()
        {

            current_player.receive(20);
            the_bank.pay(20);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());

        }

        public void Hospital_Fees()
        {
            current_player.pay(100);
            the_bank.receive(100);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }

        public void School_Fees()
        {
            current_player.pay(50);
            the_bank.receive(50);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }

        public void Consultancy_Fee()
        {
            current_player.receive(25);
            the_bank.pay(25);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }
        public void beauty_contest()
        {
            current_player.receive(10);
            the_bank.pay(10);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }
        public void inherit()
        {
            current_player.receive(100);
            the_bank.pay(100);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }
        public void sale_of_stock()
        {
            current_player.receive(50);
            the_bank.pay(50);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }

        public void Holiday_Fund()
        {
            current_player.receive(100);
            the_bank.pay(100);
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }
        public void street_repairs()
        {
            Console.WriteLine("Your new balance is \n" + current_player.getBalance());
        }









    }
}
