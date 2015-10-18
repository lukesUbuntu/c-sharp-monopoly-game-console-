using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
     /// <summary>
    /// Class that represents a generic property types
    /// </summary>
    /// 
    public class Property
    {
        protected string sName;
        protected Trader owner;
        protected decimal dPrice;
        protected decimal dMortgageValue;
        protected decimal dRent;
        public bool isMortgaged;
        public Property(): this("Property"){}

        public Property(string sName)
        {
            this.sName = sName;
            this.owner = Banker.access();
           
        }


        public Property(string sName, ref Trader owner)
        {
          
            this.sName = sName;
            this.owner = owner;
         
        }
        public Trader getOwner()
        {
            return this.owner;
        }

        public void setOwner(ref Banker newOwner)
        {
            this.owner = newOwner;
        }

        public void setOwner(ref Player newOwner)
        {
            this.owner = newOwner;
        }

        public string getName()
        {
            return this.sName;
        }

        public virtual string landOn(ref Player player)
        {
            return String.Format("{0} landed on {1}. ", player.getName(), this.getName());
        }

        public override string ToString()
        {
            return String.Format("{0}:\tOwned by: {1}", this.getName(), this.getOwner().getName());
        }

        public virtual bool availableForPurchase()
        {
            return false;//generic properties are not available for purchase
        }
        
        public virtual void mortgageProperty()
        {
            
            
            if (isMortgaged == false)
            {

                this.getOwner().receive(this.dMortgageValue);
                Banker.access().pay(this.dMortgageValue);
                this.isMortgaged = true;
            }
            else
            {

                Console.WriteLine("This property has already been mortgaged! ");
            }
            
        }

        // un mortgage property
        public virtual void unMortgageProperty()
        {
           
          

                if (this.getOwner().getBalance() <= (this.mortgage_value() * 10 / 100))
                {

                    Console.WriteLine("You do not have enough money to pay for this mortgage!");
                }

                payMortgage();
            
            

        }

        private void payMortgage()
        {
            this.getOwner().pay(this.mortgage_value() * 10 / 100);
            Console.WriteLine("You have paied: " + this.mortgage_value() * 10 / 100 + "you have now paied off your mortgage!");
            this.isMortgaged = false;
        }

        public virtual bool check_mortgaged_status() {
           
            return isMortgaged;
        }

        public virtual decimal getMortgageValue()
        {
            return this.dMortgageValue;
        }
        //Get the mortgage value and return it
        public virtual decimal mortgage_value()
        {
            
            return this.dMortgageValue;
        }

       
        }
    }

   

