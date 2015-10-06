using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class TradeableProperty : Property //should be abstract but not to make testing easier
    {
        protected decimal dPrice;
        protected decimal dMortgageValue;
        protected decimal dRent;

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
            //Pay rent if needed
            if ((this.getOwner() != Banker.access()) && (this.getOwner() != player))
            {
                //pay rent
                this.payRent(ref player);
                return base.landOn(ref player) + string.Format("Rent has been paid for {0} of ${1} to {2}.", this.getName(), this.getRent(), this.getOwner().getName());
            }
            else
                return base.landOn(ref player);
        }
    }
}
