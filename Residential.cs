using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class Residential : TradeableProperty
    {
        
        decimal dHouseCost;
        int iHouses;
        static int iMaxHouses = 4;

        //int iHotels; //not implemented

        public Residential() : this("Residential Property"){}

        public Residential(String sName) : this(sName, 200, 50, 50) { }

        public Residential(String sName, decimal dPrice, decimal dRent, decimal dHouseCost)
        {
            this.sName = sName;
            this.dPrice = dPrice;
            this.dMortgageValue = dPrice;
            this.dRent = dRent;
            this.dHouseCost = dHouseCost;
        }

        

        public override decimal getRent()
        {
            //rent is rental amount plus the rental amount for each house
            return (dRent + (dRent * iHouses));
        }


        public void addHouse()
        {
            // pay for houses
            this.getOwner().pay(this.dHouseCost);
            //add houses to residental
            this.iHouses ++;
        }

        public decimal getHouseCost()
        {
            return this.dHouseCost;
        }

        public int getHouseCount()
        {
            return this.iHouses;
        }

        public static int getMaxHouses()
        {
            return iMaxHouses;
        }

        public override string ToString()
        {
           return base.ToString()  + string.Format("\tHouses: {0}", this.getHouseCount());
        }

        public virtual void mortgage_Property()
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

        }
        public virtual void un_mortgage_Property()
        {
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
