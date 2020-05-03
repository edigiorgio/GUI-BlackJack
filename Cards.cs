using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace EricsBlackJack
{
    class Cards
    {
        public Card[] allCards = new Card[52]; //array of cards
        Random r = new Random(); //random generator for shuffle method
        int currentCard = 0; //counter for cards drawn from deck
        private int tries = 0;
        public Cards()
        {
            LoadCards(); //initializes cards
        }
        public void LoadCards()
        {
            Card aCard; //card object created from card.cs
            string msg = "";
            try
            {
                string[] list = Directory.GetFiles(@"cards", "*.gif");
                for (int index = 0; index < 52; index++) //for loop to populate deck
                {
                    int value = GetNextCardValue(index); //sets the cards value 
                    Image image = Image.FromFile(list[index]); //sets the cards image

                    aCard = new Card(image, value); //initializes the new card
                    if (index > 31 && index < 36)
                    {
                        aCard.SetCardToAce();
                        aCard.setCard();
                    }

                    allCards[index] = aCard;
                }
            }
            catch(Exception exception1)
            {
                Exception exception = exception1;
                if (tries >= 2)
                {
                    Environment.Exit(1);
                }
                else
                {
                    msg = string.Concat("Error Please make sure the card files in the Directory. \nWhen you put the cards in the Directory hit OK button.\n\n ", exception.ToString());
                    MessageBox.Show(msg);
                    tries++;
                    LoadCards();
                }
            }
            ShuffleCards(allCards); //shuffles the newly created deck

        }
        public Image setBackImage()
        {
            string[] list2 = Directory.GetFiles(@"cards", "Wfswbackcard*.gif");
            Image Backimage = Image.FromFile(list2[0]);
            Card ACardBack = new Card(Backimage, 0);
            return Backimage;
        }

        public int GetNextCardValue(int currentCard) //method to get the card value ~ cards are in order in folder
        {
            int cardValue = 0; //variable for the card value
            if (currentCard < 32) //uses the index to set the cards value first 31 card files
            {
                cardValue = (currentCard / 4) + 2;
            }
            else if (currentCard > 31 && currentCard < 36) // specifies aces have a value of 11
            {
                cardValue = 11;
            }
            else
            {
                cardValue = 10; //specifies the cards after index 35 have a value of 10 i.e. face cards
            }

            return cardValue; // returns card value to caller

        }
        public Card GetNextCard() //gets the next card from the deck
        {

            if (currentCard >= 52) //keeps the array from going out of bounds
                currentCard = 0;

            return allCards[currentCard++]; //returns next card in array
        }
        public int resetCurrentCard()
        {
            currentCard = 0;
            return currentCard;
        }
        public int GetCurrentCardNumber() //getter for card number
        {
            return currentCard;
        }
        public void ShuffleCards<T>(T[] allCards) //shuffle method
        {
            int x = allCards.Length; //sets the variable for the iterations in the for loop
            
            for (int i = 0; i < (x - 1); i++) //increments i until loop iterates through half of array
            {
                int shuf = i + r.Next(x - i);
                T t = allCards[shuf];
                allCards[shuf] = allCards[i];
                allCards[i] = t;
            }
        }
        public void PutAcesFirst()
        {
            int aceindex = 0;
            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = allCards[index];

                if (TempCard1.isCardAce())
                {
                    Card OriginalCard = allCards[aceindex];
                    TempCard1.resetCardValue();
                    allCards[aceindex] = TempCard1;
                    allCards[index] = OriginalCard;
                    aceindex++;
                }

            }

            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = allCards[index];
                if (TempCard1.GetCardValue() == 10)
                {
                    Card TempCardJack = allCards[index];
                    Card TempCard4 = allCards[5];
                    allCards[index] = TempCard4;
                    allCards[5] = TempCardJack;

                }
            }
        }
        public void putAceThird()
        {

            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = allCards[index];

                if (TempCard1.GetCardValue() == 11)
                {
                    Card TempCardJack = allCards[index];
                    Card TempCard4 = allCards[5];
                    allCards[index] = TempCard4;
                    allCards[5] = TempCardJack;

                }
            }
        }
    }
}

