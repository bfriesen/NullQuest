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
using NullQuest.Store;
using NullQuest.Inn;

namespace NullQuest.Town
{
    public class TownController : Controller
    {
        private readonly FileSystemSaveGameRepository _saveGameRepository;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IDice _dice;

        public TownController()
        {
            _saveGameRepository = new FileSystemSaveGameRepository();
            _asciiArtRepository = new HardCodedAsciiArtRepository();
            _dice = new Dice();
        }

        public override ViewModel Index()
        {
            var menu = new Menu();
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Visit the Tolbooth Tavern",
                    Id = '1',
                    ActionResult = Actions.GoTo<InnController>
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Browse Ye Olde Store",
                    Id = '2',
                    ActionResult = Actions.GoTo<StoreController>
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Enter the Dungeon",
                    Id = '3',
                    ActionResult = Actions.GoTo<DungeonController>,
                    OnInputAccepted = () => GameWorld.SetRequiredNumberOfMonstersInCurrentDungeonLevelBeforeBoss(_dice)
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Inventory",
                    Id = '4',
                    ActionResult = Actions.GoTo<InventoryController>
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Spell Book",
                    Id = '5',
                    ActionResult = Actions.GoTo<SpellBookController>
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Exit to main menu",
                    Id = '6',
                    ActionResult = Actions.GoBack
                });

            var viewModel = ViewModel.CreateWithMenu<TownViewModel>(menu);

            viewModel.Stats = StatsViewModel.FromPlayer(GameWorld.Player);
            viewModel.AsciiArt = _asciiArtRepository.GetTownArt();

            return viewModel;
        }
    }
}
