using __Project.Scripts.NewSystem.Data.Books;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Elements.UI.Books
{
    public class CollectionFishInfo : MonoBehaviour
    {
[SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Fish Info")] [SerializeField] private Image _fishGameImage;
        [SerializeField] private TextMeshProUGUI _fishNameText;
        [SerializeField] private TextMeshProUGUI _fishDescriptionText;

        [Header("Fish Stats")] [SerializeField]
        private TextMeshProUGUI _healthText;

        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _damageText;

        public void SetVisible(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1 : 0;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }
        public void SetInfo(CollectionInfo info)
        {
            _fishGameImage.sprite = info.FishGameImage;
            _fishNameText.text = info.FishName;
            _fishDescriptionText.text = info.FishDescription;

            _healthText.gameObject.SetActive(info.IsPlayersFish);
            _damageText.gameObject.SetActive(!info.IsPlayersFish);

            if (info.IsPlayersFish)
            {
                _healthText.text = info.Health.ToString();
                _speedText.text = info.Speed.ToString();
            }
            else
            {
                _speedText.text = info.Speed.ToString();
                _damageText.text = info.Damage.ToString();
            }
        }
    }
}