using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Card
{
    public class CardFloatShader : CardShader<float>
    {
        public override void InstanceShow()
        {
            this.image.material.SetFloat(this.NameShader, value);
        }
        public override void InstanceHide()
        {
            this.image.material.SetFloat(this.NameShader, EndValue);
        }
        public override IEnumerator ResetShader()
        {
            var time = this.image.material.GetFloat(NameShader);
            while (time > value)
            {
                this.image.material.SetFloat(NameShader, time);
                yield return new WaitForSeconds(0.01f);
                time += .01f;
            }
        }

        public override IEnumerator UpdateShader()
        {
            var time = value;
            while (time > EndValue)
            {
                this.image.material.SetFloat(NameShader, time);
                yield return new WaitForSeconds(0.01f);
                time -= .01f;
            }
        }
        public override bool isActive()
        {
            return value == this.image.material.GetFloat(this.NameShader);
        }
    }
}