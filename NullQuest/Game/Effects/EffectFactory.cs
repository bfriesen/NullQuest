using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Effects.Library;

namespace NullQuest.Game.Effects
{
    public class EffectFactory : IEffectFactory
    {
        public IEffect DamageHealth(Magnitude magnitude)
        {
            return new DamageHealthEffect { Magnitude = magnitude };
        }

        public IEffect RestoreHealth(Magnitude magnitude)
        {
            return new RestoreHealthEffect { Magnitude = magnitude };
        }

        public IEffect RestoreEnergy(Magnitude magnitude)
        {
            return new RestoreEnergyEffect() { Magnitude = magnitude };
        }
    }
}
