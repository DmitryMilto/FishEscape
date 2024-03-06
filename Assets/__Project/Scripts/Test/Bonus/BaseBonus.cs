using System;

public interface BaseBonus<T> where T : Enum
{
    public T value { get; }
    public void Send();
}
