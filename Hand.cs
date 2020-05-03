using System;

namespace EricsBlackJack
{
    class Hand
    {
        String NameofPlayer; //name of player for Hand setter
        Card[] MyCards = new Card[5]; //creates an array of 5 cards for players hand
        int totalvalue = 0; //total value of cards in hand
        int numberofcards = 0; //counter for the number of cards in players hand
        Cards cardValue = new Cards(); //variable for card value of ace not in use yet
        bool isAce = false;
        bool isCard = false;
        int Value;
        private bool blackJackMarker = false;
        public Hand(string Name) //setter for player name
        {
            NameofPlayer = Name;
        }
        public int DealACardtoMe(Card aCard) //deal a card method requires card from card.cs
        {
            if (numberofcards < 5)
            {
                MyCards[numberofcards] = aCard; //counter for the number of cards in hand array
                totalvalue = totalvalue + aCard.GetCardValue(); //calls get card value from card.cs to add to total value
                numberofcards++; //increments array counter 
            }
            return totalvalue;
        }
        public int GetNumberofCards() //getter for number of cards in hand
        {
            return numberofcards;
        }
        public Card GetaCard(int index) //gett for card from deck in cards.cs
        {
            return MyCards[index];
        }
        public int getCardValue(int index)
        {
            return MyCards[index].GetCardValue();
        }
        public int GetTotalValue() //getter for total value to be used in determining winner
        {
            totalvalue = 0;
            for (int i = 0; i < numberofcards; i++)
            {
                totalvalue = totalvalue + MyCards[i].GetCardValue();
            }
            return totalvalue;
        }
        public void ResetHand() //getter to reset hand after game is over
        {
            totalvalue = 0;
            numberofcards = 0;
            blackJackMarker = false;
            
            for (int i = 0; i < 5; i++)
            {
                if(MyCards[i] != null)
                {
                    if (isCardAce(i) == true)
                    {
                        MyCards[i].resetCardValue();
                    }
                }
                
                MyCards[i] = null;
            }

        }

        public void setAce(int index)
        {
            Value = MyCards[index].SetCardValue();
        }

        public void resetAce(int index)
        {
            Value = MyCards[index].resetCardValue();
        }
        public bool isCardAce(int index)
        {
            isAce = MyCards[index].isCardAce();
            return isAce;
        }
        public bool setCard(int index)
        {
            isCard = true;
            return isCard;
        }
        public void blackJack()
        {
            blackJackMarker = true;
        }
        public bool checkBlackJack()
        {
            return blackJackMarker;
        }
    }
}
