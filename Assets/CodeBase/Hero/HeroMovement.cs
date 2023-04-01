using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        
        private readonly float limitCameraXUp = 45f;
        private readonly float limitCameraXDown = 55f;
        
        private float currentRotationCamX;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private Quaternion initialCameraRotation;
        
        private IInputService inputService;
        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IInputService inputService, IEventListenerService eventListenerService)
        {
            this.inputService = inputService;
            this.eventListenerService = eventListenerService;
        }

        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            initialCameraRotation = cameraTransform.rotation;
            
            eventListenerService.OnRestartGame += RestartGame;
        }

        private void OnDestroy()
        {
            eventListenerService.OnRestartGame -= RestartGame;
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 movementVector = Vector3.zero;
            Vector2 joystickAxis = inputService.MovementAxis;
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
            var rotateX = inputService.RotationAxis.x * rotationSpeed * Time.deltaTime;
            var rotateY = inputService.RotationAxis.y * rotationSpeed * Time.deltaTime;

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

        private void RestartGame()
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            cameraTransform.rotation = initialCameraRotation;
        }
    }
}