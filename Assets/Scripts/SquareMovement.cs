using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquareMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public Vector2 squarePos = new Vector2();

    private InputAction _moveAction;

    private bool _moving = false;
    private Vector2 _moveAxis = new Vector2();

    private PlayingFieldInitializer _fieldInitializer;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _fieldInitializer = FindFirstObjectByType<PlayingFieldInitializer>();
        //Debug.Log($"target tile at:{GetTargetPosition(squarePos, 1, 0)}");
    }

    private void Update()
    {
        MoveSquare();
    }

    private void MoveSquare()
    {
        Vector2 targetTileV2 = new Vector2();
        Vector3 targetTileV3 = new Vector3();
        if (_moveAction.WasPerformedThisFrame())
        {
            _moving = true;
            _moveAxis = _moveAction.ReadValue<Vector2>();
            int dirX = 0;
            int dirY = 0;
            if(_moveAxis.x > 0)
            {
                dirX = Mathf.RoundToInt(_moveAxis.x);
            }
            if (_moveAxis.y > 0)
            {
                dirY = Mathf.RoundToInt(_moveAxis.y);
            }
            targetTileV2 = GetTargetPosition(squarePos, dirX, dirY);
            targetTileV3 = new Vector3(targetTileV2.x, targetTileV2.y, 0f);
            Debug.Log($"Current Pos: {transform.position} Target Tile at: {targetTileV3}");
            this.transform.position = targetTileV3;
        }
    }

    private Vector2 GetTargetPosition(Vector2 squarePos, int dirX = 0, int dirY = 0)
    {
        Vector2 nextTile = new Vector2(squarePos[0] + dirX * 3, squarePos[1] + dirY * 3);
        if (_fieldInitializer.fieldDict.Keys.Contains(nextTile) && !_fieldInitializer.fieldDict[nextTile])
        {
/*            Debug.Log($"Current Tile: {squarePos}");
            Debug.Log($"Move Direction: {dirX} {dirY}");
            Debug.Log($"Next tile: {nextTile}");
            Debug.Log($"Next tile is occupied: {_fieldInitializer.fieldDict[nextTile]}");*/
            return GetTargetPosition(nextTile, dirX, dirY);
        }
        return squarePos;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _moving = false;
    }
}
