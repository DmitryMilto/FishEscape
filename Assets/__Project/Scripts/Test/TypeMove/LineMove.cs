using UnityEngine;

public class LineMove : TypeMove, ITypeMove
{
    public LineMove(float speed) : base(speed)
    {
    }

    public Vector3 SpeedMove => new Vector3(-1 * this.speed, 0f, 0f);
}
