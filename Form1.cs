using GreenvilleRevenueGUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EricsBlackJack
{
    public partial class Form1 : Form
    {

        Cards deckOfCards = new Cards(); //creates deck object from cards.cs
        Hand dealer; //creates dealer object from hand.cs
        Hand player; //creates player object from hand.cs
        int click = 0; //click counter for hitme button
        int button1click = 0; //click counter for game start button
        Image aCardBack; //creates card back object from cards.cs *not working*
        Card aCard;
        private int hitMeClick = 0;
        private int funds = 1000;
        Funds money;
        int betamt = 0;
        public Form1()
        {
            InitializeComponent();
            button14.Visible = false;
            dealer = new Hand("Dealer"); //initializes dealer hand
            player = new Hand("Player"); //initializes player hand
            this.Size = new Size(1100, 700); //sets the size of the playing space
            Button1.AutoSize = true;
            money = new Funds(funds);
            label19.Text = funds.ToString();
        }

        private void Button1_Click(object sender, EventArgs e) //button click of fsw picture "deal button"
        {

            aCard = deckOfCards.GetNextCard(); //gets the next card from the deck with getter method
            dealer.DealACardtoMe(aCard); //puts the card into the dealers hand with the deal a card method
            button2.Image = aCard.getCardImage(); //gets the image for the card drawn from the deck and places it on button 2
            label2.Text = "" + aCard.GetCardValue(); //displays the value of the card drawn into the label 2
            label13.Text = "Dealers total showing = " + dealer.GetTotalValue(); // shows the total of all cards, in this case just the first card

            aCard = deckOfCards.GetNextCard(); // this block repeats the same as the first just leaving out the total value to avoid
            dealer.DealACardtoMe(aCard);        //showing the player the dealers hand
            aCardBack = aCard.getCardImage(); //this stores the card for button 5 till the end of the game
            label1.Text = "" + aCard.GetCardValue();
            button5.Visible = true;
            //updateDealerGraphics(dealer.GetNumberofCards());

            aCard = deckOfCards.GetNextCard(); // repeats process of dealer cards
            player.DealACardtoMe(aCard);
            button3.Image = aCard.getCardImage();
            label8.Text = "" + aCard.GetCardValue();

            aCard = deckOfCards.GetNextCard();
            player.DealACardtoMe(aCard);
            button6.Image = aCard.getCardImage();
            label9.Text = "" + aCard.GetCardValue();
            label5.Text = "Your total = " + player.GetTotalValue(); //shows players total
            gameLoaded();
            button1click++; //counter for the button to reset game if purple "deal" button is hit again
            checkBlackJack();
            if(player.checkBlackJack() || dealer.checkBlackJack())
            {
                button5.Image = aCardBack;
                label1.Visible = true;
                label13.Text = "Dealers total = " + dealer.GetTotalValue();
                if (player.checkBlackJack())
                {
                    if(betamt > 0)
                    {
                        money.WonBet();
                        money.GetTotalMoney();
                        label19.Text = money.GetTotalMoney().ToString();
                    }                  
                    playerWinMessage();
                }
                else if (dealer.checkBlackJack())
                {
                    if(betamt > 0)
                    {
                        money.GetTotalMoney();
                        label19.Text = money.GetTotalMoney().ToString();
                    }
                    dealerWinMessage();
                }
                else
                {
                    if (betamt > 0)
                    {
                        money.setdraw();
                        money.GetTotalMoney();
                        label19.Text = money.GetTotalMoney().ToString();
                    }
                    drawMessage();
                }
                
            }


            switch (button1click) //switch statement to reset the game 
            {
                case 1:
                    break; //ignores first click
                case 2:
                    reset();
                    Button1_Click(null, null);
                    break;
            }
        }

        private void dealDealer()
        {
            setDealerTotal();
            while (dealer.GetTotalValue() < 17)
            {
                if (dealer.GetTotalValue() < 17) //if dealer is under 16 draw a card
                {
                    dealer.setCard(2);
                    aCard = deckOfCards.GetNextCard();
                    dealer.DealACardtoMe(aCard);
                    button11.Image = aCard.getCardImage();
                    label3.Text = "" + aCard.GetCardValue();
                    label13.Text = "Dealers total showing = " + dealer.GetTotalValue();
                    button11.Visible = true;
                    setDealerTotal();

                    if (dealer.GetTotalValue() < 17) //nested if to test second round and draw another card if needed
                    {
                        dealer.setCard(3);
                        aCard = deckOfCards.GetNextCard();
                        dealer.DealACardtoMe(aCard);
                        button10.Image = aCard.getCardImage();
                        label4.Text = "" + aCard.GetCardValue();
                        label13.Text = "Dealers total showing = " + dealer.GetTotalValue();
                        button10.Visible = true;
                        setDealerTotal();

                        if (dealer.GetTotalValue() < 17)
                        {
                            dealer.setCard(4);
                            aCard = deckOfCards.GetNextCard();
                            dealer.DealACardtoMe(aCard);
                            button12.Image = aCard.getCardImage();
                            label7.Text = "" + aCard.GetCardValue();
                            label13.Text = "Dealers total showing = " + dealer.GetTotalValue();
                            button12.Visible = true;
                            setDealerTotal();
                        }
                    }
                }
                break;
            }
        }
        private void Hitme_Click(object sender, EventArgs e) //"hit me" button
        {
            hitMeClick++;
            dealPlayerCards();
            button13.Enabled = false;
        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            button5.Image = aCardBack;
            label1.Visible = true;
            label13.Text = "Dealers total = " + dealer.GetTotalValue(); //updates dealers total

            
            checkBlackJack();
            if(player.checkBlackJack() == true & dealer.checkBlackJack() == false)
            {
                if (betamt > 0)
                {
                    money.WonBet();
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                playerWinMessage();
            }
            if(dealer.checkBlackJack() == true & dealer.checkBlackJack() == false)
            {
                if(betamt > 0)
                {
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                dealerWinMessage();
            }
            else
            {
                dealDealer();
                checkWinner();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (player.isCardAce(0) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to change the Ace?", "Change the Ace", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int aceClick = 0;
                    if (aceClick < 1)
                    {
                        player.setAce(0);
                        label8.Text = "" + player.getCardValue(0);
                        label5.Text = "Your total = " + player.GetTotalValue();
                    }
                    aceClick++;
                }
                else if (dialogResult == DialogResult.No)
                {
                    player.resetAce(0);
                    label8.Text = "" + player.getCardValue(0);
                    label5.Text = "Your total = " + player.GetTotalValue();

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (player.isCardAce(1) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to change the Ace?", "Change the Ace", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    player.setAce(1);
                    label9.Text = "" + player.getCardValue(1);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
                else if (dialogResult == DialogResult.No)
                {
                    player.resetAce(1);
                    label9.Text = "" + player.getCardValue(1);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (player.isCardAce(2) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to change the Ace?", "Change the Ace", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    player.setAce(2);
                    label10.Text = "" + player.getCardValue(2);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
                else if (dialogResult == DialogResult.No)
                {
                    player.resetAce(2);
                    label10.Text = "" + player.getCardValue(2);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (player.isCardAce(3) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to change the Ace?", "Change the Ace", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    player.setAce(3);
                    label11.Text = "" + player.getCardValue(3);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
                else if (dialogResult == DialogResult.No)
                {
                    player.resetAce(3);
                    label11.Text = "" + player.getCardValue(3);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (player.isCardAce(4) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to change the Ace?", "Change the Ace", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    player.setAce(4);
                    label12.Text = "" + player.getCardValue(4);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
                else if (dialogResult == DialogResult.No)
                {
                    player.resetAce(4);
                    label12.Text = "" + player.getCardValue(4);
                    label5.Text = "Your total = " + player.GetTotalValue();
                }
            }
        }


        private void reset()
        {
            button1click = 0;
            hitMeClick = 0;
            dealer.ResetHand(); //resets all variables for dealer
            player.ResetHand(); //resets all variables for player
            deckOfCards.resetCurrentCard();
            deckOfCards.ShuffleCards(deckOfCards.allCards);
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            label1.Visible = false;
            button5.Image = deckOfCards.setBackImage();
            label3.Text = null;
            label4.Text = null;
            label7.Text = null;
            label1.Text = null;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            label11.Text = null;
            label12.Text = null;
            label10.Text = null;
            label22.Text = null;
            textBox1.Text = "100";
            button13.Enabled = true;
            betamt = 0;
            click = 0;
            label5.Text = "Your total = ";
            label13.Text = "Dealers total showing = " + dealer.GetTotalValue();
            money.SetBetAmount(betamt);
            money.resetallBets();
            
        }


        private void gameLoaded()
        {
            hitme.Visible = true; //visibility settings for the buttons that were not available before
            button4.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button6.Visible = true;
            button13.Visible = true;
            button14.Visible = false;
            button13.Enabled = true;
            textBox1.Visible = true;
            label5.Visible = true;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            label21.Visible = true;
            label22.Visible = true;
        }



        


        private void dealPlayerCards()
        {
            click++; //click counter to increment cards into play
            aCard = deckOfCards.GetNextCard(); //initializes deck object within this method gets the next card from the deck of cards
            switch (click) //switch statement to increment through clicks
            {
                case 1: //places first card
                    aCard = deckOfCards.GetNextCard(); //gets next card
                    player.DealACardtoMe(aCard); //places card into player hand
                    button7.Image = aCard.getCardImage(); //places image onto the button
                    label10.Text = "" + aCard.GetCardValue(); //places the cards value
                    label5.Text = "Your total = " + player.GetTotalValue(); //shows the new total of the cards
                    button7.Visible = true; //makes the button visible

                    if (player.GetTotalValue() > 21) //if statement to see if player goes over 21
                    {
                        if (player.isCardAce(2) != true)
                        {
                            button4_MouseClick(null, null); // if player goes over 21 initializes "stay button" to end game
                        }
                        else
                        {
                            if (player.isCardAce(2) == true && hitMeClick == 2)
                            {
                                button4_MouseClick(null, null);
                            }
                        }
                    }
                    break;
                case 2:
                    aCard = deckOfCards.GetNextCard();
                    player.DealACardtoMe(aCard);
                    button8.Image = aCard.getCardImage();
                    label11.Text = "" + aCard.GetCardValue();
                    label5.Text = "Your total = " + player.GetTotalValue();
                    button8.Visible = true;

                    if (player.GetTotalValue() > 21)
                    {
                        if (player.isCardAce(3) != true)
                        {
                            button4_MouseClick(null, null); // if player goes over 21 initializes "stay button" to end game
                        }
                        else
                        {
                            if (player.isCardAce(3) == true && hitMeClick == 3)
                            {
                                button4_MouseClick(null, null);
                            }
                        }
                    }
                    break;

                case 3:
                    aCard = deckOfCards.GetNextCard();
                    player.DealACardtoMe(aCard);
                    button9.Image = aCard.getCardImage();
                    label12.Text = "" + aCard.GetCardValue();
                    label5.Text = "Your total = " + player.GetTotalValue();
                    button9.Visible = true;

                    if (player.GetTotalValue() > 21)
                    {
                        if (player.isCardAce(4) != true)
                        {
                            button4_MouseClick(null, null); // if player goes over 21 initializes "stay button" to end game
                        }
                        else
                        {
                            if (player.isCardAce(4) == true && hitMeClick == 4)
                            {
                                button4_MouseClick(null, null);
                            }
                        }
                    }
                    break;
                case 4: //when player has max cards and attempts to draw another card this displays message to end game
                    DialogResult dialogResult = MessageBox.Show("You have reached your maximum number of Cards, Would you like to end the game?", "Oops thats not quite right!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        button4_MouseClick(null, null);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        break;
                    }
                    break;

            }
        }

        private void checkBlackJack()
        {
            int playertotal = player.GetTotalValue();
            int dealertotal = dealer.GetTotalValue();
            int playercards = player.GetNumberofCards();
            int dealercards = player.GetNumberofCards();
            if(playertotal == 21 & playercards == 2)
            {
                player.blackJack();

            }
            if(dealertotal == 21 & dealercards == 2)
            {
                dealer.blackJack();
            }
        }

        private void checkWinner()
        {
            //if statements to test for winner
            if (dealer.GetTotalValue() > player.GetTotalValue() & dealer.GetTotalValue() <= 21)
            {
                if (betamt > 0)
                {
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                dealerWinMessage();
            }
            else if (player.GetTotalValue() > 21)
            {
                if (betamt > 0)
                {
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                dealerWinMessage();

            }
            else if (player.GetTotalValue() > dealer.GetTotalValue() && player.GetTotalValue() < 22)
            {
                if (betamt > 0)
                {
                    money.WonBet();
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                playerWinMessage();
            }
            else if (dealer.GetTotalValue() > 21)
            {
                if (betamt > 0)
                {
                    money.WonBet();
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                playerWinMessage();
            }
            else if(player.GetTotalValue() == dealer.GetTotalValue() && player.GetTotalValue() < 22)
            {
                if(betamt > 0)
                {
                    money.setdraw();
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                drawMessage();
            }
            else if(player.GetTotalValue() > 21 & dealer.GetTotalValue() > 21)
            {
                if(betamt > 0)
                {
                    money.setdraw();
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
            }
            else
            {
                if (betamt > 0)
                {
                    money.GetTotalMoney();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                dealerWinMessage();
            }
        }


        private void playerWinMessage()
        {
            DialogResult dialogResult = MessageBox.Show(String.Format("You won!! do you want to play again?\nPlayer total {1}\nDealer Total{0}", dealer.GetTotalValue(), player.GetTotalValue()), "The Player wins!",  MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
                Button1_Click(null, null);
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void drawMessage()
        {
            DialogResult dialogResult = MessageBox.Show(String.Format("There was a draw between you and the dealer, do you want to play again?\nPlayer total {1}\nDealer Total{0}", dealer.GetTotalValue(), player.GetTotalValue()), "We Have a draw!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
                Button1_Click(null, null);
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void dealerWinMessage()
        {
            DialogResult dialogResult = MessageBox.Show(String.Format("The dealer wins do you want to play again?\nPlayer total {1}\nDealer Total{0}", dealer.GetTotalValue(), player.GetTotalValue()), "Dealer Wins!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
                Button1_Click(null, null);
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }
        private void setDealerTotal()
        {
            int iteration = 0;
            while (dealer.GetTotalValue() > 21 && iteration < 2)
            {
                iteration++;
                for (int i = 0; i < dealer.GetNumberofCards(); i++)
                {
                    if (dealer.isCardAce(i))
                    {
                        dealer.setAce(i);
                    }

                }
            }
            if (dealer.isCardAce(0))
            {
                label2.Text = dealer.getCardValue(0).ToString();
            }
            if (dealer.isCardAce(1))
            {
                label1.Text = dealer.getCardValue(1).ToString();
            }
            if (dealer.GetNumberofCards() > 2)
            {
                if (dealer.isCardAce(2))
                {
                    label3.Text = dealer.getCardValue(2).ToString();
                }
            }
            if (dealer.GetNumberofCards() > 3)
            {
                if (dealer.isCardAce(3))
                {
                    label4.Text = dealer.getCardValue(3).ToString();
                }
            }
            if (dealer.GetNumberofCards() > 4)
            {
                if (dealer.isCardAce(4))
                {
                    label7.Text = dealer.getCardValue(4).ToString();
                }
            }
        }


        private void button14_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want your aces first or third? Yes for first, No for Third", "How do you want your aces?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reset();
                deckOfCards.LoadCards();
                deckOfCards.ShuffleCards(deckOfCards.allCards);
                deckOfCards.PutAcesFirst();
            }
            else if (dialogResult == DialogResult.No)
            {
                reset();
                deckOfCards.LoadCards();
                deckOfCards.ShuffleCards(deckOfCards.allCards);
                deckOfCards.putAceThird();
            }
            gameLoaded();
            Button1_Click(null, null);
            button5.Image = aCardBack;

        }


        private void button13_Click(object sender, EventArgs e)
        {
            betamt = Convert.ToInt32(textBox1.Text);
            if (money.GetTotalMoney() > 0)
            {
                if (betamt > money.GetTotalMoney())
                {
                    DialogResult dialogResult = MessageBox.Show("You Have Insufficient funds, would you like to add funds?", "Houston we have a problem!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        money.resetFunds();
                        label19.Text = money.GetTotalMoney().ToString();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        DialogResult dialogResult2 = MessageBox.Show("Would you like to exit the game?", "What would you like to do?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Application.Exit();
                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                    }
                }
                else
                {
                    money.SetBetAmount(betamt);
                    money.GetBetAmount();
                    money.currentPot();
                    label19.Text = money.GetTotalMoney().ToString();
                    label22.Text = money.setPot().ToString();
                }
                
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("You Have Insufficient funds, would you like to add funds?", "Houston we have a problem!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    money.resetFunds();
                    label19.Text = money.GetTotalMoney().ToString();
                }
                else if (dialogResult == DialogResult.No)
                {
                    DialogResult dialogResult2 = MessageBox.Show("Would you like to exit the game?", "What would you like to do?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(hitMeClick < 1)
            {
                button13.Enabled = true;
            }
        }
    }
}

