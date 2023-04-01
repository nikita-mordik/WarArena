using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 MovementAxis
        {
            get
            {
                Vector2 axis = MobileMovementAxis();
                if (axis == Vector2.zero)
                    axis = StandaloneMovementAxis();

                return axis;
            }
        }

        public override Vector2 RotationAxis
        {
            get
            {
                Vector2 axis = MobileRotationAxis();
                if (axis == Vector2.zero)
                    axis = StandaloneRotationAxis();

                return axis;
            }
        }

        private static Vector2 StandaloneMovementAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        private static Vector2 StandaloneRotationAxis() =>
            new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
    }
}