using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Player : MonoBehaviour, IMoveble, IDamageble
{
    [SerializeField] private float _beginHealthPoints;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _attackButton = KeyCode.Mouse0;

    private InputReader _inputReader;
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
        _mover = new Mover(this);
        _jumper = new Jumper(this);
        _flipper = new CharacterFlipper(this);
        _health = new Health(_beginHealthPoints);
        OnHealthPointsChanged?.Invoke(_health.HealthPoints);
    }

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

    public Rigidbody2D GetRigidbody() =>
        Rigitbody;

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        OnHealthPointsChanged?.Invoke(_health.HealthPoints);

        if (_health.IsAlive == false)
            Dead?.Invoke();
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
            _attacker.Attack();
    }
}