using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class TradeableProperty : Property //should be abstract but not to make testing easier
    {
       
   
        public TradeableProperty()
        {
            this.dPrice = 200;
            this.dMortgageValue = 100;
            this.dRent = 50;
        }

        public decimal getPrice()
        {
            return dPrice;
        }

        public virtual decimal getRent()
        {
            return this.dRent;
        }

        public virtual void payRent(ref Player player)
        {
            //Need to check if the property has been mortgage before we change the player rent.
            if (this.isMortgaged == false)
            {
                player.pay(this.getRent());
                this.getOwner().receive(this.getRent());
            }
            //Do not have to pay rent
            else
            {
                //Should change this to an exception
                Console.WriteLine("This property has been mortgaged, you do not need to pay rent: ");
            }
        }

        public void purchase(ref Player buyer)
        {
            //check that it is owned by bank
            if (this.availableForPurchase())
            {
                //pay price 
                buyer.pay(this.getPrice());
                //set owner to buyer
                this.setOwner(ref buyer);
            }
            else
            {
                throw new ApplicationException("The property is not available from purchase from the Bank.");
            }
        }

        public override bool availableForPurchase()
        {
            //if owned by bank then available
            if (this.owner == Banker.access())
                return true;
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string landOn(ref Player player)
        {
            //Pay rent if needed, you will only pay rent when the property is owned by a player that is not the current player and the propety has not been mortgaged.
            if ((this.getOwner() != Banker.access()) && (this.getOwner() != player) && (this.isMortgaged = false))
            {
               
                //pay rent
                this.payRent(ref player);
                return base.landOn(ref player) + string.Format("Rent has been paid for {0} of ${1} to {2}.", this.getName(), this.getRent(), this.getOwner().getName());
            }
            else
                return base.landOn(ref player);
        }

     /*   public  void mortgage_Property()
        {
            if (isMortgaged == false)
            {
                this.getOwner().pay(this.dMortgageValue);
                Banker.access().pay(this.dMortgageValue);
                isMortgaged = true;
            }
            else
            {

                Console.WriteLine("This property has already been mortgaged! ");
            }

        }*/
        public override void un_mortgage_Property()
        {


            this.getOwner().pay(this.dMortgageValue);
            Banker.access().pay(this.dMortgageValue);
            isMortgaged = true;


            if (isMortgaged == true)
            {
                isMortgaged = false;
            }
            else
            {
                //not sure if I need this as the choice to un mortgage a property should only be avalible to a mortgaged property
                Console.WriteLine("This property has not been mortgaged! ");
            }

        }
    

    }
}
