using BadSnowstorm;
using NullQuest.Data;

namespace NullQuest.MainMenu
{
    public class GameLaunchedController : Controller
    {
        private readonly IAsciiArtRepository _asciiArtRepository;

        public GameLaunchedController()
        {
            _asciiArtRepository = new HardCodedAsciiArtRepository();
        }

        public override ViewModel Index()
        {
            var menu = new Menu();
            
            menu.AddMenuItem(new MenuItem { Text = "New Game", Id = '1', ActionResult = Actions.GoTo<NewGameController> });
            menu.AddMenuItem(new MenuItem { Text = "Load Game", Id = '2', ActionResult = Actions.GoTo<LoadGameController> });
            menu.AddMenuItem(new MenuItem { Text = "Exit", Id = '3', ActionResult = Actions.GoBack });

            var viewModel = ViewModel.CreateWithMenu<MainMenuViewModel>(menu);

            viewModel.Message = "Null Quest: it's pretty awesome.";
            viewModel.TitleArt = _asciiArtRepository.GetTitleArt();

            return viewModel;
        }
    }
}