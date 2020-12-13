using System;
namespace BusinessHouseGame.Interfaces
{
    /// <summary>
    /// Isolate the transaction for debit or credit coming from Bank or Player. Transaction Locks
    /// </summary>
    public interface ITransactions
    {
        void Credit(int amount);
        /// <summary>
        /// Debit the amount from the bank
        /// </summary>
        void Debit(int amount);
    }
}
