using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NumGates
{
    public class PlayerManager : MonoBehaviour
    {
        private InputMaster input;

        private Vector2 inputPosition;

        private void Awake()
        {
            InitInput();
        }

        private void InitInput()
        {
            input = new InputMaster();
        }

        private void OnClick(InputAction.CallbackContext ctx)
        {
            //Debug.LogWarning($"On Click! {ctx.phase} {inputPosition}");

            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!hit.collider) return;

                if (hit.collider.transform.parent.TryGetComponent(out Collectable collectable))
                {
                    collectable.OnCollected?.Invoke();
                }
            }
        }

        private void OnPoint(InputAction.CallbackContext ctx)
        {
            inputPosition = ctx.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Click.performed += OnClick;
            input.Player.Point.performed += OnPoint;
        }

        private void OnDisable()
        {
            input.Disable();

            input.Player.Click.performed -= OnClick;
            input.Player.Point.performed -= OnPoint;
        }
    }
}

