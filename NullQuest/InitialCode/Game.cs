//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NullQuest.Screens;
//using NullQuest.Screens.Animations;
//using NullQuest.ViewModel;

//namespace NullQuest
//{
//    class Game
//    {
//        private readonly IViewModelFactory viewModelFactory;
//        private readonly IScreenManager screens;


//        public Game(IViewModelFactory viewModelFactory, IScreenManager screens)
//        {
//            this.viewModelFactory = viewModelFactory;
//            this.screens = screens;
//        }

//        public void Start()
//        {
//            // Load the splash screen and play the cool sound effect
//            screens.Play(new SplashAnimation());
//            screens.Push(new Screen(viewModelFactory.Get<CharacterManager>()));


//            // Display opening menu (choose a character, and character management)

//        }
//    }
//}
