using DG.Tweening;

namespace __Project.Scripts.NewSystem.Fishes.Players.Providers
{
    public class FishBlinkProvider
    {
        private readonly PlayerFishBase _fish;
        private Sequence _blinkSequence;
        private bool _isBlinking;

        public FishBlinkProvider(PlayerFishBase fish)
        {
            _fish = fish;
        }

        public void StartBlink()
        {
            if (_isBlinking) return;
            _isBlinking = true;
            _blinkSequence = DOTween.Sequence();
            _blinkSequence.Append(_fish.Sprite.DOFade(0f, 0.15f))
                .Append(_fish.Sprite.DOFade(1f, 0.15f))
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopBlink()
        {
            if (!_isBlinking) return;
            _isBlinking = false;
            _blinkSequence?.Kill(true);
            _blinkSequence = null;
            _fish.Sprite.DOFade(1f, 0f);
        }
    }
}