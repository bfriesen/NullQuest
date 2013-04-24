using System;
using System.Linq;
using System.Text;
using NullQuest.Game;
using NullQuest.Game.Effects;
using NullQuest.Game.Factories;
using NullQuest.MainMenu;
using BadSnowstorm;
using NullQuest.Data;
using NullQuest.Game.Combat;
using NullQuest.DependencyInjection;

namespace NullQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureConsole();

            //var myjectContainer = new MyjectContainer();
            //var controllerFactory = new MyjectControllerFactory(myjectContainer);

            if (args.Any(arg => arg.ToLower() == "/nosplash"))
            {
                Application.Run(
                    new ApplicationContext<GameLaunchedController>
                    {
                        //ControllerFactory = controllerFactory
                    });
            }
            else
            {
                Application.Run(
                    new ApplicationContext<SplashScreen, GameLaunchedController>
                    {
                        //ControllerFactory = controllerFactory
                    });
            }
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
