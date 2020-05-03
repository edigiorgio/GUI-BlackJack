using System.Drawing;

namespace EricsBlackJack
{
    class Card
    {
        private Image image;     //creates image object
        private int value;       //variable for card value
        private bool isAce;
        private bool isCard;
        public Card(Image myImage, int myValue)
        {
            image = myImage; //setter for image
            value = myValue; //setter for card value
        }
        public Image getCardImage() //getter for card image
        {
            return image;
        }
        public Image backImage()
        {
            return image;
        }
        public int GetCardValue() //getter for card value
        {
            return value;
        }
        public void SetCardToAce()
        {
            isAce = true;
        }
        public bool isCardAce()
        {
            return isAce;
        }
        public int resetCardValue()
        {
            value = 11;
            return value;
        }
        public int SetCardValue()
        {
            value = 1;
            return value;
        }
        public bool doesCardExist()
        {
            return isCard;
        }
        public void setCard()
        {
            isCard = true;
        }
        public bool ResetAce()
        {
            isAce = false;
            return isAce;
        }
    }
}
