using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace __Project.Scripts.NewSystem.Elements
{
    [RequireComponent(typeof(Button))]
    public class UIButtonSound : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        public SoundType soundType = SoundType.ButtonClick;

        public 
        void Awake()
        {
            GetComponent<Button>().AddListener(OnClick);
        }

        void OnClick()
        {
            _gameManager.Audio.Play(soundType, SoundCategory.Sfx);
        }
    }
}