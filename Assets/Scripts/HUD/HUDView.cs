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

        public void UpdateEnemiesDestroyed(int currentKill)
        {
            _enemiesDestroyedText.text = $"Kills: {currentKill}";
        }

        public void UpdateHealthBar(int health, int maxHealth)
        {
            _healthBar.value = (float)health / maxHealth;
        }

        public void HideJoystick()
        {
            _joystick.gameObject.SetActive(false);
        }

    }
}
