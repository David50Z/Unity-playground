using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DD
{
    public class Input_Handler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool clicked;

        Player_Controls inputActions;
        Camera_Handler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraHandler = Camera_Handler.singleton;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }

        public void OnEnable()
        {
            if(inputActions == null)
            {
                //Debug.Log("Hello!");
                inputActions = new Player_Controls();
                inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                //inputActions.PlayerMovement.Movement.performed += inputActions => Debug.Log(inputActions.ReadValue<Vector2>());

            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}
