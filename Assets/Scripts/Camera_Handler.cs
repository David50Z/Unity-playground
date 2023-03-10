using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD
{
    public class Camera_Handler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        [SerializeField]
        private LayerMask ignoreLayers;

        public static Camera_Handler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.1f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;

        public float minimumPivot = -35;
        public float maximumPivot = 35;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {
            /*Debug.Log(myTransform.position);
            Debug.Log(targetTransform.position);
            Debug.Log(delta);
            Debug.Log(followSpeed);
            Debug.Log("END");*/
            Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, 0.000f /*delta / followSpeed*/);
            //Debug.Log(targetPosition);
            myTransform.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;

        }
    }
}
