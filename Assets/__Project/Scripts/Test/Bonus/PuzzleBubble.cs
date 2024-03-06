using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;

public class PuzzleBubble : BaseBonus<EnumPuzzle>
{
    public EnumPuzzle value { get; private set; }

    public PuzzleBubble(EnumPuzzle value)
    {
        this.value = value;
    }

    public void Send()
    {
        switch (value)
        {
            case EnumPuzzle.FishIndian1: 
                break;

            case EnumPuzzle.FishIndian2: 
                break;

            default: 
                break;
        }
    }
}
