using Unity.Mathematics;
using UnityEngine;

public class TileArrayHolder : MonoBehaviour
{
    [SerializeField] private int _squaresPerSide;
    [SerializeField] private int _tileSize;

    public static (bool, Vector2, (int, int), GameObject)[,] TileArray;
    public static int ArraySize;

    private void Awake()
    {
        TileArray = FieldCalculator.InitializeFieldArray(_tileSize, _squaresPerSide);
        ArraySize = _squaresPerSide;
    }


    public static void UpdateTileArray((int, int) arrayInd, bool occupied, GameObject square)
    {
        TileArray[arrayInd.Item1, arrayInd.Item2].Item1 = occupied;
        TileArray[arrayInd.Item1, arrayInd.Item2].Item4 = square;
    }
}
