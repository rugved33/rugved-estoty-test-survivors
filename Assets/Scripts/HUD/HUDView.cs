using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Zenject;
namespace SurvivorGame
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemiesDestroyedText;  
        [Inject] private Joystick _joystick;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Slider _levelUpBar;

        public void UpdateEnemiesDestroyed(int currentKill, int killTarget)
        {
            _enemiesDestroyedText.text = $"Kills: {currentKill}";
            _levelUpBar.value = (float)currentKill / killTarget;
        }

        public void UpdateHealthBar(int health, int maxHealth)
        {
            _healthBar.value = (float)health / maxHealth;
        }

        public void HideJoystick()
        {
            _joystick.gameObject.SetActive(false);
        }
        public void ShowJoystick()
        {
            _joystick.gameObject.SetActive(true);
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
