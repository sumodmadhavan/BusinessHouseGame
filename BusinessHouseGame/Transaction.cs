using System;
using BusinessHouseGame.Interfaces;

namespace BusinessHouseGame
{
    public class Transaction : ITransactions
    {
        public Transaction()
        {

        }

        public void Credit(int amount)
        {
            throw new NotImplementedException();
        }

        public void Debit(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
