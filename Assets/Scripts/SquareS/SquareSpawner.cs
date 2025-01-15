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
        tileArray = TileArrayHolder.TileArray;
        (Vector2 freeTilePos, (int x, int y))  = SpawnPositioner.GetRandomFreeTile(tileArray);

        GameObject squareSpawned = Instantiate(square, freeTilePos, Quaternion.identity, this.transform);
        tileArray[x, y].Item1 = true;
        tileArray[x, y].Item4 = squareSpawned;
        squareSpawned.GetComponent<Square>().ArrayIndex = (x, y);
        squareSpawned.GetComponent<Square>().Value = 2;
        TileArrayHolder.TileArray = tileArray;

    }

    public void EventSpawnSquare()
    {
        if (!GameManager.gameOver) SpawnSquare(_square);
    }
}
