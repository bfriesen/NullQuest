using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NullQuest.Game.Combat;
using NullQuest.Game.Items;

namespace NullQuest.Game
{
    public class Player : Combatant
    {
        private readonly Lazy<StatsModifier> _statModifier;

        public Player()
        {
            _statModifier = new Lazy<StatsModifier>(() => Class + Race);
        }

        public void Rest()
        {
            RestoreHealth(MaxHitPoints);
            RestoreEnergy(MaxEnergy);
        }

        public Class Class { get; set; }
        public Race Race { get; set; }

        protected override StatsModifier BaseStatsModifier
        {
            get { return _statModifier.Value; }
        }

        protected override IEnumerable<ICombatAction> AdditionalActions(IContext context)
        {
            foreach (var useItem in _inventory.Where(x => x.CanUse(this, context)).Take(5).Select(x => new UseItem(x)))
            {
                yield return useItem;
            }

            yield return new Flee();
        }

        public override string BareHandsAttackName
        {
            get
            {
                return Class.BareHandsAttack;
            }
        }

        public void AddExperience(int experienceToAdd)
        {
            Experience += experienceToAdd;
            while (Experience > ExperienceRequiredForNextLevel(Level))
            {
                Level++;
            }
        }

        public double ProgressTowardsNextLevel()
        {
            int expIntoCurrentLevel = Experience - ExperienceRequiredForNextLevel(Level - 1);
            int additionalExperienceForNextLevel = ExperienceRequiredForNextLevel(Level) - ExperienceRequiredForNextLevel(Level - 1);
            return (double)expIntoCurrentLevel/additionalExperienceForNextLevel;
        }

        public static int ExperienceRequiredForNextLevel(int currentLevel)
        {
            if(currentLevel < 0)  throw new ArgumentException("current level can't be less than 0", "currentLevel");
            if (currentLevel == 0) return 0;

            int expSum = 0;
            for (int i = 0; i <= currentLevel; i++)
            {
                expSum += (int)Math.Floor((i * 10.5) * (2 + i));
            }

            return expSum;
        }
    }
}
