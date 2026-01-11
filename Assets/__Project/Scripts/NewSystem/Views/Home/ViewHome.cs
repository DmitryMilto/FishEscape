using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Views.Home
{
    public class ViewHome: AViewBase
    {
        [SerializeField] private Button _buttonShop;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAvatars;
        [SerializeField] private Button _buttonLibrary;
        [SerializeField] private Button _buttonCompany;
        [SerializeField] private Button _buttonFree;

        public Button ButtonShop => _buttonShop;
        public Button ButtonSettings => _buttonSettings;
        public Button ButtonAvatars => _buttonAvatars;
        public Button ButtonLibrary => _buttonLibrary;
        public Button ButtonCompany => _buttonCompany;
        public Button ButtonFree => _buttonFree;
    }
}