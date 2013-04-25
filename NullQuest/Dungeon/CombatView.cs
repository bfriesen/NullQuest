using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.MainMenu;
using BadSnowstorm;

namespace NullQuest.Game.Dungeon
{
    public class CombatView : View<CombatView, CombatViewModel>
    {
        private readonly ContentArea mainArea;
        private readonly ContentArea dungeonNameArea;
        private readonly ContentArea informationArea;
        private readonly ContentArea combatLogArea;
        private readonly ContentArea asciiArtArea;
        private readonly ContentArea statsArea;
        private readonly StatsView<CombatView, CombatViewModel> statsView;

        public CombatView()
        {
            mainArea = new ContentArea("mainArea")
            {
                Border = new Border(BorderType.SingleLine, BorderType.DoubleLine, BorderType.DoubleLine, BorderType.DoubleLine),
                Bounds = new Rectangle(35, 0, 63, 43),
                BorderRenderOverride = (location, borderInfo) => location.Left == 35 && location.Top == 27 && borderInfo == BorderInfo.SingleLeft
            };

            dungeonNameArea = new ContentArea("dungeonNameArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.Centered,
                Bounds = new Rectangle(35, 2, 63, 1)
            };

            informationArea = new ContentArea("informationArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.Centered,
                Border = new Border(BorderType.SingleLine, BorderType.DoubleLine, BorderType.None, BorderType.SingleLine),
                Bounds = new Rectangle(35, 4, 63, 5),
                Padding = new Padding(2, 2, 1, 1)
            };

            combatLogArea = new ContentArea("combatLogArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Border = new Border(BorderType.SingleLine, BorderType.DoubleLine, BorderType.SingleLine, BorderType.DoubleLine),
                Bounds = new Rectangle(35, 9, 63, 34),
                Padding = new Padding(4, 4, 2, 2)
            };

            asciiArtArea = new ContentArea("asciiArtArea")
            {
                ContentType = ContentType.AsciiArt,
                ContentAlignment = ContentAlignment.Centered,
                Border = new Border(BorderType.SingleLine, BorderType.DoubleLine, BorderType.SingleLine, BorderType.DoubleLine),
                Bounds = new Rectangle(35, 9, 63, 34)
            };

            statsArea = new ContentArea("statsArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(0, 26, 35, 17),
                Border = new Border(BorderType.DoubleLine, BorderType.SingleLine, BorderType.SingleLine, BorderType.DoubleLine)
            };

            statsView = new StatsView<CombatView, CombatViewModel>(
                new Rectangle(0, 26, 35, 17),
                view => view.statsView,
                viewModel => viewModel.Stats);

            Children.Add(InputArea);
            Children.Add(mainArea);
            Children.Add(statsArea);
            mainArea.Children.Add(dungeonNameArea);
            mainArea.Children.Add(informationArea);
            mainArea.Children.Add(combatLogArea);
            mainArea.Children.Add(asciiArtArea);
            statsArea.Children.Add(statsView);

            Bindings.Add(view => view.dungeonNameArea, viewModel => viewModel.DungeonName);
            Bindings.Add(view => view.informationArea, viewModel => viewModel.Information);
            Bindings.Add(view => view.combatLogArea, viewModel => viewModel.CombatLog);
            Bindings.Add(view => view.asciiArtArea, viewModel => viewModel.AsciiArt);
            Bindings.Add(statsView);
        }

        protected override InputArea CreateInputArea()
        {
            return new InputArea
            {
                Border = new Border(BorderType.DoubleLine, BorderType.SingleLine, BorderType.DoubleLine, BorderType.SingleLine),
                Bounds = new Rectangle(0, 0, 35, 26),
                Padding = new Padding(2, 2, 1, 1),
                ContentAlignment = ContentAlignment.TopLeft,
                BorderRenderOverride = (location, borderInfo) => location.Left == 35 && location.Top == 9 && borderInfo == BorderInfo.SingleRight
            };
        }
    }
}
