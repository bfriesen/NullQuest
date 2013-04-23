namespace NullQuest.Game.Combat
{
    public interface ICombatantSelector
    {
        Combatant GetNextCombatant(CombatContext combatContext);
    }
}