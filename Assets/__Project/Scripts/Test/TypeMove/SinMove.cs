using UnityEngine;

public class SinMove : TypeMove, ITypeMove
{
    private float frequency;
    private float amplitude;
    public SinMove(float speed, float frequency, float amplitude) : base(speed)
    {
        this.frequency = frequency;
        this.amplitude = amplitude;
    }

    public Vector3 SpeedMove => new Vector3(-1 * this.speed, Mathf.Sin(Time.time * frequency) * amplitude, 0f);
}
