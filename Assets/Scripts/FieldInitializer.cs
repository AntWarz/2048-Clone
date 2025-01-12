using UnityEngine;

public class FieldInitializer : MonoBehaviour
{
    [SerializeField] private int _squaresPerSide;
    [SerializeField] private int _tileSize;

    public static (bool, Vector2, (int, int), GameObject)[,] TileArray;

    private void Awake()
    {
        TileArray = FieldCalculator.InitializeFieldArrays(_tileSize, _squaresPerSide);
    }
}
