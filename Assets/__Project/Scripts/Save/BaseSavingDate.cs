using System;


namespace Scripts.Save
{
    [Serializable]
    public abstract class BaseSavingDate
    {
        public int Version { get; private set; }
    }
}
