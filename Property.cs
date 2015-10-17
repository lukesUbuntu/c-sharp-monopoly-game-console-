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
        
        public virtual void mortgage_Property()
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
        public virtual void un_mortgage_Property()
        {
            if (isMortgaged == true)
            {
                isMortgaged = false;
            }
            else {
                Console.WriteLine("This property has not been mortgaged! ");
            }

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

   

