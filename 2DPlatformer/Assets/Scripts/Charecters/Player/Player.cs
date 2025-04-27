using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader), typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IMoveble, IDamageble
{
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

    public Rigidbody2D Rigitbody { get; private set; }
    public Health Health { get; private set; }

    public event Action Dead;

    private void Awake()
    {
        Rigitbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();

        _mover = new Mover(this, _playerAnimator);
        _jumper = new Jumper(this, _playerAnimator);
        _flipper = new CharacterFlipper(transform);
        Health = new Health(_playerAnimator);
    }

    private void Start() =>
        Health.SetHealthPoints(_beginHealthPoints);

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

    public void TakeDamage(float damage)
    {
        if (Health.IsAlive == false)
            return;

        Health.TakeDamage(damage);

        if (Health.IsAlive == false)
            Dead?.Invoke();
    }

    public void Heal(float healthPoints) =>
        Health.Heal(healthPoints);

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