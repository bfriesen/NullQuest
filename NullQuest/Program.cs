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

            var kernel = GetNinjectKernel(DisableEncryption(args));
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

        private static bool DisableEncryption(string[] args)
        {
#if DEBUG
            return args.Any(arg => arg.ToLower() == "/disableencryption");
#else
            return false;
#endif
        }

        private static IKernel GetNinjectKernel(bool disableEncryption)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDice>().To<Dice>().InSingletonScope();
            kernel.Bind<IStatsGenerator>().To<StatsGenerator>().InSingletonScope();
            kernel.Bind<ISaveGameRepository>().To<FileSystemSaveGameRepository>().InSingletonScope();
            kernel.Bind<IFileAccess>().ToConstant<AESFileAccess>(new AESFileAccess(new FileAccess(), disableEncryption));
            kernel.Bind<GameWorld>().ToSelf().InSingletonScope();
            kernel.Bind<IActionSelector>().To<MonsterActionSelector>().InSingletonScope();
            kernel.Bind<ICombatantSelector>().To<CombatantSelector>().InSingletonScope();
            kernel.Bind<ICombatEngine>().To<CombatEngine>().InTransientScope();
            kernel.Bind<IDungeonNameGenerator>().To<DeterministicDungeonNameGenerator>().InSingletonScope();

            kernel.Bind<IMonsterDataRepository>().To<HardCodedMonsterDataRepository>().InSingletonScope();
            kernel.Bind<IWeaponDataRepository>().To<HardCodedWeaponDataRepository>().InSingletonScope();
            kernel.Bind<ISpellDataRepository>().To<HardCodedSpellDataRepository>().InSingletonScope();
            kernel.Bind<IItemDataRepository>().To<HardCodedItemDataRepository>().InSingletonScope();
            
            kernel.Bind<IMonsterFactory>().To<MonsterFactory>().InSingletonScope();
            kernel.Bind<IWeaponFactory>().To<WeaponFactory>().InSingletonScope();
            kernel.Bind<ISpellFactory>().To<SpellFactory>().InSingletonScope();
            kernel.Bind<IEffectFactory>().To<EffectFactory>().InSingletonScope();
            kernel.Bind<IItemFactory>().To<ItemFactory>().InSingletonScope();

            kernel.Bind<IAsciiArtRepository>().To<HardCodedAsciiArtRepository>().InSingletonScope();
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
