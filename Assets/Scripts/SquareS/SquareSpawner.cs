using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _square;

    private void Start()
    {
        SquareMover.spawnEvent += EventSpawnSquare;
        SpawnSquare(_square);
        SpawnSquare(_square);
    }



    private void SpawnSquare(GameObject square)
    {
        (bool, Vector2, (int, int), GameObject)[,] tileArray;
        tileArray = FieldInitializer.TileArray;
        (Vector2 freeTilePos, (int x, int y))  = SpawnPositioner.GetRandomFreeTile(tileArray);

        GameObject squareSpawned = Instantiate(square, freeTilePos, Quaternion.identity, this.transform);
        tileArray[x, y].Item1 = true;
        tileArray[x, y].Item4 = squareSpawned;
        FieldInitializer.TileArray = tileArray;

    }

    public void EventSpawnSquare()
    {
        SpawnSquare(_square);
    }
}
