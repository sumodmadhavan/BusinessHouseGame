using BusinessHouseGame.Interfaces;

namespace BusinessHouseGame {
    public interface IBank : ITransactions
    {
        /// <summary>
        /// Gets or sets the Bank Balance
        /// </summary>
        int Balance { get; }
    }
}

