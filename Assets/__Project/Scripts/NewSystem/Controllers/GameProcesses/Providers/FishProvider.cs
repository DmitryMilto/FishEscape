using __Project.Scripts.NewSystem.Fishes.Players;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public class FishProvider : BaseGameProvider
    {
        private readonly PlayerFishBase _player;
        private readonly Camera _mainCamera;
        
        private PlayerFishBase _currentFish;
        
        public FishProvider(Transform spawnPoint,PlayerFishBase player) : base(spawnPoint)
        {
            _mainCamera = Camera.main;
            _player = player;
            SpawnFish();
        }

        public override void NewGame()
        {
            SpawnFish();
        }

        public override void GameOverGame()
        {
            DestroyAllFishes();
        }

        public override void ResumeGame()
        {
            SpawnFish();
        }

        public override void Update()
        {
            if (isPauseGame) return;
        }

        public override void DestroyProvider()
        {
            DestroyAllFishes();
        }
        
        private void SpawnFish()
        {
            if (_player == null) return;
            if (_currentFish != null)
            {
                TDebug.Log($"{_nameLog}: Destroying previous fish instance.");
                UnityEngine.Object.Destroy(_currentFish.gameObject);
            }

            var fishInstance = UnityEngine.Object.Instantiate(_player, _spawnPoint);
            var leftX = ScreenBoundsUtils.GetLeftScreenX();
            var sizeX = fishInstance.Sprite.bounds.size.x;
            fishInstance.transform.position = new Vector3(leftX + (10f * sizeX), 0, 0);
            _currentFish = fishInstance;
        }
        private void DestroyAllFishes()
        {
            if (_currentFish != null)
            {
                UnityEngine.Object.Destroy(_currentFish.gameObject);
                _currentFish = null;
            }
        }

        public void MoveY(float moveY)
        {
            if (isPauseGame) return;
            _player.transform.Translate(Vector3.up * (moveY * _currentFish.Speed * Time.deltaTime));
        }

        public void MouseMove(Vector3 mouseWorld)
        {
            if (isPauseGame) return;
            float targetY = mouseWorld.y;
            _player.transform.position = new Vector3(_player.transform.position.x, targetY,
                _player.transform.position.z);
        }
    }
}