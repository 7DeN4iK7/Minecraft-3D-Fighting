namespace Project.StateMachine.SteveStates
{
    public class NeutralState : IState
    {
        private SteveMovement _steveMovement;

        public NeutralState(SteveMovement steveMovement)
        {
            _steveMovement = steveMovement;
        }
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void FixedUpdate()
        {
            _steveMovement.UpdateGravity();
            _steveMovement.ApplyVelocity();
        }

        public void Update()
        {
        }
    }
}