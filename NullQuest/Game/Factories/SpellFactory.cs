using System;
using System.Collections.Generic;
using System.Linq;
using NullQuest.Data;
using NullQuest.Game.Combat;
using NullQuest.Game.Effects;
using NullQuest.Game.Spells;

namespace NullQuest.Game.Factories
{
    public class SpellFactory : ISpellFactory
    {
        private readonly List<SpellArchetype> spells;
        private readonly IDice dice;

        public SpellFactory(ISpellDataRepository spellDataRepository, IDice dice)
        {
            spells = spellDataRepository.GetAllSpells().ToList();
            this.dice = dice;
        }

        public ISpell CreateSpell(int spellLevel)
        {
            var currentSpellEffects = new List<IEffect>();
            int energyCost = 0;

            int minAveragePower = (1 + (spellLevel));
            int maxAveragePower = 2 * minAveragePower;

            int basePower = dice.Random(minAveragePower, maxAveragePower);

            var possibleSpells = spells.Where(x => x.MinimumPower <= basePower);
            var selectedSpell = possibleSpells.OrderBy(x => dice.Random()).First();

            var effectsToCreate = new Queue<EffectArchetype>(selectedSpell.Effects);

            double powerTweak = 2.6 + (dice.Random() * 2);
            double energyCostTweak = (powerTweak*1.3) - spellLevel;
            int remainingPower = (int)Math.Floor(powerTweak * basePower * selectedSpell.PowerMultiplier);
            energyCost = (int)Math.Floor(energyCostTweak * basePower * selectedSpell.EnergyCostMultiplier);

            while (effectsToCreate.Count > 0)
            {
                var curEffect = effectsToCreate.Dequeue();
                int powerToUse = 0;
                if (effectsToCreate.Count > 0)
                {
                    powerToUse = dice.Random(1, remainingPower - effectsToCreate.Count);
                }
                else
                {
                    powerToUse = remainingPower;
                }
                remainingPower -= powerToUse;
                currentSpellEffects.Add(curEffect.EffectCreator(CreateMagnitude(powerToUse)));
            }

            var newSpell = selectedSpell.SpellCreator(energyCost, currentSpellEffects);
            newSpell.Name = selectedSpell.Name;
            newSpell.EnergyCost = energyCost;
            newSpell.Level = (int)Math.Floor((double) (basePower)/10);

            return newSpell;
        }

        private static Magnitude CreateMagnitude(int power)
        {
            if(power < 1) return new Magnitude();
            if(power < 3) return new Magnitude(0, 0, power);
            if(power < 5) return new Magnitude(1, 4, power - 3);

            var randomDice = RandomDiceSize((power - 1)*2);
            var numDice = (int) Math.Floor((double) (power/((randomDice + 1)/2)));
            var mod = power - new Magnitude(numDice, randomDice, 0).GetAverageValue();
            return new Magnitude(numDice, randomDice, (int) mod);
        }

        private static int RandomDiceSize(int maxSize)
        {
            if (maxSize < 4) throw new ArgumentException();
            var pool = new[] {4, 6, 8, 10, 12, 20, 100}.Where(x => x <= maxSize).ToList();
            return pool[new Random().Next(0, pool.Count)];
        }
    }
}
