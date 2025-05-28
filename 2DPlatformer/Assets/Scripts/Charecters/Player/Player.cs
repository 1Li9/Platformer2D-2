using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader), typeof(PlayerAnimator))]
public class Player : Charecter, IMoveble, IDamageble, IHealeble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private float _vampirismAttackTime;
    [SerializeField] private float _vampirismAttackReloadTime;
    [SerializeField] private float _vampirismHealPoints;
    [SerializeField] private View _view;
    [SerializeField] private Timer _timer;
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private HandAttackZone _attackTrigger;
    [SerializeField] private ClosestAttackZone _vampirismAttackTrigger;
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _usualAttackButton = KeyCode.Mouse0;
    [SerializeField] private KeyCode _vampirismAttackButton = KeyCode.Mouse1;

    private InputReader _inputReader;
    private PlayerAnimator _playerAnimator;
    private Mover _mover;
    private Jumper _jumper;
    private CharacterFlipper _flipper;

    private VampirismAttacker _vampirismAttacker;
    private HandAttacker _usualAttacker;

    public override Health Health { get; protected set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public float VampirismAttackReloadTime => _vampirismAttackReloadTime;
    public float VampirismAttackTime => _vampirismAttackTime;

    public override event Action<float> HealthChanged;
    public override event Action<float> MaxHealthChanged;

    public event Action<float> VampirismIsReloading;
    public event Action VampirismAttacked;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();

        _mover = new Mover(this, _playerAnimator);
        _jumper = new Jumper(this);
        _flipper = new CharacterFlipper(_view.transform);
        Health = new Health(_playerAnimator, _beginHealthPoints);

        Attacker attacker = new();
        _usualAttacker = new HandAttacker(attacker, _attackCooldownTime, _timer);
        _vampirismAttacker = new VampirismAttacker(attacker, this, _vampirismAttackTime, _vampirismAttackReloadTime,
            _vampirismHealPoints, _timer);
    }

    private void OnEnable()
    {
        _inputReader.OnInputChanged += ProcessMovement;
        _inputReader.OnInputChanged += UsualAttack;
        _inputReader.OnInputChanged += VampirismAttack;

        Health.ValueChanged += (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged += (value) => MaxHealthChanged?.Invoke(value);

        _vampirismAttacker.Reloading += (value) => VampirismIsReloading?.Invoke(value);
        _vampirismAttacker.Attacked += () => VampirismAttacked?.Invoke();
    }

    private void OnDisable()
    {
        _inputReader.OnInputChanged -= ProcessMovement;
        _inputReader.OnInputChanged -= UsualAttack;
        _inputReader.OnInputChanged -= VampirismAttack;

        Health.ValueChanged -= (value) => HealthChanged?.Invoke(value);
        Health.MaxValueChanged -= (value) => MaxHealthChanged?.Invoke(value);

        _vampirismAttacker.Reloading -= (value) => VampirismIsReloading?.Invoke(value);
        _vampirismAttacker.Attacked -= () => VampirismAttacked?.Invoke();
    }

    private void Start() =>
        Health.Initialize();

    public void IncreaseHealth(float healthPoints) =>
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

        _playerAnimator.SetVerticalSpeed(Rigidbody.velocity.y);
    }

    private void UsualAttack(InputInformation information) =>
        Attack(information, _usualAttackButton, _usualAttacker, _attackTrigger);

    private void VampirismAttack(InputInformation information) =>
        Attack(information, _vampirismAttackButton, _vampirismAttacker, _vampirismAttackTrigger);

    private void Attack(InputInformation information, KeyCode keyCode, IAttacker attacker, IAttackZone attackZone)
    {
        if (Health.IsAlive == false)
            return;

        if (information.KeyCode == keyCode)
        {
            IDamageble damageble = attackZone.Damageble;
            attacker.Attack(_damage, _playerAnimator, damageble);
        }
    }
}