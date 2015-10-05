using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame
{
    class Community_Chest
    {
        protected Player this_player; //Current Player

       // protected static Community_Chest Community_Chest_Cards() { }
        //info for creating a list of actions was taken from http://stackoverflow.com/questions/4910775/can-a-list-hold-multiple-void-methods

      //Deck of comunity chest cards
           
         List<Actionable> Community_Cards_Actions = new List<Actionable>
           
            {
                new Actionable
                    {
                          Name = "Doctor's fees – Pay $50 ",
                          Action = Card_Actions.Doctor_fees

                    },
                new Actionable
                {
                    Name = "Get out of jail free",
                    Action = Card_Actions.Get_out_of_jail_free
                },
                new Actionable
                {
                    Name = "Go to jail – go directly to jail – Do not pass Go, do not collect $200",
                    Action = Card_Actions. Go_to_jail
                },
                new Actionable
                {
                    Name = "It is your birthday Collect $10 from each player ",
                    Action = Card_Actions.birthday
                },
                 new Actionable
                {
                    Name = "Grand Opera Night – collect $50 from every player for opening night seats ",
                    Action = Card_Actions.Opera_Night
                },
                 new Actionable
                {
                    Name = "Income Tax refund – collect $20",
                    Action = Card_Actions.Tax_refund
                },
                 new Actionable
                {
                    Name = "Pay Hospital Fees of $100 ",
                    Action = Card_Actions.Hospital_Fees
                },
                 new Actionable
                {
                    Name = "Pay School Fees of $50",
                    Action = Card_Actions.School_Fees
                },
                 new Actionable
                {
                    Name = "Receive $25 Consultancy Fee",
                    Action = Card_Actions.Consultancy_Fee
                },
                 new Actionable
                {
                    Name = "You are assessed for street repairs – $40 per house, $115 per hotel ",
                    Action = Card_Actions.street_repairs
                },
                 new Actionable
                {
                    Name = "You have won second prize in a beauty contest– collect $10 ",
                    Action = Card_Actions.beauty_contest
                },
                 new Actionable
                {
                    Name = "You inherit $100 ",
                    Action = Card_Actions.inherit
                },
                 new Actionable
                {
                    Name = "From sale of stock you get $50 ",
                    Action = Card_Actions.sale_of_stock
                },
                 new Actionable
                {
                    Name = "Holiday Fund matures - Receive $100",
                    Action = Card_Actions.Holiday_Fund
                }

            };


         public virtual string draw_card(ref Player player)
         {
             this_player = player;//Current Player
             
          
             return ("Card action was");
         }
       

     public class Actionable
    {
        public string Name { get; set; }
        public Action Action { get; set; }
    }
        public static class Card_Actions
    {
        public static void Doctor_fees()
        {
            
            
        }
       public static void Get_out_of_jail_free(){
           //Logic for each card will go here
       }
            public static void Go_to_jail(){

       }
            public static void birthday(){

       }
            public static void Opera_Night(){

       }
            public static void Tax_refund(){

       }
            public static void Hospital_Fees(){

       }
            public static void School_Fees(){

       }
            public static void Consultancy_Fee(){

       }
            public static void street_repairs(){

       }
               public static void beauty_contest(){

       }
               public static void inherit(){

       }
               public static void sale_of_stock(){



       }
        
               public static void Holiday_Fund(){

       }
             
       }
        
    }

   






    }

    


