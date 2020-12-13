namespace BusinessHouseGame {
    /// <summary>
    /// Represents Treasury Cell
    /// </summary>
    public class TreasuryCell : BaseCell {
        /// <summary>
        /// Process the player entry
        /// </summary>
        public override void ProcessPlayerEntry() {
            Bank.Debit(Helper.TreasuryMoney);
            Player.Credit(Helper.TreasuryMoney);
        }
    }
}

