using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 MovementAxis { get; }
        Vector2 RotationAxis { get; }
        bool IsAttackButtonUp();
        bool IsUltButtonUp();
    }
}