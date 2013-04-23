using System;
using System.Collections.Generic;
using System.Linq;
using NullQuest.Data;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Items;

namespace NullQuest.Game.Factories
{
    public class ItemFactory : IItemFactory
    {
        private readonly List<ItemArchetype> items;
        private readonly IDice dice;

        public ItemFactory(IItemDataRepository spellDataRepository, IDice dice)
        {
            items = spellDataRepository.GetAllItems().ToList();
            this.dice = dice;
        }
        public IItem CreateItem(int itemLevel)
        {
            var currentSpellEffects = new List<IEffect>();

            int minAveragePower = 1 + (itemLevel);
            int maxAveragePower = 2 * minAveragePower;

            int basePower = dice.Random(minAveragePower, maxAveragePower);

            var possibleItems = items.Where(x => x.MinimumPower <= basePower);
            var selectedItem = possibleItems.OrderBy(x => dice.Random()).First();

            basePower = (int) (basePower*selectedItem.PowerMultiplier);
            var levels = 1 + Math.Floor(basePower/selectedItem.PerLevelPower);
            var effectsToCreate = new Queue<EffectArchetype>(selectedItem.Effects);
            var levelsPerEffect = (int)Math.Floor(levels/effectsToCreate.Count);

            while (effectsToCreate.Count > 0)
            {
                var curEffect = effectsToCreate.Dequeue();
                currentSpellEffects.Add(curEffect.EffectCreator(new Magnitude(0, 0, levelsPerEffect  * ((int) Math.Floor(selectedItem.PerLevelPower)))));
            }

            var newItem = selectedItem.ItemCreator(currentSpellEffects);
            newItem.Name = selectedItem.Name;
            newItem.Level = levelsPerEffect - 1;

            return newItem;
        }
    }
}