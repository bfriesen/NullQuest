namespace NullQuest.Game.Combat
{
    public class CombatOutcome
    {
        public static readonly CombatOutcome Empty = new CombatOutcome();

        public int Healing { get; set; }
        public int Damage { get; set; }

        public static CombatOutcome operator +(CombatOutcome c1, CombatOutcome c2)
        {
            return new CombatOutcome
            {
                Healing = c1.Healing + c2.Healing,
                Damage = c1.Damage + c2.Damage
            };
        }
    }
}
