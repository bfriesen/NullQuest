using System;
using System.Linq;
using BadSnowstorm;
using NullQuest.Data;
using NullQuest.Town;
using NullQuest.Game;

namespace NullQuest.MainMenu
{
    public class LoadGameController : Controller
    {
        private readonly HardCodedAsciiArtRepository _asciiArtRepository;

        public LoadGameController()
        {
            _asciiArtRepository = new HardCodedAsciiArtRepository();
        }

        public override ViewModel Index()
        {
            var menu = new Menu();
            menu.Prompt = "Select save game slot: ";

            var saveGameRepository = new FileSystemSaveGameRepository();
            var saveGameSlots = saveGameRepository.GetSaveGameSlots()
                .Where(x => !x.IsEmpty)
                .Select((x, i) => new { SaveGameData = x, Index = i + 1 }).ToList();

            foreach (var item in saveGameSlots.OrderBy(x => x.SaveGameData.Id))
            {
                menu.AddMenuItem(
                    new MenuItem
                    {
                        ActionResult = Actions.PopAndGoTo<TownController>,
                        Text = item.SaveGameData.Title,
                        Id = item.Index.ToString()[0],
                        OnInputAccepted = () =>
                        {
                            item.SaveGameData.LoadToGameWorld();
                        }
                    });
            }

            menu.AddMenuItem(
                new MenuItem
                {
                    ActionResult = Actions.GoBack,
                    Text = "Cancel",
                    Id = (saveGameSlots.Count + 1).ToString()[0]
                });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);
            
            viewModel.Message = "Three slots to choose from. Which one will it be?";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }
    }
}