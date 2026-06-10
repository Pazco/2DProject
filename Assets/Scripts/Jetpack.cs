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
        set
        {
            _energy = Mathf.Clamp(value, 0, _maxEnergy);

            if (_energy >= _maxEnergy && !_energyFullEffectPlayed)
            {
                if (_particulasRecarga != null)
                    Instantiate(_particulasRecarga, transform.position, Quaternion.identity);
                _energyFullEffectPlayed = true;
            }
            else if (_energy < _maxEnergy)
            {
                _energyFullEffectPlayed = false;
            }
        }
    }

    public bool Flying { get; set; }
    public bool IsMoving { get; set; }

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
    [SerializeField] private AudioClip _flyClip;
    [SerializeField] private GameObject _particulasRecarga;
    private bool _flying = false;
    private bool _energyFullEffectPlayed = false;
    private AudioSource _audioSource;


    public void Awake()
    {
        _targetRB = GetComponent<Rigidbody2D>();

        // --- BUSCAMOS LOS COMPONENTES VISUALES (NUEVO) ---
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (_targetRB.velocity.x < -0.1f)
            _spriteRenderer.flipX = true;
        else if (_targetRB.velocity.x > 0.1f)
            _spriteRenderer.flipX = false;

        if (Mathf.Abs(_targetRB.velocity.x) > 0.1f)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        // 2. Animación Vertical (Volar y Caer)
        if (_flying) // Solo si el jetpack tiene energía y está impulsando
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
        if (Energy <= 0) return;

        if (!_flying && _flyClip != null && _audioSource != null)
        {
            _audioSource.clip = _flyClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        _flying = true;
    }

    public void StopFlying()
    {
        _flying = false;
        if (_audioSource != null)
            _audioSource.Stop();
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
        else if (IsGrounded() && !IsMoving)
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
            if (_audioSource != null)
                _audioSource.Stop();
        }
    }

    public void AddEnergy(float amount)
    {
        Energy += amount;
    }

    private bool IsGrounded()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float rayLength = 0.15f;
        float[] xOffsets = { -0.2f, 0f, 0.2f };

        foreach (float xOffset in xOffsets)
        {
            Vector2 startPos = new Vector2(bounds.center.x + xOffset, bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, rayLength, LayerMask.GetMask("Default"));
            if (hit.collider != null && (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Platform")))
                return true;
        }
        return false;
    }
}
