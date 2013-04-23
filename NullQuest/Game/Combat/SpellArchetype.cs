using System;
using System.Collections.Generic;
using NullQuest.Game.Effects;
using NullQuest.Game.Spells;

namespace NullQuest.Game.Combat
{
    public class SpellArchetype
    {
        public Func<int, IEnumerable<IEffect>, ISpell> SpellCreator { get; set; }
        public string Name { get; set; }
        public IEnumerable<EffectArchetype> Effects { get; set; }
        public int MinimumPower { get; set; }
        public double PowerMultiplier { get; set; }
        public double EnergyCostMultiplier { get; set; }

        public SpellArchetype(Func<int, IEnumerable<IEffect>, ISpell> spellCreator)
        {
            this.SpellCreator = spellCreator;
            PowerMultiplier = 1;
            EnergyCostMultiplier = 1;
        }
    }
}
