using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public class BackgroundProvider : BaseGameProvider
    {
        private readonly GameObject _background;
        public BackgroundProvider(Transform spawnPoint, GameObject background) : base(spawnPoint)
        {
            _background = background;
        }

        public override void NewGame()
        {
            //throw new System.NotImplementedException();
        }

        public override void GameOverGame()
        {
            //throw new System.NotImplementedException();
        }

        public override void ResumeGame()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }

        public override void DestroyProvider()
        {
            //throw new System.NotImplementedException();
        }
    }
}