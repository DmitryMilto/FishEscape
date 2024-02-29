using System.Collections;
using UnityEngine;

namespace Scripts.Card
{
    public class CardVectorShager : CardShader<Vector2>
    {
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
    }
}