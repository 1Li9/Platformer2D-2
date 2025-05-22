using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader), typeof(PlayerAnimator))]
public class Player : Charecter, IMoveble, IDamageble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private CharecterView _view;
    [SerializeField] private Timer _timer;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _attackButton = KeyCode.Mouse0;

    private InputReader _inputReader;
    private PlayerAnimator _playerAnimator;
    private Mover _mover;
    private Jumper _jumper;
    private CharacterFlipper _flipper;

    public Rigidbody2D Rigitbody { get; private set; }
    public override Health Health { get; protected set; }

    public override event Action<float> HealthChanged;
    public override event Action<float> MaxHealthChanged;

    public event Action Dead;

    private void Awake()
    {
        Rigitbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();

        _mover = new Mover(this, _playerAnimator);
        _jumper = new Jumper(this, _playerAnimator);
        _flipper = new CharacterFlipper(_view.transform);
        Health = new Health(_playerAnimator, _beginHealthPoints);
    }

    private void OnEnable()
    {
        _inputReader.OnInputChanged += ProcessMovement;
        _inputReader.OnInputChanged += Attack;

        Health.ValueChanged += (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged += (value) => MaxHealthChanged?.Invoke(value);
    }

    private void OnDisable()
    {
        _inputReader.OnInputChanged -= ProcessMovement;
        _inputReader.OnInputChanged -= Attack;

        Health.ValueChanged -= (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged -= (value) => MaxHealthChanged?.Invoke(value);
    }

    private void Start() =>
        Health.Initialize();

    public void TakeDamage(float damage)
    {
        if (Health.IsAlive == false)
            return;

        Health.TakeDamage(damage);

        if (Health.IsAlive == false)
            Dead?.Invoke();
    }

    public void Heal(float healthPoints) =>
        Health.Increase(healthPoints);

    private void ProcessMovement(InputInformation information)
    {
        if (Health.IsAlive == false)
            return;

        _playerAnimator.SetIsGrounded(_groundChecker.IsGrounded);

        _mover.Move(information.Axis * _speed);

        if (information.KeyCode == _jumpButton & _groundChecker.IsGrounded)
            _jumper.Jump(_jumpForce);

        if (_flipper.IsTurnedToRight & information.Axis < 0f | _flipper.IsTurnedToRight == false & information.Axis > 0f)
            _flipper.Flip();
    }

    private void Attack(InputInformation information)
    {
        if (Health.IsAlive == false)
            return;

        if (information.KeyCode == _attackButton)
            _attacker.Attack(_timer, _attackCooldownTime, _playerAnimator);
    }
}