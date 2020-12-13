using System;
namespace BusinessHouseGame.Interfaces
{
    public interface ITransactions
    {
        void Credit(int amount);
        /// <summary>
        /// Debit the amount from the bank
        /// </summary>
        void Debit(int amount);
    }
}
