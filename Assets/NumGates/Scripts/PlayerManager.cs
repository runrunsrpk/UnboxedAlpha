using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NumGates
{
    public class PlayerManager : MonoBehaviour
    {
        InputMaster input;

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
            Debug.LogWarning($"On Click! {ctx.phase}");
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Click.performed += OnClick;
        }

        private void OnDisable()
        {
            input.Disable();

            input.Player.Click.performed -= OnClick;
        }
    }
}

