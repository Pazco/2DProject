using System;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jetpack : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    public float Energy
    {
        get { return _energy; }
        set { _energy = Mathf.Clamp(value, 0, _maxEnergy); }
    }

    public bool Flying { get; set; }

    private Rigidbody2D _targetRB;

    // --- VARIABLES DE ANIMACIÓN (NUEVAS) ---
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _energy;
    [SerializeField] public float _maxEnergy;
    [SerializeField] private float _energyFlyingRatio;
    [SerializeField] private float _energyRegenerationRatio;
    [SerializeField] private float _horizontalForce;
    [SerializeField] private float _flyForce;
    private bool _flying = false;
    private int _platformCount = 0;


    public void Awake()
    {
        _targetRB = GetComponent<Rigidbody2D>();

        // --- BUSCAMOS LOS COMPONENTES VISUALES (NUEVO) ---
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Mathf.Abs(_targetRB.velocity.x) > 0.1f)
        {
            _animator.SetBool("isMoving", true);
            if (_targetRB.velocity.x > 0.1f)
                _spriteRenderer.flipX = false;
            else if (_targetRB.velocity.x < -0.1f)
                _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        // 2. Animación Vertical (Volar y Caer)
        if (Input.GetAxisRaw("Vertical") > 0 && Energy > 0) // Si va hacia arriba
        {
            _animator.SetBool("isFlying", true);
            _animator.SetBool("isMoving", false);
            _animator.SetBool("isFalling", false);
        }
        else if (_targetRB.velocity.y < -0.1f) // Si va hacia abajo
        {
            _animator.SetBool("isFlying", false);
            _animator.SetBool("isFalling", true);
        }
        else // Si está quieto en el eje Y (en el suelo)
        {
            _animator.SetBool("isFlying", false);
            _animator.SetBool("isFalling", false);
        }
    }

    public void FlyUp()
    {
        _flying = true;
    }

    public void StopFlying()
    {
        _flying = false;
    }

    public void Regenerate()
    {
        Energy += _energyRegenerationRatio * Time.fixedDeltaTime;
    }

    public void Regenerate(float energy)
    {
        Energy += energy * Time.fixedDeltaTime;
    }

    public void FlyHorizontal(Direction flyDirection)
    {
        if (flyDirection == Direction.Left)
        {
            _targetRB.AddForce(Vector2.left * _horizontalForce);
        }
        else
        {
            _targetRB.AddForce(Vector2.right * _horizontalForce);
        }
    }

    void Start()
    {
        Energy = _maxEnergy;

    }

    void FixedUpdate()
    {
        if (_flying)
        {
            DoFly();
        }
        else if (_platformCount > 0)
        {
            Regenerate();
        }
    }

    private void DoFly()
    {
        if (Energy > 0)
        {
            _targetRB.AddForce(Vector2.up * _flyForce);
            Energy -= _energyFlyingRatio;
        }
        else
        {
            _flying = false;
        }
    }

    public void AddEnergy(float amount)
    {
        Energy += amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _platformCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _platformCount--;
        }
    }
}
