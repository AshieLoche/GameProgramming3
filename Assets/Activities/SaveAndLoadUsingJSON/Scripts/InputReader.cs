using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewInputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions
{

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.GamePlay.SetCallbacks(this);
            _gameInput.GamePlay.Enable();
        }
    }

    public event Action<Vector2> MoveEvent;
    public event Action FireEvent;
    public event Action FireCanceledEvent;
    public event Action SaveEvent;
    public event Action LoadEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FireEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            FireCanceledEvent?.Invoke();
        }
    }

    public void OnSave(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SaveEvent?.Invoke();
        }
    }

    public void OnLoad(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            LoadEvent?.Invoke();
        }
    }
}
