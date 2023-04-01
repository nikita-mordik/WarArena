using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 MovementAxis => MobileMovementAxis();
        public override Vector2 RotationAxis => MobileRotationAxis();
    }
}