using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;

public class InitializePuzzle : InitializeBase<EnumPuzzle>
{
    public PuzzleBubble puzzleBubble { get; private set; }
    public override void Initialize(EmptyDatabaseBonus<EnumPuzzle> bonus)
    {
        if (bonus != null)
        {
            puzzleBubble = new PuzzleBubble(bonus.TypeBonus);
            bonusDB = bonus;

            this.Setting();
        }
    }
    public override void Send()
    {
        puzzleBubble.Send();
    }
}
