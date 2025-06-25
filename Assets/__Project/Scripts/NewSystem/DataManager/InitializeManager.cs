using System;
using __Project.Scripts.NewSystem.Views.Home;
using __Project.Scripts.NewSystem.Views.Managers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.DataManager
{
    public class InitializeManager : MonoBehaviour
    {
        [HideInInspector][Inject] public ViewManager _viewManager;
        
        [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private LifetimeScope sceneContext;
        private LifetimeScope _createdSceneContext;

        private void Awake()
        {
            PreloadScene().Forget();
        }

        private async UniTask PreloadScene()
        {
            _createdSceneContext = Instantiate(sceneContext);
            _createdSceneContext.Container.Inject(this);
            DontDestroyOnLoad(_createdSceneContext.gameObject);

            await UniTask.DelayFrame(1);
            
            if (cameraPrefab == null)
            {
                TDebug.LogError($"{nameof(cameraPrefab)} is not assigned in {nameof(InitializeManager)}.");
                return;
            }

            DontDestroyOnLoad(cameraPrefab);
            await UniTask.DelayFrame(1);
            
            await SceneManager.LoadSceneAsync("Home", LoadSceneMode.Single);
            var home = await _viewManager.OpenViewAsync<ViewHome>();
        }
    }
}