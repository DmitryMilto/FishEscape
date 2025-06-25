using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Views.Base
{
    public abstract class PopupBase : ViewBase
    {
        [SerializeField] protected TypePopupBackground _popupBackground = TypePopupBackground.None;
        [SerializeField] protected Button _closeButton;
        
        public TypePopupBackground Background => _popupBackground;
        protected override void Awake()
        {
            base.Awake();
            if (_closeButton != null)
            {
                _closeButton.AddListener(ClosePopup);
            }
        }

        protected virtual void ClosePopup()
        {
            TDebug.Log($"{_nameLog} : Closing popup {this.name}");
            _manager.CloseViewAsync(this).Forget();
        }
    }
}