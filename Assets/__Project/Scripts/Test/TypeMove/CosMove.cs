using UnityEngine;

public class CosMove : TypeMove, ITypeMove
{
    private float frequency;
    private float amplitude;
    public CosMove(float speed, float frequency, float amplitude) : base(speed)
    {
        this.frequency = frequency;
        this.amplitude = amplitude;
    }

    public Vector3 SpeedMove => new Vector3(-1 * this.speed, Mathf.Cos(Time.time * frequency) * amplitude, 0f);
}
