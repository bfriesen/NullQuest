using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    public class MonsterActionSelector : IActionSelector
    {
        private readonly IDice dice;

        public MonsterActionSelector(IDice dice)
        {
            this.dice = dice;
        }

        public ICombatAction SelectAction(CombatContext combatContext, IEnumerable<ICombatAction> allowedActions)
        {
            // Get a list of all possible actions that the monster can make
            var allowedActionsList = allowedActions.ToList();

            // Calculate the weighted results based on past performance (or expected performance if action not yet exectued in the current combat)
            // This will return a Dictionary of Action Hashcode (int) to Weight (int)
            var evaluation = GetWeights(combatContext, allowedActionsList);

            // Sum the weights, and generate a number between 1 and the sum of the weights. 
            // Example: If we have two actions weighted 20 and 80, we will generate a number between 1 and 100 (20+80)
            var sumOfWeights = evaluation.Sum(x => x.Value);
            var randomNumberInWeightedRange = dice.Random(1, sumOfWeights);

            // Use the randomly selected weight to choose the correct action.
            // Example: If we have two actions weighted 20 and 80, a random number of 1-20 will select the first option, and 21-100 will select the second option.
            int runningTotal = 0;
            foreach (var possibleAction in evaluation)
            {
                if (possibleAction.Value + runningTotal >= randomNumberInWeightedRange)
                {
                    return allowedActionsList.Single(x => x.CreateHash() == possibleAction.Key);
                }
                else
                {
                    runningTotal += possibleAction.Value;
                }
            }

            throw new InvalidOperationException();
        }

        public Dictionary<int, int> GetWeights(CombatContext combatContext, IEnumerable<ICombatAction> allowedActions)
        {
            // Examine the combat log and create a lookup of ICombatAction (using it's hashcode (int)) to CombatLogEntry
            var monsterActionsFromCombat = combatContext.CombatLog.Where(entry => entry.Attacker == combatContext.Monster && entry is CombatLogEntryFromAction).Cast<CombatLogEntryFromAction>();
            ILookup<int, CombatLogEntryFromAction> lookups = monsterActionsFromCombat.ToLookup(x => x.Type.CreateHash(x.Name));
            
            // Setup a dictionary to hold the weights of Action Hashcode (int) to Weight (int)
            var weights = new Dictionary<int, int>();

            // Iterate through each action the monster can perform right now

            bool first = true;
            foreach (var allowedAction in allowedActions)
            {
                int weight = 0;
                // If the action's hashcode exists in the lookup table, average the historical outcomes to create the weight of that action
                if (lookups[allowedAction.CreateHash()].Any())
                {
                    weight = (int)lookups[allowedAction.CreateHash()].Average(x => x.CombatEffect.Damage + x.CombatEffect.Healing);
                }
                // If it doesn't exist in the lookup table, use the expected performance as the weight
                else
                {
                    var potentialEffect = allowedAction.GetPotentialCombatOutcome(combatContext.Monster);
                    weight = potentialEffect.Damage + potentialEffect.Healing;
                }

                // The first action must have a minimum weight of 1
                weights[allowedAction.CreateHash()] = first ? Math.Max(1, weight) : weight;
                first = false;
            }

            return weights;
        }
    }
}
