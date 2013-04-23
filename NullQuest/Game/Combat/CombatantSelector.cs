namespace NullQuest.Game.Combat
{
    public class CombatantSelector : ICombatantSelector
    {
        private bool _monsterIsNext;

        public Combatant GetNextCombatant(CombatContext combatContext)
        {
            _monsterIsNext = !_monsterIsNext;
            return _monsterIsNext ? (Combatant) combatContext.Monster : combatContext.Player;
        }
    }
}
