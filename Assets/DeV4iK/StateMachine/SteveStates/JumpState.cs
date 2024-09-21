namespace Project.StateMachine.SteveStates
{
    public class JumpState : IState
    {
        private SteveMovement _steveMovement;

        public JumpState(SteveMovement steveMovement)
        {
            _steveMovement = steveMovement;
        }
        
        
        public void OnEnter()
        {
            _steveMovement.Jump();
        }

        public void OnExit()
        {
            
        }

        public void FixedUpdate()
        {
            _steveMovement.HandleJumping();
            _steveMovement.ApplyVelocity();
        }

        public void Update()
        {
        }
    }
}