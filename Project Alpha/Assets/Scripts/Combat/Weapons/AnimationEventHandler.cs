using System;
using UnityEngine;

namespace Combat.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnAttackAction;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnEnableFlip;
        public event Action OnDisableFlip;
        public event Action OnMinHold;
        public event Action OnEnableOptionalSprite;
        public event Action OnDisableOptionalSprite;
    
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void EnableFlipTrigger() => OnEnableFlip?.Invoke();
        private void DisableFlipTrigger() => OnDisableFlip?.Invoke();
        private void MinHoldTrigger() => OnMinHold?.Invoke();
        private void EnableOptionalSpriteTrigger() => OnEnableOptionalSprite?.Invoke();
        private void DisableOptionalSpriteTrigger() => OnDisableOptionalSprite?.Invoke();
    }
}
