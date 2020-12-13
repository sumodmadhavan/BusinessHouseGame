using System.Collections.Generic;
using Castle.Windsor;

namespace BusinessHouseGame {
    /// <summary>
    /// Represent business house game
    /// </summary>
    public class BusinessHouseGame {
        /// <summary>
        /// Gets or sets Bank
        /// </summary>
        private IBank Bank { get; set; }

        /// <summary>
        /// Gets or sets container
        /// </summary>
        private IWindsorContainer Container { get; set; }

        public List<IPlayer> PlayerList { get; set; }
        /// <summary>
        /// Gets or sets cells
        /// </summary>
        private List<BaseCell> Cells { get; set; }

        private int PlayerCount { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">IoC Container</param>
        /// <param name="bank">Bank instance</param>
        public BusinessHouseGame(IWindsorContainer container, IBank bank,int count) {
            Cells = new List<BaseCell>();
            Container = container;
            Bank = bank;
            PlayerCount = count;
            if (PlayerCount>2)
            {
                IsValidPlayers = CreatePlayer(PlayerCount); ;
            }
            else
            {
                IsValidPlayers = false;
            }
        }

        private bool CreatePlayer(int playerCount)
        {
            PlayerList = new List<IPlayer>();
            for (int i = 0; i < playerCount; i++)
            {
                IPlayer player = Container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
                PlayerList.Add(player);
            }
            return PlayerList.Count > 2;
        }

        public bool IsValidPlayers { get; set; }

        /// <summary>
        /// Builds the Business House Board Game cells
        /// </summary>
        /// <param name="boardString">Board game intput string</param>
        public void BuildBoard(string boardString) {
            //base case
            if (!IsValidPlayers)
            {
                throw new System.Exception("Invalid Players");
            }
            string[] boardCellArray = boardString.Split(',');
            foreach (var boardCell in boardCellArray) {
                BaseCell baseCell = null;
                switch (boardCell)
                {
                    case "J":
                        baseCell = Container.Resolve<JailCell>();
                        break;
                    case "H":
                        baseCell = Container.Resolve<HotelCell>();
                        break;
                    case "T":
                        baseCell = Container.Resolve<TreasuryCell>();
                        break;
                    case "E":
                        baseCell = Container.Resolve<EmptyCell>();
                        break;
                }
                Cells.Add(baseCell);
            }
        }

        /// <summary>
        /// Moves the player across the cells
        /// </summary>
        public void Move(int cellToMove, IPlayer player) {
            player.CurrentCellPosition += cellToMove;

            if (player.CurrentCellPosition >= Cells.Count) {
                player.CurrentCellPosition -= Cells.Count;
            }
            BaseCell cell = Cells[player.CurrentCellPosition];
            cell.Bank = Bank;
            cell.Player = player;
            cell.ProcessPlayerEntry();
        }
    }
}

