using UnityEngine;
using Zenject;

namespace Abstraction
{

    public class PlayerController : MonoBehaviour
    {
        [Inject] public InputController Input { get; private set; }
        [Inject] public LayerController Layer { get; private set; }

        public CharacterController CharCtrl { get; private set; }
        public PlayerState Movement;

        [Header("Movement")]
        public Vector2 Move;
        public Vector2 LastMove;

        public bool IsRunningMode;
        public bool IsPressRunning;
        public bool IsCtrl;
        public bool IsAlt;

        public float CrouchTime = 10f;
        public float SpeedWalking = 2f;
        public float SpeedRunning = 4f;
        public float Speed;
        public bool IsGrounded;
        public bool IsIdling;
        public bool IsWalking;
        public bool IsRunning;
        public bool IsMoving;
        public bool IsStoping;
        public float DistanceToGround;
        public bool IsAirbone;
        public bool IsFalling;

        [Header("Animation")]
        public float AnimationSpeed;
        public Animator Animator;

        string _animSpeed = "speed";
        int _praSpeed;

        private void Awake()
        {
            CharCtrl = GetComponent<CharacterController>();

            Movement = new PlayerState(this);
            Movement.ChangeState(Movement.idling);

            InitAnimation();
        }

        public void Start()
        {
            Input.InputActions.Player.Ctrl.started += ctx => Ctrl();

            Input.InputActions.Player.Sprint.started += ctx => Shift();
            Input.InputActions.Player.Sprint.canceled += ctx => Shift();

            Input.InputActions.Player.Alt.started += ctx => Alt();
            Input.InputActions.Player.Alt.canceled += ctx => Alt();



            //_ = TimingExtention.DownloadFileWithProgressAsync("http://localhost/game/game.zip", "downloadedFile.zip");
            //Debug.Log(Application.persistentDataPath);
        }

        public void Alt()
        {
            IsAlt = !IsAlt;

            //Input.GameCursor(IsAlt);
            //CamaraManager.IsCamInput(!IsAlt);
        }

        public void Shift()
        {
            if (IsRunningMode)
            {
                IsPressRunning = true;
                return;
            }
            IsPressRunning = !IsPressRunning;
        }

        public void Ctrl()
        {
            IsCtrl = !IsCtrl;
            IsRunningMode = !IsRunningMode;

            if (!IsRunningMode)
            {
                IsPressRunning = false;
            }
            else
            {
                IsPressRunning = true;
            }
        }

        public void PressRunning()
        {
            if (IsRunningMode) return;
            IsPressRunning = !IsPressRunning;
        }

        public void InitAnimation()
        {
            Animator = GetComponentInChildren<Animator>();
            _praSpeed = Animator.StringToHash(_animSpeed);
        }

        public void SetAnimationSpeed()
        {
            Animator.SetFloat(_praSpeed, AnimationSpeed);
        }

        public void Update()
        {
            Movement.HandleInput();
            Movement.Update();
        }

        public void FixedUpdate()
        {
            Movement.PhysicsUpdate();
        }

        public void OnTriggerEnter(Collider other)
        {
            Movement.OntriggerEnter(other);
        }

        public void OnTriggerExit(Collider other)
        {
            Movement.OntriggerExit(other);
        }

        public void IsFreezing(bool status)
        {
            if (status)
            {
                Movement.ChangeState(Movement.freezing);
                return;
            }
            Movement.ChangeState(Movement.idling);
        }
    }

    #region Player Movement system

    public interface IState
    {
        public void Enter();
        public void Update();
        public void PhysicsUpdate();
        public void HandleInput();
        public void Exit();

