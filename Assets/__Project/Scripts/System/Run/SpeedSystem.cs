public class SpeedSystem
{
    public float speed { get; private set; }

    public delegate void SpeedDelegate();
    public SpeedDelegate onSpeedEvent;

    public SpeedSystem(float speed)
    {
        this.speed = speed;
    }

    public void AddSpeed(float speed)
    {
        this.speed += speed;
        onSpeedEvent?.Invoke();
    }
    public void RemoveSpeed(float speed)
    {
        this.speed -= speed;
        onSpeedEvent?.Invoke();
    }
}
