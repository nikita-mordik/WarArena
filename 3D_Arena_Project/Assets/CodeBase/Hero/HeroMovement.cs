using SimpleInputNamespace;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;


        [Header("Test")] 
        [SerializeField] private Joystick movementJoystick;
        [SerializeField] private Joystick rotationJoystick;

        private readonly float limitCameraXUp = 45f;
        private readonly float limitCameraXDown = 55f;
        
        private float currentRotationCamX;

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 movementVector = Vector3.zero;
            var moveX = movementJoystick.xAxis.value;
            var moveY = movementJoystick.yAxis.value;
            var joystickAxis = new Vector2(moveX, moveY);
            if (joystickAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = transform.right * joystickAxis.x + transform.forward * joystickAxis.y;
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;
            characterController.Move(movementVector * movementSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            var rotateX = rotationJoystick.xAxis.value * rotationSpeed * Time.deltaTime;
            var rotateY = rotationJoystick.yAxis.value * rotationSpeed * Time.deltaTime;
            
            if (rotateX != 0)
            {
                transform.RotateAround(transform.position, Vector3.up, rotateX);
            }

            if (rotateY != 0)
            {
                currentRotationCamX -= rotateY;
                currentRotationCamX = Mathf.Clamp(currentRotationCamX, -limitCameraXUp, limitCameraXDown);
                cameraTransform.localEulerAngles = new Vector3(currentRotationCamX, cameraTransform.localEulerAngles.y, 0);
            }
        }
    }
}