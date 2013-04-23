using System;
using NullQuest.Game.Effects;

namespace NullQuest.Game.Combat
{
    public class EffectArchetype
    {
        public Func<Magnitude, IEffect> EffectCreator { get; set; }

        public EffectArchetype(Func<Magnitude, IEffect> effectCreator)
        {
            EffectCreator = effectCreator;
        }
    }
}