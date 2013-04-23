namespace NullQuest.Game.Combat
{
    public class CombatLogEntry
    {
        public CombatLogEntry()
        {
            CombatEffect = new CombatOutcome();
        }

        public string Text { get; set; }
        public int TimePassed { get; set; }
        public int Round { get; set; }

        public Combatant Attacker { get; set; }
        public CombatOutcome CombatEffect { get; set; }
    }
}
