﻿using BadSnowstorm;
using NullQuest.Game.Factories;
using NullQuest.Game.Inventory;
using NullQuest.SpellBook;
using NullQuest.Game.Extensions;
using System;
using NullQuest.Data;

namespace NullQuest.Game.Dungeon
{
    public class DungeonController : Controller
    {
        private readonly GameWorld _gameWorld;
        private readonly IDice _dice;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private string _encounterDetails;
        private LastAction _lastAction = LastAction.MenuNavigation;

        public DungeonController(GameWorld gameWorld, IDice dice, IWeaponFactory weaponFactory, IAsciiArtRepository asciiArtRepository)
        {
            _gameWorld = gameWorld;
            _dice = dice;
            _weaponFactory = weaponFactory;
            _asciiArtRepository = asciiArtRepository;
        }

        public override ViewModel Index()
        {
            var menu = new Menu();
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Keep moving forward",
                    Id = '1',
                    ActionResult = GetNextEncounter(),
                    OnInputAccepted = () => _lastAction = LastAction.MovedForward
                });
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Inventory",
                    Id = '2',
                    ActionResult = Actions.GoTo<InventoryController>,
                    OnInputAccepted = () => _lastAction = LastAction.MenuNavigation
                });
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Spell Book",
                    Id = '3',
                    ActionResult = Actions.GoTo<SpellBookController>,
                    OnInputAccepted = () => _lastAction = LastAction.MenuNavigation
                });
            menu.AddMenuItem(
                new MenuItem
                {
                    Text = "Run away back to town",
                    Id = '4',
                    ActionResult = Actions.GoBack
                });

            DungeonViewModel viewModel = ViewModel.CreateWithMenu<DungeonViewModel>(menu);

            viewModel.Stats = StatsViewModel.FromPlayer(_gameWorld.Player);
            viewModel.DungeonName = string.Format("{0} (Level {1})", _gameWorld.GetCurrentDungeonName(), _gameWorld.CurrentDungeonLevel);
            viewModel.Information = _encounterDetails ?? "This is the dungeon - it isn't a very friendly place. Hallways go off in seemingly every direction, with no end in sight. A skeleton sits upright against a wall in the corner. No doubt an adventurer that came before you...";
            viewModel.AsciiArt = _asciiArtRepository.GetDungeonArt(_gameWorld.CurrentDungeonLevel);

            _encounterDetails = null;

            return viewModel;
        }

        private Func<IActionResult> GetNextEncounter()
        {
            // 9 in 10 chance of encountering a monster.
            if (_lastAction == LastAction.MenuNavigation || _dice.Random(1, 10) < 10)
            {
                return Actions.GoTo<CombatController>;
            }

            // 1 in 10 chance of finding a chest.
            if (_dice.Random(1, 10) < 10)
            {
                var gold = (_gameWorld.CurrentDungeonLevel * _dice.Roll(9, 6)) + _dice.Roll(1, 6);
                _gameWorld.Player.Gold += gold;
                _encounterDetails = string.Format("You find a chest with {0} gold inside!", gold);
                return Actions.Reload;
            }

            // 1 in 100 chance of finding an awesome weapon.
            var weapon = _weaponFactory.CreateWeapon(_gameWorld.CurrentDungeonLevel.GetBossLevel() + 2, true);
            _gameWorld.Player.AddItemToInventory(weapon);
            _encounterDetails = string.Format("As you take a step, your foot bumps against something. Upon further inspection, you discover that it is a {0}. Awesome!", weapon.GetLeveledName());
            return Actions.Reload;
        }

        private enum LastAction
        {
            MenuNavigation,
            MovedForward
        }
    }
}
