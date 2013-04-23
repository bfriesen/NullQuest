namespace NullQuest.Game.Effects
{
    public interface IEffectFactory
    {
        IEffect DamageHealth(Magnitude magnitude);
        IEffect RestoreHealth(Magnitude magnitude);
        IEffect RestoreEnergy(Magnitude magnitude);
    }
}