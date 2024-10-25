using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SurvivorGame
{
    public class GameManager : MonoBehaviour
    {
        private GameStateService _gameStateService;
        private const float Delay = 2f;

        [Inject]
        public void Construct(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
            _gameStateService.OnPlayerDead += ReloadScene;
        }

        private void ReloadScene()
        {
            StartCoroutine(ReloadRoutine());
        }

        private IEnumerator ReloadRoutine()
        {
            yield return new WaitForSeconds(Delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}
