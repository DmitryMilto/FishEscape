using System.Threading;
using __Project.Scripts.NewSystem.Fishes.Players;
using __Project.Scripts.NewSystem.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public class PlayerFishManager : BaseGameProvider
    {
        private readonly PlayerFishBase _playerPrefab;
        private readonly Camera _mainCamera;
        private CancellationTokenSource _moveCts;

        public PlayerFishBase CurrentFish { get; private set; }
        private const float SpawnOffsetMultiplier = 10f;

        public PlayerFishManager(Transform spawnPoint, PlayerFishBase playerPrefab) : base(spawnPoint)
        {
            _mainCamera = Camera.main;
            _playerPrefab = playerPrefab;
            SpawnFish();
        }

        public override void NewGame() => SpawnFish();

        public override void GameOverGame() => DestroyAllFishes();

        public override void ResumeGame() => SpawnFish();

        public override void Update()
        {
            if (isPauseGame) return;
            if (Input.GetMouseButton(0))
            {
                var mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                MoveToMouse(mouseWorld);
            }

            var moveY = Input.GetAxis("Vertical");
            if (Mathf.Abs(moveY) > 0.01f)
            {
                MoveY(moveY);
            }
        }

        public override void PauseGame(bool isPause)
        {
            TDebug.Log($"{_nameLog}: Pause game - {isPause}");
            isPauseGame = isPause;
            CurrentFish?.SetPause(isPause);
        }

        public override void DestroyProvider()
        {
            DestroyAllFishes();
            Dispose();
        }

        private void Dispose()
        {
            // Для предотвращения утечек памяти
            _moveCts?.Cancel();
            _moveCts = null;
        }

        private void SpawnFish()
        {
            if (_playerPrefab == null) return;

            var fishInstance = Object.Instantiate(_playerPrefab, _spawnPoint);
            var leftX = ScreenBoundsUtils.GetLeftScreenX();
            var sizeX = fishInstance.Sprite.bounds.size.x;
            fishInstance.transform.position = new Vector3(leftX + (SpawnOffsetMultiplier * sizeX), 0, 0);
            CurrentFish = fishInstance;
            TDebug.Log(
                $"{_nameLog}: Spawned new fish instance {CurrentFish.name} at {CurrentFish.transform.position.x}, {CurrentFish.transform.position.y}, {CurrentFish.transform.position.z}.");
        }

        private void DestroyAllFishes()
        {
            _moveCts?.Cancel();
            _moveCts = null;
            if (CurrentFish == null) return;
            Object.Destroy(CurrentFish.gameObject);
            CurrentFish = null;
        }

        private void MoveY(float moveY)
        {
            if (isPauseGame || CurrentFish == null) return;
            CurrentFish.transform.Translate(Vector3.up * (moveY * CurrentFish.Speed * Time.deltaTime));
        }

        private void MoveToMouse(Vector3 mouseWorld)
        {
            if (isPauseGame || CurrentFish == null) return;
            _moveCts?.Cancel();
            _moveCts = new CancellationTokenSource();
            MoveToYAsync(mouseWorld.y, _moveCts.Token).Forget();
        }

        private async UniTaskVoid MoveToYAsync(float targetY, CancellationToken token)
        {
            var fish = CurrentFish;
            if (fish == null) return;
            var pos = fish.transform.position;
            var target = new Vector3(pos.x, targetY, pos.z);

            while (fish != null && Mathf.Abs(fish.transform.position.y - targetY) > 0.01f)
            {
                if (isPauseGame || token.IsCancellationRequested) break;
                if (fish == null) break;
                fish.transform.position = Vector3.MoveTowards(
                    fish.transform.position,
                    target,
                    fish.Speed * Time.deltaTime
                );
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}