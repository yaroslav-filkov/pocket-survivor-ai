using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private Creature _creatureObserveDeath;
    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private Button _restratButton;
    [SerializeField] private string _sceneRestartName = "Game";
    private void OnEnable()
    {
        _creatureObserveDeath.Died += OnDied;
        _restratButton.onClick.AddListener(Restart);
    }
    private void OnDisable()
    {
        _creatureObserveDeath.Died -= OnDied;
        _restratButton.onClick.RemoveListener(Restart);
    }
    private void OnDied(DeathData deathData)
    {
        _gameOverWindow.SetActive(true);
    }

    private void Restart()
    {
        SceneManager.LoadScene(_sceneRestartName);
    }
}
