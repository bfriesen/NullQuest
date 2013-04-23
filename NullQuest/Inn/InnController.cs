using NullQuest.Game;
using NullQuest.Game.Dungeon;
using BadSnowstorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Data;
using NullQuest.Game.Inventory;
using NullQuest.SpellBook;

namespace NullQuest.Inn
{
    public class InnController : Controller
    {
        private readonly GameWorld _gameWorld;
        private readonly ISaveGameRepository _saveGameRepository;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IDice _dice;

        private string _title;

        public InnController(GameWorld gameWorld, ISaveGameRepository saveGameRepository, IAsciiArtRepository asciiArtRepository, IDice dice)
        {
            _gameWorld = gameWorld;
            _saveGameRepository = saveGameRepository;
            _asciiArtRepository = asciiArtRepository;
            _dice = dice;
            _title = "Welcome to the Tolbooth Tavern. The food ain't great and the beds aren't soft. But it's the only Inn in town.";
        }

        public override ViewModel Index()
        {
            var menu = new Menu();

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Rest",
                    Id = '1',
                    ActionResult = Actions.Reload,
                    OnInputAccepted = () => 
                    {
                        _gameWorld.Player.Rest();
                        _title = "Your health and energy has been restored.";
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Save Game",
                    Id = '2',
                    ActionResult = Actions.Reload,
                    OnInputAccepted = () =>
                    {
                        _saveGameRepository.SaveGame(SaveGameData.FromGameWorld(_gameWorld));
                        _title = "Your game has been saved.";
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Exit",
                    Id = '3',
                    ActionResult = Actions.GoBack
                });

            var viewModel = ViewModel.CreateWithMenu<InnViewModel>(menu);

            viewModel.Title = _title;
            viewModel.Stats = StatsViewModel.FromPlayer(_gameWorld.Player);
            viewModel.AsciiArt = _asciiArtRepository.GetInnArt();

            return viewModel;
        }
    }
}
