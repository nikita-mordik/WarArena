using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Vertical = "Vertical";
        protected const string Horizontal = "Horizontal";
        private const string VerticalRotation = "Pitch";
        private const string HorizontalRotation = "Roll";
        private const string FireButton = "Fire";
        private const string UltButton = "Ult";
        
        public abstract Vector2 MovementAxis { get; }
        public abstract Vector2 RotationAxis { get; }

        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(FireButton);
        public bool IsUltButtonUp() => SimpleInput.GetButtonUp(UltButton);

        protected static Vector2 MobileMovementAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        protected static Vector2 MobileRotationAxis() => 
            new Vector2(SimpleInput.GetAxis(HorizontalRotation), SimpleInput.GetAxis(VerticalRotation));
    }
}