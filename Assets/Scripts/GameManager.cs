using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public static bool gameOver = false;

    private void OnEnable()
    {
        SpawnPositioner.gameOver += OnGameOver;
    }

    private void OnDisable()
    {
        SpawnPositioner.gameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        gameOver = true;
        _panel.SetActive(true);
    }
}
