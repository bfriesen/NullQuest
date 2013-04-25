using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.MainMenu;
using BadSnowstorm;
using NullQuest.Data;
using NullQuest.Game.Factories;
using NullQuest.Game.Effects;

namespace NullQuest.Game.Dungeon
{
    public class CombatController : Controller
    {
        private readonly ICombatEngine _combatEngine;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IEnumerator<CombatStep> _combatSteps;

        public CombatController()
        {
            _combatEngine =
                new CombatEngine(
                    new MonsterFactory(
                        new WeaponFactory(
                            new HardCodedWeaponDataRepository(),
                            new Dice()),
                        new SpellFactory(
                            new HardCodedSpellDataRepository(
                                new EffectFactory()),
                                new Dice()),
                        new ItemFactory(
                            new HardCodedItemDataRepository(
                                new EffectFactory()),
                                new Dice()),
                        new HardCodedMonsterDataRepository(),
                        new MonsterActionSelector(
                            new Dice()),
                        new StatsGenerator(
                            new Dice()),
                        new Dice()),
                    new CombatantSelector(),
                    new Dice());

            _asciiArtRepository = new HardCodedAsciiArtRepository();

            _combatSteps = _combatEngine.GetSteps().GetEnumerator();
        }

        public override ViewModel Index()
        {
            CombatViewModel viewModel = null;

            while (_combatSteps.MoveNext())
            {
                switch (_combatSteps.Current)
                {
                    case CombatStep.Start:
                    {
                        viewModel = ViewModel.CreateWithAutoReloadInput<CombatViewModel>(this, "Thar be monsters here...", 500);
                        break;
                    }
                    case CombatStep.PlayerAction:
                    {
                        var menu = new Menu();
                        foreach (var item in _combatEngine.CombatContext.Player.GetAllowedActions(_combatEngine.CombatContext).Select((a, i) => new { Action = a, Index = i + 1 }))
                        {
                            menu.AddMenuItem(
                                new MenuItem
                                {
                                    Text = item.Action.Name,
                                    Id = item.Index.ToString()[0],
                                    ActionResult = Actions.Reload,
                                    OnInputAccepted = () => _combatEngine.ApplyPlayerAction(item.Action)
                                });
                        }

                        viewModel = ViewModel.CreateWithMenu<CombatViewModel>(menu);
                        break;
                    }
                    case CombatStep.MonsterActionStart:
                    {
                        viewModel = ViewModel.CreateWithAutoUpdateInput<CombatViewModel>(
                            "Monster attacking...",
                            theViewModel => {},
                            new AutoReloadInput(this),
                            500);
                        break;
                    }
                    case CombatStep.MonsterActionEnd:
                    {
                        viewModel = ViewModel.CreateWithAutoReloadInput<CombatViewModel>(this);
                        break;
                    }
                    case CombatStep.PlayerDead:
                    {
                        var menu = new Menu();
                        menu.AddMenuItem(
                            new MenuItem
                            {
                                Text = "Exit to Main Menu",
                                Id = '1',
                                ActionResult = Actions.PopToNearest<GameLaunchedController>
                            });

                        viewModel = ViewModel.CreateWithAutoUpdateInput<CombatViewModel>(
                            "Sad trombone.",
                            theViewModel => {},
                            menu,
                            1000);
                        break;
                    }
                    case CombatStep.MonsterDead:
                    {
                        var menu = new Menu();
                        menu.AddMenuItem(
                            new MenuItem
                            {
                                Text = "Continue",
                                Id = '1',
                                ActionResult = Actions.GoBack
                            });

                        viewModel = ViewModel.CreateWithAutoUpdateInput<CombatViewModel>(
                            "It's happy dance time!",
                            theViewModel => { },
                            menu,
                            1000);
                        break;
                    }
                    case CombatStep.CombatEnded:
                    {
                        var menu = new Menu();
                        menu.AddMenuItem(
                            new MenuItem
                            {
                                Text = "Continue",
                                Id = '1',
                                ActionResult = Actions.GoBack
                            });

                        viewModel = ViewModel.CreateWithAutoUpdateInput<CombatViewModel>(
                            "",
                            theViewModel => { },
                            menu);
                        break;
                    }
                }

                break;
            }

            viewModel.Stats = StatsViewModel.FromPlayer(GameWorld.Player);
            viewModel.DungeonName = string.Format("{0} (Level {1})", GameWorld.GetCurrentDungeonName(), GameWorld.CurrentDungeonLevel);
            viewModel.Information = GetInformation(_combatEngine.CombatContext.CombatLog);
            viewModel.CombatLog = GetCombatLog(_combatEngine.CombatContext.CombatLog);
            viewModel.AsciiArt = GetAsciiArt();

            return viewModel;
        }

        private string GetInformation(IList<CombatLogEntry> logEntries)
        {
            if (_combatSteps.Current == CombatStep.PlayerDead)
            {
                return logEntries[logEntries.Count - 1].Text;
            }

            return string.Format("You see: {0} ({1})", _combatEngine.CombatContext.Monster.Name, _combatEngine.CombatContext.Monster.GetDescription());
        }

        private string GetCombatLog(IList<CombatLogEntry> logEntries)
        {
            if (_combatSteps.Current == CombatStep.PlayerDead)
            {
                return null;
            }

            return string.Join(
                Environment.NewLine + Environment.NewLine,
                logEntries.Reverse().Select(x => x.Text));
        }

        private string GetAsciiArt()
        {
            if (_combatSteps.Current == CombatStep.PlayerDead)
            {
                return _asciiArtRepository.GetPlayerDeadArt(GameWorld.CurrentDungeonLevel);
            }

            return null;
        }
    }
}
