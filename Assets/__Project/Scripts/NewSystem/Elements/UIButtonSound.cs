using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Elements
{
    [RequireComponent(typeof(Button))]
    public class UIButtonSound : MonoBehaviour
    {
        public SoundType soundType = SoundType.ButtonClick;

        public 
        void Awake()
        {
            GetComponent<Button>().AddListener(OnClick);
        }

        void OnClick()
        {
            GameManager.Audio.Play(soundType, SoundCategory.Sfx);
        }
    }
}