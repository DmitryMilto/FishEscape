using System;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using FishEscape.Enums.Players;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Elements.UI.Tabs
{
    public class UITab : MonoBehaviour
    {
        private static readonly Color ActiveTabColor = new Color(0.976f, 0.941f, 0.788f); // #F9F0C9
        private static readonly Color ActiveIconColor = new Color(0.749f, 0.678f, 0.502f); // #BFAD80
        private static readonly Color InactiveTabColor = new Color(0.866f, 0.792f, 0.604f); // #DDCA9A
        private static readonly Color InactiveIconColor = new Color(0.518f, 0.455f, 0.294f); // #84744B

        [SerializeField] private Image _bg;
        [SerializeField] private Image _icon;
        [SerializeField] private TypeFish _tabType;
        [SerializeField] private Button _buttonClick;

        public event Action<TypeFish> OnChooseTab;
        private bool isChoose;

        public void ChosenTab(TypeFish type)
        {
            if (_tabType == type && !isChoose)
            {
                isChoose = true;
                _bg.color = ActiveTabColor;
                _icon.color = ActiveIconColor;
            }
            else if (_tabType != type)
            {
                isChoose = false;
                _bg.color = InactiveTabColor;
                _icon.color = InactiveIconColor;
            }
        }

        private void ChooseTab()
        {
            if (!isChoose)
                OnChooseTab?.Invoke(_tabType);
        }

        private void Awake()
        {
            _buttonClick.AddListener(ChooseTab);
        }

        private void OnDestroy()
        {
            OnChooseTab = null;
            _buttonClick.RemoveListener(ChooseTab);
        }
    }
}