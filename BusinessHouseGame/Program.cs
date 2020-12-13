using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace BusinessHouseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the test case.
            string boardString = "E,E,J,H,E,T,J,T,E,E,H,J,T,H,E,E,J,H,E,T,J,T,E,E,H,J,T,H,J,E,E,J,H,E,T,J,T,E,E,H,J,T,E,H,E";
            string diceOutput = "4,4,4,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12";
            var windsorContainer = new WindsorContainer();
            windsorContainer.Register(Component.For<IBank>().ImplementedBy<Bank>().LifestyleSingleton());
            windsorContainer.Register(Component.For<IPlayer>().ImplementedBy<Player>().LifestyleTransient());
            windsorContainer.Register(Component.For<EmptyCell>().ImplementedBy<EmptyCell>().LifestyleTransient());
            windsorContainer.Register(Component.For<JailCell>().ImplementedBy<JailCell>().LifestyleTransient());
            windsorContainer.Register(Component.For<TreasuryCell>().ImplementedBy<TreasuryCell>().LifestyleTransient());
            windsorContainer.Register(Component.For<HotelCell>().ImplementedBy<HotelCell>().LifestyleTransient());
            windsorContainer.Register(Component.For<BusinessHouseGame>().ImplementedBy<BusinessHouseGame>().LifestyleSingleton());
            var bank = windsorContainer.Resolve<IBank>(new Castle.MicroKernel.Arguments() { { "initialBalance", 5000 } });

            var businessHouseGame =
                windsorContainer.Resolve<BusinessHouseGame>(
                    new Castle.MicroKernel.Arguments() { { "container", windsorContainer }, { "bank", bank }, { "count", 3 } }
                );

            var player1 = windsorContainer.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player2 = windsorContainer.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player3 = windsorContainer.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            Console.WriteLine(businessHouseGame.IsValidPlayers);
            businessHouseGame.BuildBoard(boardString);

            string[] diceOutputArray = diceOutput.Split(',');
            for (int index = 0; index < diceOutputArray.Length; index += 3)
            {
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index]), player1);
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index + 1]), player2);
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index + 2]), player3);
            }

            Console.WriteLine("Player-1 has total money {0} and asset of amount : {1} = {2}", player1.Balance, player1.AssetBalance, player1.Balance + player1.AssetBalance);
            Console.WriteLine("Player-2 has total money {0} and asset of amount : {1} = {2}", player2.Balance, player2.AssetBalance, player2.Balance + player2.AssetBalance);
            Console.WriteLine("Player-3 has total money {0} and asset of amount : {1} = {2}", player3.Balance, player3.AssetBalance, player3.Balance + player3.AssetBalance);
            Console.WriteLine("Balance at Bank: {0}", bank.Balance);

            Console.ReadLine();
            windsorContainer.Dispose();
        }
    }
}

