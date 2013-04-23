using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Effects;
using NullQuest.Game.Items;
using NullQuest.Game.Spells;

namespace NullQuest.Game.Combat
{
    public class ItemArchetype
    {
        public Func<IEnumerable<IEffect>, IItem> ItemCreator { get; set; }
        public string Name { get; set; }
        public IEnumerable<EffectArchetype> Effects { get; set; }
        public int MinimumPower { get; set; }
        public double PerLevelPower { get; set; }
        public double GoldValueMultiplier { get; set; }
        public double PowerMultiplier { get; set; }

        public ItemArchetype(Func<IEnumerable<IEffect>, IItem> itemCreator)
        {
            ItemCreator = itemCreator;
            PerLevelPower = 1;
            GoldValueMultiplier = 1;
        }
    }
}
