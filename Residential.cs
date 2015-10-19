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

        int iHotels;

        public Residential() : this("Residential Property"){}

        public Residential(String sName) : this(sName, 200, 50, 50) { }

        public Residential(String sName, decimal dPrice, decimal dRent, decimal dHouseCost)
        {
            this.sName = sName;
            this.dPrice = dPrice;

            this.dMortgageValue = dPrice / 2;
            this.dRent = dRent;
            this.dHouseCost = dHouseCost;
        }
        public void SellHouses()
        {
            if (this.getHouseCount() != 0)
            {
                this.owner.receive(dHouseCost / 2);
                iHouses --;
                
            }
            
        }

        public override decimal getMortgageValue()
        {
            return this.dMortgageValue;
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

        //add hotel
        public void addHotel()
        {
            // if all the houses are owned
            if (iHouses == iMaxHouses)
            {
                //pay for hotel
                this.getOwner().pay(this.dHouseCost * 2);
                //add to the hotel counter
                this.iHotels++;
                //remove the houses
                this.iHouses = 0;

            }

        }

        public decimal getHotelCount()
        {
           return this.iHotels;
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

        //override the mortgage_property function from property as we need to check if the property has houses 
        //or hotels
        public override void mortgageProperty()
        {
            //Check if the property has house and hotels
            if (this.getHouseCount() != 0 && this.getHotelCount() != 0)
            {

                Console.WriteLine("You must sell your houses and hotels before you can mortgage this property!");
            }


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
    
    
    }
}