        public void OntriggerEnter(Collider coll);
        public void OntriggerExit(Collider coll);
    }
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void OntriggerEnter(Collider coll) { currentState?.OntriggerEnter(coll); }
        public void OntriggerExit(Collider coll) { currentState?.OntriggerExit(coll); }
    }
    public class PlayerState : StateMachine
    {
        public PlayerController player { get; }
        public PlayerFreezing freezing { get; }
        public PlayerStoping stoping { get; }
        public PlayerIdling idling { get; }
        public PlayerWalking walking { get; }
        public PlayerRunning running { get; }
        public PlayerFall falling { get; }
        public PlayerState(PlayerController player)
        {
            this.player = player;

            freezing = new PlayerFreezing(this);
            idling = new PlayerIdling(this);
            walking = new PlayerWalking(this);
            running = new PlayerRunning(this);
            stoping = new PlayerStoping(this);
            falling = new PlayerFall(this);
        }

    }
    public class PlayerMovementState : IState
    {
        protected PlayerState playerState;

        protected float targetAngle;
        protected float angle;
        protected Vector3 moveDir;

        protected float turnSmoothTime = 0.1f;
        protected float turnSmoothVelocity;

        protected float speed;

        protected float modeAnimation = 0f;

        public PlayerMovementState(PlayerState playerState)
        {
            this.playerState = playerState;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void HandleInput()
        {
            playerState.player.Move = playerState.player.Input.InputActions.Player.Move.ReadValue<Vector2>();

            if (playerState.player.Move != Vector2.zero)
            {
                playerState.player.LastMove = playerState.player.Move;
            }
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Update()
        {
            playerState.player.DistanceToGround = CheckDistanceToGrounded();

            if (playerState.player.DistanceToGround >= -1)
            {
                ApplyGravity();
            }

            playerState.player.SetAnimationSpeed();

            if (playerState.player.Move != Vector2.zero && playerState.player.IsPressRunning)
            {
                playerState.ChangeState(playerState.running);
            }
        }

        protected void Move()
        {
            playerState.player.Speed = Mathf.Lerp(playerState.player.Speed, speed, Time.deltaTime * playerState.player.CrouchTime);
            playerState.player.AnimationSpeed = Mathf.Lerp(playerState.player.AnimationSpeed, modeAnimation, Time.deltaTime * playerState.player.CrouchTime);

            targetAngle = Mathf.Atan2(playerState.player.LastMove.x, playerState.player.LastMove.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(playerState.player.CharCtrl.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            if (playerState.player.Move != Vector2.zero) playerState.player.CharCtrl.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerState.player.CharCtrl.Move(moveDir.normalized * playerState.player.Speed * Time.deltaTime);
        }

        protected float CheckDistanceToGrounded()
        {
            Vector3 bottom = playerState.player.transform.position
            + Vector3.up * playerState.player.CharCtrl.center.y
            - Vector3.up * (playerState.player.CharCtrl.height / 2 - playerState.player.CharCtrl.skinWidth);

            Ray ray = new Ray(bottom, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerState.player.Layer.GroundLayer))
            {
                return hit.distance;
            }

            return -1f;
        }

        protected void ApplyGravity()
        {
            playerState.player.CharCtrl.Move(Physics.gravity * Time.deltaTime * 0.9f);
        }

        //protected float FindTimeFromValue(AnimationCurve curve, float precision = 0.01f)
        //{
        //    float valueAtTime = curve.keys.Last().time;
        //    float val = 0;

        //    for (float i = 0; i < valueAtTime; i += precision)
        //    {
        //        val = curve.Evaluate(i);
        //        if (val < playerState.player.Speed)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        public virtual void OntriggerEnter(Collider coll)
        {

        }

        public virtual void OntriggerExit(Collider coll)
        {

        }
    }
    public class PlayerGrounded : PlayerMovementState
    {
        public PlayerGrounded(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsGrounded = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsGrounded = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class PlayerAirborne : PlayerMovementState
    {
        public PlayerAirborne(PlayerState playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsAirbone = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsAirbone = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class PlayerMoving : PlayerGrounded
    {
        public PlayerMoving(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsMoving = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsMoving = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Update()
        {
            base.Update();
            Move();
        }
    }
    public class PlayerWalking : PlayerMoving
    {
        public PlayerWalking(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsWalking = true;

            speed = playerState.player.SpeedWalking;
            modeAnimation = 1f;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsWalking = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Move != Vector2.zero)
            {
                return;
            }

            if (playerState.player.Move == Vector2.zero)
            {
                playerState.ChangeState(playerState.stoping);
            }
        }
    }
    public class PlayerRunning : PlayerMoving
    {
        public PlayerRunning(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsRunning = true;
            speed = playerState.player.SpeedRunning;

            modeAnimation = 2f;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsRunning = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (!playerState.player.IsPressRunning || playerState.player.Move == Vector2.zero)
            {
                playerState.ChangeState(playerState.stoping);
                return;
            }
        }
    }
    public class PlayerStoping : PlayerMoving
    {
        public PlayerStoping(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            playerState.player.IsStoping = true;
            speed = 0;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsStoping = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Speed < 0.001f)
            {
                playerState.ChangeState(playerState.idling);
            }

            if (playerState.player.Move != Vector2.zero)
            {
                playerState.ChangeState(playerState.walking);
            }
        }
    }
    public class PlayerIdling : PlayerGrounded
    {
        public PlayerIdling(PlayerState playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            //playerState.player.Input.GameCursor(false);
            playerState.player.Input.InputActions.Player.Enable();

            base.Enter();
            playerState.player.IsIdling = true;
            playerState.player.Speed = 0f;

            playerState.player.AnimationSpeed = 0f;
            modeAnimation = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsIdling = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            if (playerState.player.Move == Vector2.zero)
            {
                return;
            }

            playerState.ChangeState(playerState.walking);
        }
    }
    public class PlayerFreezing : PlayerMovementState
    {
        public PlayerFreezing(PlayerState playerState) : base(playerState)
        {

        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.Input.InputActions.Player.Move.Disable();
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.Input.InputActions.Player.Move.Enable();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
            if (playerState.player.Speed > 0.1f)
            {
                Move();
            }
        }
    }
    public class PlayerFall : PlayerAirborne
    {
        public PlayerFall(PlayerState playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerState.player.IsFalling = true;
        }

        public override void Exit()
        {
            base.Exit();
            playerState.player.IsFalling = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
    }

    #endregion Player Movement system
}
