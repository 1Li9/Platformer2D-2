using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Player : MonoBehaviour, IMoveble, IDamageble
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackCooldownTime;
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
    private Health _health;

    public Rigidbody2D Rigitbody { get; private set; }

    public event Action Dead;
    public event Action<float> OnHealthPointsChanged;

    private void Awake()
    {
        Rigitbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = new PlayerAnimator(this, _animator, _groundChecker, _attacker);
        _mover = new Mover(this);
        _jumper = new Jumper(this);
        _flipper = new CharacterFlipper(this);
        _health = new Health(_beginHealthPoints);
    }

    private void Start() =>
        OnHealthPointsChanged?.Invoke(_health.HealthPoints);

    private void OnEnable()
    {
        _inputReader.OnInputChanged += ProcessMovement;
        _inputReader.OnInputChanged += Attack;
    }

    private void OnDisable()
    {
        _inputReader.OnInputChanged -= ProcessMovement;
        _inputReader.OnInputChanged -= Attack;
    }

    private void Update() =>
        _playerAnimator.Update();

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        OnHealthPointsChanged?.Invoke(_health.HealthPoints);

        if (_health.IsAlive == false)
            Dead?.Invoke();
    }

    public void Heal(float healthPoints)
    {
        _health.Heal(healthPoints);
        OnHealthPointsChanged?.Invoke(_health.HealthPoints);
    }

    private void ProcessMovement(InputInformation information)
    {
        if (_health.IsAlive == false)
            return;

        _mover.Move(information.Axis * _speed);

        if (information.KeyCode == _jumpButton & _groundChecker.IsGrounded)
            _jumper.Jump(_jumpForce);

        if (_flipper.IsTurnedToRight & information.Axis < 0f | _flipper.IsTurnedToRight == false & information.Axis > 0f)
            _flipper.Flip();
    }

    private void Attack(InputInformation information)
    {
        if (_health.IsAlive == false)
            return;

        if (information.KeyCode == _attackButton)
            _attacker.Attack(_timer, _attackCooldownTime);
    }
}