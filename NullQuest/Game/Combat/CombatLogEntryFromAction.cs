using System;

namespace NullQuest.Game.Combat
{
    public abstract class CombatLogEntryFromAction : CombatLogEntry
    {
        public abstract Type Type { get; }
        public string Name { get; private set; }

        protected CombatLogEntryFromAction(string name)
        {
            Name = name;
        }
    }

    public class CombatLogEntryFromAction<TType> : CombatLogEntryFromAction
        where TType : ICombatAction
    {
        public CombatLogEntryFromAction(string name)
            : base(name)
        {
        }

        public override Type Type
        {
            get { return typeof(TType); }
        }
    }
}
