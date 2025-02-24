using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Player : MonoBehaviour, IMoveble, IDamageble
{
    [SerializeField] private float _healthPoints;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Mover _mover;
    private Jumper _jumper;
    private CharacterFlipper _flipper;
    private Health _health;

    public event Action Dead;
    public event Action<float> OnHealthPointsChanged;

    public float HealthPoints => _health.HealthPoints;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _mover = new(this);
        _jumper = new(this);
        _flipper = new(this);
        _health = new(_healthPoints);
    }

    private void OnEnable() =>
        _inputReader.OnInputChanged += ProcessMovement;

    private void OnDisable() =>
        _inputReader.OnInputChanged -= ProcessMovement;

    public Rigidbody2D GetRigidbody() =>
        _rigidbody;

    public void TakeDamage(float damage)
    {
        if (_health.IsAlive == false)
            return;

        _health.TakeDamage(damage);

        OnHealthPointsChanged?.Invoke(_health.HealthPoints);

        if(_health.IsAlive == false )
            Dead?.Invoke();
    }

    private void ProcessMovement(InputInformation information)
    {
        if (_health.IsAlive == false)
            return;

        _mover.Move(information.Axis * _speed);
        
        if(information.KeyCode == _jumpButton & _groundChecker.IsGrounded)
            _jumper.Jump(_jumpForce);

        if (_flipper.IsTurnedToRight & information.Axis < 0f | _flipper.IsTurnedToRight == false & information.Axis > 0f)
            _flipper.Flip();
    }
}
