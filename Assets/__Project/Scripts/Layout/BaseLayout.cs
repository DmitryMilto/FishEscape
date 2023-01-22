using UnityEngine;
using FishEscape.Enums.Layout;
using System.Collections.Generic;

namespace FishEscape.Layout
{
    public class BaseLayout : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private List<StatusLayout> StatusesLayout;

        protected virtual void Start()
        {
            //TODO: subspribe to action
        }

        protected virtual void CheckStatus(StatusLayout status)
        {
            foreach(var item in StatusesLayout)
            {
                if(item == status)
                {
                    EnableContainer(true);
                    return;
                }
                else
                {
                    EnableContainer(false);
                }
            }
        }

        protected virtual void EnableContainer(bool value)
        {
            _container.SetActive(value);
        }
    }
}
