using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SurvivorGame
{
    public class GameManager : MonoBehaviour
    {
        private GameModel _gameModel;
        private const float Delay = 2f;

        [Inject]
        public void Construct(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.OnPlayerDeath += ReloadScene;
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
