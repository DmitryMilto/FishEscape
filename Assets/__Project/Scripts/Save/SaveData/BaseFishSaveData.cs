using Sirenix.OdinInspector;

namespace Scripts.Save
{
    public abstract class BaseFishSaveData : BaseSavingDate
    {
        [Title("Status cardShader")]
        public EnumStatusCard StatusCard = EnumStatusCard.Close;
    }
}
