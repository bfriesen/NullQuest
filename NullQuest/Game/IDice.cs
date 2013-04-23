namespace NullQuest.Game
{
    public interface IDice
    {
        int Roll(int numberOfDice, int numberOfSides);
        int Roll(Magnitude magnitude);
        double Random();
        int Random(int min, int max);
    }
}
