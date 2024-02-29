
using System.Collections;

namespace Scripts.Buffer
{
    public interface IBuffer
    {
        public void SetBuffer(float value);
        public IEnumerator UpdateBuffer();
        public void ResetBuffer();
    }
}