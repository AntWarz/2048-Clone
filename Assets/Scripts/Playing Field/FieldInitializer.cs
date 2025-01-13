using Unity.Mathematics;
using UnityEngine;

public class FieldInitializer : MonoBehaviour
{
    [SerializeField] private int _squaresPerSide;
    [SerializeField] private int _tileSize;

    public static (bool, Vector2, (int, int), GameObject)[,] TileArray;

    private void Awake()
    {
        TileArray = FieldCalculator.InitializeFieldArray(_tileSize, _squaresPerSide);
    }


    public static void UpdateTileArray((int, int) arrayInd, bool occupied, GameObject square)
    {
        TileArray[arrayInd.Item1, arrayInd.Item2].Item1 = occupied;
        TileArray[arrayInd.Item1, arrayInd.Item2].Item4 = square;
    }
}
