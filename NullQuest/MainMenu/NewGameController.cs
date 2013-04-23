using System;
using System.Linq;
using NullQuest.Data;
using NullQuest.Game;
using BadSnowstorm;
using NullQuest.Town;

namespace NullQuest.MainMenu
{
    public class NewGameController : Controller
    {
        private readonly ISaveGameRepository _saveGameRepository;
        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IStatsGenerator _statsGenerator;
        private readonly GameWorld _gameWorld;
        private readonly GameInfo _gameInfo = new GameInfo();

        public NewGameController(ISaveGameRepository saveGameRepository, IAsciiArtRepository asciiArtRepository, IStatsGenerator statsGenerator, GameWorld gameWorld)
        {
            _saveGameRepository = saveGameRepository;
            _asciiArtRepository = asciiArtRepository;
            _statsGenerator = statsGenerator;
            _gameWorld = gameWorld;
        }

        public override ViewModel Index()
        {
            switch (_gameInfo.Step)
            {
                case GameCreationStep.CharacterName:
                    return GetCharacterNameViewModel();
                case GameCreationStep.ChooseSaveGameSlot:
                    return GetChooseSaveGameSlotViewModel();
                case GameCreationStep.ConfirmOverwrite:
                    return GetConfirmOverwriteViewModel();
                case GameCreationStep.Race:
                    return GetSelectRaceViewModel();
                case GameCreationStep.Class:
                    return GetSelectClassViewModel();
                case GameCreationStep.RollForStats:
                    return GetRollForStatsViewModel();
                default:
                    throw new InvalidOperationException();
            }
        }

        private ViewModel GetCharacterNameViewModel()
        {
            var textInput = new TextInput(Actions.Reload);
            textInput.Prompt = string.Format("Enter character name:{0}Hit <Escape> to cancel.{0}", Environment.NewLine);
            textInput.OnInputAccepted = () =>
            {
                _gameInfo.CharacterName = textInput.Value;
                _gameInfo.Step = GameCreationStep.ChooseSaveGameSlot;
            };

            var viewModel = ViewModel.CreateWithTextInput<MainMenuViewModel>(textInput);

            viewModel.Message = "You're gonna need a name. Make it a good one.";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }

        private ViewModel GetChooseSaveGameSlotViewModel()
        {
            var menu = new Menu();
            menu.Prompt = "Select save game slot: ";

            var saveGameSlots = _saveGameRepository.GetSaveGameSlots().ToList();

            foreach (var saveGameSlot in saveGameSlots.OrderBy(x => x.Id))
            {
                menu.AddMenuItem(
                    new MenuItem
                    {
                        ActionResult = Actions.Reload,
                        Text = saveGameSlot.Title,
                        Id = saveGameSlot.Id.ToString()[0],
                        OnInputAccepted = () =>
                        {
                            _gameInfo.SaveGameSlotPosition = saveGameSlot.Id;
                            if (saveGameSlot.IsEmpty)
                            {
                                _gameInfo.Step = GameCreationStep.Race;
                            }
                            else
                            {
                                _gameInfo.Step = GameCreationStep.ConfirmOverwrite;
                            }
                        }
                    });
            }

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = (saveGameSlots.Max(x => x.Id) + 1).ToString()[0]
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "Three slots to choose from. Which one will it be?";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }

        private ViewModel GetConfirmOverwriteViewModel()
        {
            var menu = new Menu();
            menu.Prompt = "Confirm overwrite? ";

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Yes",
                    Id = '1',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Step = GameCreationStep.Race;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "No",
                    Id = '2',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Step = GameCreationStep.ChooseSaveGameSlot;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = '3'
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "A game already exists in that slot. Do you want to throw it all away and start anew?";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }

        private ViewModel GetSelectRaceViewModel()
        {
            var menu = new Menu();
            menu.Prompt = "Select your race: ";

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Human",
                    Id = '1',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Race = Race.Human;
                        _gameInfo.Step = GameCreationStep.Class;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Elf",
                    Id = '2',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Race = Race.Elf;
                        _gameInfo.Step = GameCreationStep.Class;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Orc",
                    Id = '3',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Race = Race.Orc;
                        _gameInfo.Step = GameCreationStep.Class;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Dwarf",
                    Id = '4',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Race = Race.Dwarf;
                        _gameInfo.Step = GameCreationStep.Class;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = '5'
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "I'm not racist, but you'll have to choose your race now.";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }

        private ViewModel GetSelectClassViewModel()
        {
            var menu = new Menu();
            menu.Prompt = "Select your class: ";

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Fighter",
                    Id = '1',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Class = Class.Fighter;
                        _gameInfo.Step = GameCreationStep.RollForStats;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Wizard",
                    Id = '2',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Class = Class.Wizard;
                        _gameInfo.Step = GameCreationStep.RollForStats;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Thief",
                    Id = '3',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Class = Class.Thief;
                        _gameInfo.Step = GameCreationStep.RollForStats;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Cleric",
                    Id = '4',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Class = Class.Cleric;
                        _gameInfo.Step = GameCreationStep.RollForStats;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Bard",
                    Id = '5',
                    OnInputAccepted = () =>
                    {
                        _gameInfo.Class = Class.Bard;
                        _gameInfo.Step = GameCreationStep.RollForStats;
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = '6'
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "Now's your chance to move up in the world. Choose your class.";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }

        private ViewModel GetRollForStatsViewModel()
        {
            var menu = new Menu();
            menu.Prompt = "What'll it be? ";

            _gameInfo.Stats = _statsGenerator.GenerateStats(_gameInfo.Race, _gameInfo.Class);

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.PopAndGoTo<TownController>,
                    Text = "Accept and start game",
                    Id = '1',
                    OnInputAccepted = () =>
                    {
                        var saveGameData =
                            _saveGameRepository.CreateGame(
                                _gameInfo.CharacterName,
                                _gameInfo.SaveGameSlotPosition,
                                _gameInfo.Race,
                                _gameInfo.Class,
                                _gameInfo.Stats);

                        saveGameData.LoadToGameWorld(_gameWorld);
                    }
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.Reload,
                    Text = "Roll again",
                    Id = '2'
                });

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = '3'
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "Here are your stats. If you don't like them, roll again.";
            viewModel.TitleArt = string.Format(@"    Strength : {0}
   Endurance : {1}
   Dexterity : {2}
     Agility : {3}
Intelligence : {4}
      Wisdom : {5}
        Luck : {6}{7}",
                _gameInfo.Stats.Strength.ToString().PadRight(2),
                _gameInfo.Stats.Endurance.ToString().PadRight(2),
                _gameInfo.Stats.Dexterity.ToString().PadRight(2),
                _gameInfo.Stats.Agility.ToString().PadRight(2),
                _gameInfo.Stats.Intelligence.ToString().PadRight(2),
                _gameInfo.Stats.Wisdom.ToString().PadRight(2),
                _gameInfo.Stats.Luck.ToString().PadRight(2),
                string.Join("", Enumerable.Repeat(Environment.NewLine, 16)));

            return viewModel;
        }

        private class GameInfo
        {
            public GameCreationStep Step { get; set; }
            public string CharacterName { get; set; }
            public int SaveGameSlotPosition { get; set; }
            public Race Race { get; set; }
            public Class Class { get; set; }
            public CharacterStats Stats { get; set; }
        }

        private enum GameCreationStep
        {
            CharacterName,
            ChooseSaveGameSlot,
            ConfirmOverwrite,
            Race,
            Class,
            RollForStats
        }
    }
}