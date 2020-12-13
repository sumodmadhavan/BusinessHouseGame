using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using BusinessHouseGame;
using System;

namespace BusinessHouseGame.Test
{
    [TestClass]
    public class BhouseTest
    {
        private WindsorContainer container = null;
        [TestMethod]
        public void Setup_Containers()
        {
            container = new WindsorContainer();
            container.Register(Component.For<IBank>().ImplementedBy<Bank>().LifestyleSingleton());
            container.Register(Component.For<IPlayer>().ImplementedBy<Player>().LifestyleTransient());
            container.Register(Component.For<EmptyCell>().ImplementedBy<EmptyCell>().LifestyleTransient());
            container.Register(Component.For<JailCell>().ImplementedBy<JailCell>().LifestyleTransient());
            container.Register(Component.For<TreasuryCell>().ImplementedBy<TreasuryCell>().LifestyleTransient());
            container.Register(Component.For<HotelCell>().ImplementedBy<HotelCell>().LifestyleTransient());
            container.Register(Component.For<BusinessHouseGame>().ImplementedBy<BusinessHouseGame>().LifestyleSingleton());
            var bank = container.Resolve<IBank>(new Castle.MicroKernel.Arguments() { { "initialBalance", 5000 } });
            Assert.IsInstanceOfType(bank, typeof(Bank));
            Assert.AreEqual(5000, bank.Balance);

            var player1 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            Assert.IsInstanceOfType(player1, typeof(IPlayer));
            Assert.AreEqual(1000, player1.Balance);

            var emptyCell = container.Resolve<EmptyCell>();
            Assert.IsInstanceOfType(emptyCell, typeof(EmptyCell));

            var jailCell = container.Resolve<JailCell>();
            Assert.IsInstanceOfType(jailCell, typeof(JailCell));

            var treasuryCell = container.Resolve<TreasuryCell>();
            Assert.IsInstanceOfType(treasuryCell, typeof(TreasuryCell));

            var hotelCell = container.Resolve<HotelCell>();
            Assert.IsInstanceOfType(hotelCell, typeof(HotelCell));

            var businessHouseGame =
                container.Resolve<BusinessHouseGame>(
                    new Castle.MicroKernel.Arguments() { { "container", container }, { "bank", bank }, { "count", 3 } }
                );
            Assert.IsInstanceOfType(businessHouseGame, typeof(BusinessHouseGame));
        }

        [TestMethod]
        [DataRow
            ("E,E,J,H,E,T,J,T,E,E,H,J,T,H,E,E,J,H,E,T,J,T,E,E,H,J,T,H,J,E,E,J,H,E,T,J,T,E,E,H,J,T,E,H,E",
            "4,4,4,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12",
            DisplayName =" Input from File - Should Pass")]
        public void Player_Move(string boardString,string diceOutput)
        {
            container = new WindsorContainer();
            container.Register(Component.For<IBank>().ImplementedBy<Bank>().LifestyleSingleton());
            container.Register(Component.For<IPlayer>().ImplementedBy<Player>().LifestyleTransient());
            container.Register(Component.For<EmptyCell>().ImplementedBy<EmptyCell>().LifestyleTransient());
            container.Register(Component.For<JailCell>().ImplementedBy<JailCell>().LifestyleTransient());
            container.Register(Component.For<TreasuryCell>().ImplementedBy<TreasuryCell>().LifestyleTransient());
            container.Register(Component.For<HotelCell>().ImplementedBy<HotelCell>().LifestyleTransient());
            container.Register(Component.For<BusinessHouseGame>().ImplementedBy<BusinessHouseGame>().LifestyleSingleton());
            var bank = container.Resolve<IBank>(new Castle.MicroKernel.Arguments() { { "initialBalance", 5000 } });
            var businessHouseGame =
                container.Resolve<BusinessHouseGame>(
                    new Castle.MicroKernel.Arguments() { { "container", container }, { "bank", bank }, { "count", 3 } }
                );

            var player1 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player2 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player3 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });

