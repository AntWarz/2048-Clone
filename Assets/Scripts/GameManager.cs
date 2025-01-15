using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public static bool gameOver = false;

    private void OnEnable()
    {
        SquareMover.gameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        SquareMover.gameOverEvent -= OnGameOver;
    }

    private void OnGameOver()
    {
        gameOver = true;
        _panel.SetActive(true);
    }
}
