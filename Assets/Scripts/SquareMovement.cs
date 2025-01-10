using UnityEngine;
using UnityEngine.InputSystem;

public class SquareMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private InputAction _moveAction;

    private bool _moving = false;
    Vector2 moveAxis = new Vector2();

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        MoveSquare();
    }

    private void MoveSquare()
    {

        if (_moveAction.WasPressedThisFrame())
        {
            _moving = true;
            moveAxis = _moveAction.ReadValue<Vector2>();
        }



        if (_moving )
        {
            this.transform.Translate(moveAxis * _moveSpeed * Time.deltaTime);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _moving = false;
    }
}
