using BadSnowstorm;
using NullQuest.Game;

namespace NullQuest.Town
{
    public class TownView : View<TownView, TownViewModel>
    {
        private readonly ContentArea mainArea;
        private readonly ContentArea statsArea;
        private readonly StatsView<TownView, TownViewModel> statsView;

        public TownView()
        {
            mainArea = new ContentArea("mainArea")
            {
                ContentType = ContentType.AsciiArt,
                ContentAlignment = ContentAlignment.Centered,
                Border = new Border(BorderType.SingleLine, BorderType.DoubleLine, BorderType.DoubleLine, BorderType.DoubleLine),
                Bounds = new Rectangle(35, 0, 63, 43),
                BorderRenderOverride = (location, borderInfo) => location.Left == 35 && location.Top == 27 && borderInfo == BorderInfo.SingleLeft
            };

            statsArea = new ContentArea("statsArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(0, 26, 35, 17),
                Border = new Border(BorderType.DoubleLine, BorderType.SingleLine, BorderType.SingleLine, BorderType.DoubleLine)
            };

            statsView = new StatsView<TownView, TownViewModel>(
                new Rectangle(0, 26, 35, 17),
                view => view.statsView,
                viewModel => viewModel.Stats);

            Children.Add(InputArea);
            Children.Add(mainArea);
            Children.Add(statsArea);
            statsArea.Children.Add(statsView);

            Bindings.Add(view => view.mainArea, viewModel => viewModel.AsciiArt);
            Bindings.Add(statsView);
        }

        protected override InputArea CreateInputArea()
        {
            return new InputArea
            {
                Border = new Border(BorderType.DoubleLine, BorderType.SingleLine, BorderType.DoubleLine, BorderType.SingleLine),
                Bounds = new Rectangle(0, 0, 35, 26),
                Padding = new Padding(2, 2, 1, 1),
                ContentAlignment = ContentAlignment.TopLeft
            };
        }
    }
}