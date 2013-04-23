using System.Collections.Generic;
using System.Linq;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Items;

namespace NullQuest.Data
{
    class HardCodedItemDataRepository : IItemDataRepository
    {
        private readonly IEffectFactory effectFactory;

        public HardCodedItemDataRepository(IEffectFactory effectFactory)
        {
            this.effectFactory = effectFactory;
        }

        public IEnumerable<ItemArchetype> GetAllItems()
        {
            yield return new ItemArchetype(effects => new NonCombatItem() { Effects = effects.Cast<NonCombatEffect>().ToList() })
                {
                    Name = "Heal Potion",
                    MinimumPower = 0,
                    PerLevelPower = 20,
                    PowerMultiplier = 4,
                    Effects = new List<EffectArchetype>()
                        {
                            new EffectArchetype(magnitude => effectFactory.RestoreHealth(magnitude))
                        }
                };
            yield return new ItemArchetype(effects => new NonCombatItem() { Effects = effects.Cast<NonCombatEffect>().ToList() })
                {
                    Name = "Energy Potion",
                    MinimumPower = 0,
                    PerLevelPower = 20,
                    PowerMultiplier = 4,
                    Effects = new List<EffectArchetype>()
                        {
                            new EffectArchetype(magnitude => effectFactory.RestoreEnergy(magnitude))
                        }
                };
        }
    }
}
