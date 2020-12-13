namespace BusinessHouseGame {
    public interface IPlayer : IBank
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Gets or sets the player asset balance
        /// </summary>
        int AssetBalance { get; set; }

        /// <summary>
        /// Gets or sets the current player cell position
        /// </summary>
        int CurrentCellPosition { get; set; }
    }
}

