using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD
{
    public class Player_Movement : MonoBehaviour
    {
        [SerializeField]
        Transform cameraFollowTarget;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;

        [SerializeField]
        float rotationSpeed = 10;

        Transform cameraObject;
        public Transform target;
        Input_Handler inputHandler;
        Timer_Handler timerFunc;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public Animator_Handler animatorHandler;

        public new Rigidbody rigidBody;
        public GameObject normalCamera;
        int test = 1;

        public Vector3 inputDir = new Vector3(0,0,0);

        public bool attack = false;
        public bool trigger = false;
        bool checkActive = false;

        bool clickedDuringAnim = false;



        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<Input_Handler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler = GetComponentInChildren<Animator_Handler>();
            animatorHandler.Initialize();
        }

        public void Update()
        {
            /*float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            //Debug.Log(projectVelocity);
            rigidBody.velocity = projectVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if(animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }*/

            if (Input.GetMouseButtonDown(0) && attack == true)
            {
                clickedDuringAnim = true;
            }

            if (Input.anyKey && !attack)
            {
                inputDir = new Vector3(0, 0, 0);
                if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
                if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
                if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
                if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;
            } else
            {
                inputDir = new Vector3(0, 0, 0);
            }

            if(Input.GetMouseButtonDown(0) && attack == false)
            {
                attack = true;
                trigger = true;
                //animatorHandler.startAttack();
                //Invoke("disableAttack", 1);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }



        }

        private void FixedUpdate()
        {
            if (checkActive && animatorHandler.isActive == false)
            {
                attack = false;
                checkActive = false;
            }

            Vector3 moveDir = cameraObject.forward * inputDir.z + cameraObject.right * inputDir.x;

            float moveSpeed = 10f;
            moveDir *= moveSpeed;

            moveDir.y = rigidBody.velocity.y;

            rigidBody.velocity = moveDir;
            target.position = rigidBody.position;

            float delta = Time.fixedDeltaTime;

            inputHandler.TickInput(delta);

            //animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if(!attack)
            {
                animatorHandler.updateMoveAnimations(inputDir.z, inputDir.x);
            } else if(trigger)
            {
                animatorHandler.startAttack();
                Invoke("disableAttack", 1);
                trigger = false;
                checkActive = true;
            } else if(clickedDuringAnim && animatorHandler.currentAttack < 4)
            {
                animatorHandler.currentAttack += 1;
                clickedDuringAnim = false;
            }

            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }

            target.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.z * -1.0f);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        #endregion

        private void LateUpdate()
        {
            CameraRotation();
        }

        void CameraRotation()
        {
            //cameraFollowTarget.rotation = Quaternion.identity;
        }


        #region helperFuncs
        void disableAttack()
        {
            if(animatorHandler.currentAttack == 0)
            {
                attack = false;
            }
        }
        #endregion
    }
}