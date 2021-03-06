﻿using NullQuest.Game;
using NullQuest.Game.Dungeon;
using BadSnowstorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Data;
using NullQuest.Game.Inventory;
using NullQuest.SpellBook;

namespace NullQuest.Store
{
    public class StoreController : Controller
    {
        private readonly GameWorld _gameWorld;
        private readonly ISaveGameRepository _saveGameRepository;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IDice _dice;

        public StoreController(GameWorld gameWorld, ISaveGameRepository saveGameRepository, IAsciiArtRepository asciiArtRepository, IDice dice)
        {
            _gameWorld = gameWorld;
            _saveGameRepository = saveGameRepository;
            _asciiArtRepository = asciiArtRepository;
            _dice = dice;
        }

        public override ViewModel Index()
        {
            var menu = new Menu();
            
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Exit",
                    Id = '1',
                    ActionResult = Actions.GoBack
                });

            var viewModel = ViewModel.CreateWithMenu<StoreViewModel>(menu);

            viewModel.Title = "We're closed. Go away.";
            viewModel.Stats = StatsViewModel.FromPlayer(_gameWorld.Player);
            viewModel.AsciiArt = _asciiArtRepository.GetStoreArt();

            return viewModel;
        }
    }
}
