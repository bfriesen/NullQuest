using System;
using System.Linq;
using System.Text;
using NullQuest.Game;
using NullQuest.Game.Effects;
using NullQuest.Game.Factories;
using NullQuest.MainMenu;
using BadSnowstorm;
using Ninject;
using NullQuest.Data;
using NullQuest.Game.Combat;

namespace NullQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureConsole();

            var kernel = GetNinjectKernel();
            var controllerFactory = new NinjectControllerFactory(kernel);

            if (args.Any(arg => arg.ToLower() == "/nosplash"))
            {
                Application.Run(
                    new ApplicationContext<GameLaunchedController>
                    {
                        ControllerFactory = controllerFactory
                    });
            }
            else
            {
                Application.Run(
                    new ApplicationContext<SplashScreen, GameLaunchedController>
                    {
                        ControllerFactory = controllerFactory
                    });
            }
        }

        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<GameWorld>().ToSelf().InSingletonScope();
            kernel.Bind<ICombatEngine>().To<CombatEngine>().InTransientScope();
            
            kernel.Bind<IDice>().To<Dice>();
            kernel.Bind<IStatsGenerator>().To<StatsGenerator>();
            kernel.Bind<ISaveGameRepository>().To<FileSystemSaveGameRepository>();
            kernel.Bind<IFileAccess>().To<FileAccess>();
            kernel.Bind<IActionSelector>().To<MonsterActionSelector>();
            kernel.Bind<ICombatantSelector>().To<CombatantSelector>();
            kernel.Bind<IDungeonNameGenerator>().To<DeterministicDungeonNameGenerator>();

            kernel.Bind<IMonsterDataRepository>().To<HardCodedMonsterDataRepository>();
            kernel.Bind<IWeaponDataRepository>().To<HardCodedWeaponDataRepository>();
            kernel.Bind<ISpellDataRepository>().To<HardCodedSpellDataRepository>();
            kernel.Bind<IItemDataRepository>().To<HardCodedItemDataRepository>();
            
            kernel.Bind<IMonsterFactory>().To<MonsterFactory>();
            kernel.Bind<IWeaponFactory>().To<WeaponFactory>();
            kernel.Bind<ISpellFactory>().To<SpellFactory>();
            kernel.Bind<IEffectFactory>().To<EffectFactory>();
            kernel.Bind<IItemFactory>().To<ItemFactory>();

            kernel.Bind<IAsciiArtRepository>().To<HardCodedAsciiArtRepository>();
            
            return kernel;
        }

        private static void ConfigureConsole()
        {
            Console.Title = "Null Quest: The Legend of the Eternal Grind";

            Console.CursorVisible = false;

            Console.OutputEncoding = Encoding.GetEncoding(1252);
            Console.SetWindowSize(99, 44);
            Console.BufferWidth = 99;
            Console.BufferHeight = 44;
        }
    }
}