            businessHouseGame.BuildBoard(boardString);

            string[] diceOutputArray = diceOutput.Split(',');

            for (int index = 0; index < diceOutputArray.Length; index += 3)
            {
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index]), player1);
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index + 1]), player2);
                businessHouseGame.Move(Convert.ToInt32(diceOutputArray[index + 2]), player3);
            }
            Assert.AreEqual(1200,player1.Balance + player1.AssetBalance);
            Assert.AreEqual(1200, player2.Balance + player2.AssetBalance);
            Assert.AreEqual(1050, player3.Balance + player3.AssetBalance);
        }
        [TestMethod]
        public void Player_Move_Asset_InitialBalance()
        {
            container = new WindsorContainer();
            container.Register(Component.For<IBank>().ImplementedBy<Bank>().LifestyleSingleton());
            container.Register(Component.For<IPlayer>().ImplementedBy<Player>().LifestyleTransient());
            container.Register(Component.For<EmptyCell>().ImplementedBy<EmptyCell>().LifestyleTransient());
            container.Register(Component.For<JailCell>().ImplementedBy<JailCell>().LifestyleTransient());
            container.Register(Component.For<TreasuryCell>().ImplementedBy<TreasuryCell>().LifestyleTransient());
            container.Register(Component.For<HotelCell>().ImplementedBy<HotelCell>().LifestyleTransient());
            container.Register(Component.For<BusinessHouseGame>().ImplementedBy<BusinessHouseGame>().LifestyleSingleton());
            var bank = container.Resolve<IBank>(new Castle.MicroKernel.Arguments() { { "initialBalance", 5000 } });
            var businessHouseGame =
                container.Resolve<BusinessHouseGame>(
                    new Castle.MicroKernel.Arguments() { { "container", container }, { "bank", bank }, { "count", 3 } }
                );

            var player1 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player2 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            var player3 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            Assert.AreEqual(1000, player1.Balance);
            Assert.AreEqual(1000, player2.Balance);
            Assert.AreEqual(1000, player3.Balance);
        }
        [TestMethod]
        [DataRow
            ("E,E,J,H,E,T,J,T,E,E,H,J,T,H,E,E,J,H,E,T,J,T,E,E,H,J,T,H,J,E,E,J,H,E,T,J,T,E,E,H,J,T,E,H,E",
            "4,4,4,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12,2,3,5,6,7,8,5,11,10,12",
            DisplayName = " Input from File - Should Pass")]
        public void Player_Move_Single_Player_Fail(string boardString, string diceOutput)
        {
            container = new WindsorContainer();
            container.Register(Component.For<IBank>().ImplementedBy<Bank>().LifestyleSingleton());
            container.Register(Component.For<IPlayer>().ImplementedBy<Player>().LifestyleTransient());
            container.Register(Component.For<EmptyCell>().ImplementedBy<EmptyCell>().LifestyleTransient());
            container.Register(Component.For<JailCell>().ImplementedBy<JailCell>().LifestyleTransient());
            container.Register(Component.For<TreasuryCell>().ImplementedBy<TreasuryCell>().LifestyleTransient());
            container.Register(Component.For<HotelCell>().ImplementedBy<HotelCell>().LifestyleTransient());
            container.Register(Component.For<BusinessHouseGame>().ImplementedBy<BusinessHouseGame>().LifestyleSingleton());
            var bank = container.Resolve<IBank>(new Castle.MicroKernel.Arguments() { { "initialBalance", 5000 } });
            var businessHouseGame =
                 container.Resolve<BusinessHouseGame>(
                     new Castle.MicroKernel.Arguments() { { "container", container }, { "bank", bank }, { "count", 3 } }
                 );

            var player1 = container.Resolve<IPlayer>(new Castle.MicroKernel.Arguments() { { "initalBalance", 1000 } });
            Assert.IsTrue(businessHouseGame.IsValidPlayers, "Invalid Player");
        }
    }
}
