using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Spells;

namespace NullQuest.Data
{
    public class HardCodedSpellDataRepository : ISpellDataRepository
    {
        private readonly IEffectFactory effectFactory;

        public HardCodedSpellDataRepository(IEffectFactory effectFactory)
        {
            this.effectFactory = effectFactory;
        }

        public IEnumerable<SpellArchetype> GetAllSpells()
        {
            yield return new SpellArchetype((i, effects) => new CombatSpell() { EnergyCost = i, Effects = effects.ToList() })
                {
                    Name = "Magic Missile",
                    MinimumPower = 0,
                    Effects = new List<EffectArchetype>()
                        {
                            new EffectArchetype(magnitude => effectFactory.DamageHealth(magnitude))
                        }
                };
            yield return new SpellArchetype((i, effects) => new NonCombatSpell() { EnergyCost = i, Effects = effects.Cast<NonCombatEffect>().ToList() })
                {
                    Name = "Heal",
                    MinimumPower = 0,
                    Effects = new List<EffectArchetype>()
                        {
                            new EffectArchetype(magnitude => effectFactory.RestoreHealth(magnitude))
                        }
                };
            yield return new SpellArchetype((i, effects) => new CombatSpell() { EnergyCost = i, Effects = effects.ToList() })
                {
                    Name = "Lifetap",
                    MinimumPower = 10,
                    EnergyCostMultiplier = 1.2,
                    PowerMultiplier = 0.8,
                    Effects = new List<EffectArchetype>()
                        {
                            new EffectArchetype(magnitude => effectFactory.DamageHealth(magnitude)),
                            new EffectArchetype(magnitude => effectFactory.RestoreHealth(magnitude))
                        }
                };
        }
    }
}
