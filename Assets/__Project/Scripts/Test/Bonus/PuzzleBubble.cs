using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;

public class PuzzleBubble : BaseBonus<EnumPuzzle>
{
    public override void Send()
    {
        switch (bonus)
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
