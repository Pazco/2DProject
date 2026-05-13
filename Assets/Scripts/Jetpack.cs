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
        get
        {
            return _energy;
        }
        set
        {
            _energy = Mathf.Clamp(value, 0, _maxEngery);
        }
    }

    public bool Flying { get; set; }




    private Rigidbody2D _targetRB;
    [SerializeField] private float _energy;
    [SerializeField] private float _maxEngery;
    [SerializeField] private float _energyFlyingRatio;
    [SerializeField] private float _energyRegenerationRatio;
    [SerializeField] private float _horizontalForce;
    [SerializeField] private float _flyForce;
    private bool _flying = false;
    private bool _onPlatform = false;


    public void Awake()
    {
        _targetRB = GetComponent<Rigidbody2D>();
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
        if (!_flying)
            return;

        if (flyDirection == Direction.Left)
        {
            _targetRB.AddForce(Vector2.left * _horizontalForce);
        }
        else
        {
            _targetRB.AddForce(Vector2.right * _horizontalForce);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Energy = _maxEngery;

    }

    void FixedUpdate()
    {
        if (_flying)
        {
            DoFly();
        }
        // 2. Solo regeneramos si estamos sobre la plataforma y NO estamos volando
        else if (_onPlatform)
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

    
    public void AddEnergy(float NOSE_DAMAGE)
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Importante: Asegúrate de que el suelo/plataformas de tu juego tengan el Tag "Ground"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _onPlatform = false;
        }
    }

}
