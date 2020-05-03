using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    //MONEY CLASS
    class Funds
    {
        private int totalmoney = 0;
        private int betamount = 0;
        private int potAmount = 0;

        public Funds(int initialfunds)
        {
            totalmoney = initialfunds;
        }

        public int GetBetAmount()
        {
            return betamount;
        }
        public void SetBetAmount(int betamt)
        {
            betamount = betamt;
        }
        public void WonBet()
        {
            totalmoney = totalmoney + betamount + potAmount;
            
        }

        public int GetTotalMoney()
        {
            return totalmoney;
        }
        public void currentPot()
        {
            potAmount = potAmount + betamount;
            totalmoney = totalmoney - betamount;
        }
        public int setPot()
        {
            return potAmount;
        }
        public void resetallBets()
        {
            potAmount = 0;
            betamount = 0;
        }
        public void setdraw()
        {
            totalmoney = totalmoney + betamount;
        }
        public void resetFunds()
        {
            totalmoney = 1000;
        }
    }

}