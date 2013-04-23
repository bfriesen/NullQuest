using BadSnowstorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class StatsView<TParentView, TParentViewModel> : PartialView<TParentView, StatsView<TParentView, TParentViewModel>, TParentViewModel, StatsViewModel>
        where TParentView : View<TParentView, TParentViewModel>, IView
        where TParentViewModel : ViewModel
    {
        private readonly ContentArea nameArea;
        private readonly ContentArea levelArea;
        private readonly ContentArea goldArea;
        private readonly ContentArea strengthArea;
        private readonly ContentArea enduranceArea;
        private readonly ContentArea dexterityArea;
        private readonly ContentArea agilityArea;
        private readonly ContentArea intelligenceArea;
        private readonly ContentArea wisdomArea;
        private readonly ContentArea luckArea;
        private readonly ContentArea hitPointsArea;
        private readonly ContentArea hitPointsMeterArea;
        private readonly ContentArea energyArea;
        private readonly ContentArea energyMeterArea;
        private readonly ContentArea experienceArea;
        private readonly ContentArea experienceMeterArea;
        private readonly ContentArea weaponArea;

        public StatsView(Rectangle bounds, Func<TParentView, StatsView<TParentView, TParentViewModel>> getPartialView, Func<TParentViewModel, StatsViewModel> getPartialViewModel)
            : base(getPartialView, getPartialViewModel)
        {
            var top = bounds.Top + 2;
            var left = bounds.Left + 3;
            var width = bounds.Width;

            nameArea = new ContentArea("nameArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.Centered,
                Bounds = new Rectangle(bounds.Left, top, bounds.Width, 1)
            };
            Children.Add(nameArea);
            Bindings.Add(view => view.nameArea, viewModel => viewModel.Name);

            levelArea = new ContentArea("levelArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 2, width / 2, 1)
            };
            Children.Add(levelArea);
            Bindings.Add(view => view.levelArea, viewModel => viewModel.Level);

            goldArea = new ContentArea("goldArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 2, width / 2, 1)
            };
            Children.Add(goldArea);
            Bindings.Add(view => view.goldArea, viewModel => viewModel.Gold);

            strengthArea = new ContentArea("strengthArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 4, width / 2, 1)
            };
            Children.Add(strengthArea);
            Bindings.Add(view => view.strengthArea, viewModel => viewModel.Strength);

            enduranceArea = new ContentArea("enduranceArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 5, width / 2, 1)
            };
            Children.Add(enduranceArea);
            Bindings.Add(view => view.enduranceArea, viewModel => viewModel.Endurance);

            dexterityArea = new ContentArea("dexterityArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 6, width / 2, 1)
            };
            Children.Add(dexterityArea);
            Bindings.Add(view => view.dexterityArea, viewModel => viewModel.Dexterity);

            agilityArea = new ContentArea("agilityArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 7, width / 2, 1)
            };
            Children.Add(agilityArea);
            Bindings.Add(view => view.agilityArea, viewModel => viewModel.Agility);

            intelligenceArea = new ContentArea("intelligenceArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 4, width / 2, 1)
            };
            Children.Add(intelligenceArea);
            Bindings.Add(view => view.intelligenceArea, viewModel => viewModel.Intelligence);

            wisdomArea = new ContentArea("wisdomArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 5, width / 2, 1)
            };
            Children.Add(wisdomArea);
            Bindings.Add(view => view.wisdomArea, viewModel => viewModel.Wisdom);

            luckArea = new ContentArea("luckArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 6, width / 2, 1)
            };
            Children.Add(luckArea);
            Bindings.Add(view => view.luckArea, viewModel => viewModel.Luck);

            hitPointsArea = new ContentArea("hitPointsArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 9, width / 2, 1)
            };
            Children.Add(hitPointsArea);
            Bindings.Add(view => view.hitPointsArea, viewModel => viewModel.HitPoints);

            hitPointsMeterArea = new ContentArea("hitPointsMeterArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 9, width / 2, 1)
            };
            Children.Add(hitPointsMeterArea);
            Bindings.Add(view => view.hitPointsMeterArea, viewModel => viewModel.HitPointsMeter);

            energyArea = new ContentArea("energyArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 10, width / 2, 1)
            };
            Children.Add(energyArea);
            Bindings.Add(view => view.energyArea, viewModel => viewModel.Energy);

            energyMeterArea = new ContentArea("energyMeterArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 10, width / 2, 1)
            };
            Children.Add(energyMeterArea);
            Bindings.Add(view => view.energyMeterArea, viewModel => viewModel.EnergyMeter);

            experienceArea = new ContentArea("experienceArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 11, width / 2, 1)
            };
            Children.Add(experienceArea);
            Bindings.Add(view => view.experienceArea, viewModel => viewModel.Experience);

            experienceMeterArea = new ContentArea("experienceMeterArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left + (width / 2) + 1, top + 11, width / 2, 1)
            };
            Children.Add(experienceMeterArea);
            Bindings.Add(view => view.experienceMeterArea, viewModel => viewModel.ExperienceMeter);

            weaponArea = new ContentArea("weaponArea")
            {
                ContentType = ContentType.Text,
                ContentAlignment = ContentAlignment.TopLeft,
                Bounds = new Rectangle(left, top + 13, width, 1)
            };
            Children.Add(weaponArea);
            Bindings.Add(view => view.weaponArea, viewModel => viewModel.Weapon);
        }
    }
}
