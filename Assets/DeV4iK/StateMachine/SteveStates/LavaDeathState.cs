using UnityEngine;

namespace Project.StateMachine.SteveStates
{
    public class LavaDeathState : IState
    {
        private readonly SteveMovement _steveMovement;
        private readonly Animator _animator;

        private readonly int _lavaHash = Animator.StringToHash("In Lava");

        public LavaDeathState(SteveMovement steveMovement, Animator animator)
        {
            _steveMovement = steveMovement;
            _animator = animator;
        }

        public void FixedUpdate()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(_lavaHash, true);
            _steveMovement.enabled = false;
        }

        public void OnExit()
        {
            _animator.SetBool(_lavaHash, false);
            _steveMovement.enabled = true;
        }

        public void Update()
        {
        }
    }
}