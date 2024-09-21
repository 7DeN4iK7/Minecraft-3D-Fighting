namespace Project.StateMachine.SteveStates
{
    public class DeadState : IState
    {
        private SteveMovement _steveMovement;

        public DeadState(SteveMovement steveMovement)
        {
            _steveMovement = steveMovement;
        }

        public void FixedUpdate()
        {
            _steveMovement.UpdateGravity();
            _steveMovement.ApplyVelocity();
        }

        public void OnEnter()
        {
            _steveMovement.enabled = false;
        }

        public void OnExit()
        {
            _steveMovement.enabled = true;
        }

        public void Update()
        {
        }
    }
}