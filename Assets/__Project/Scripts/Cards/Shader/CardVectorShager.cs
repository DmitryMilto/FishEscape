using System.Collections;
using UnityEngine;

namespace Scripts.Card
{
    public class CardVectorShager : CardShader<Vector2>
    {
        public override void InstanceShow()
        {
            this.image.material.SetVector(this.NameShader, new Vector2(value.x, value.y));
        }
        public override void InstanceHide()
        {
            this.image.material.SetVector(this.NameShader, new Vector2(value.x, EndValue));
        }
        public override IEnumerator ResetShader()
        {
            var time = this.image.material.GetVector(NameShader).y;
            while (time > EndValue)
            {
                this.image.material.SetVector(NameShader, new Vector2(value.x, time));
                yield return new WaitForSeconds(0.01f);
                time += .01f;
            }
        }

        public override IEnumerator UpdateShader()
        {
            var time = value.y;
            while (time > EndValue)
            {
                this.image.material.SetVector(NameShader, new Vector2(value.x, time));
                yield return new WaitForSeconds(0.01f);
                time -= .01f;
            }
        }
        public override bool isActive()
        {
            return value.y == this.image.material.GetVector(this.NameShader).y;
        }
    }
}