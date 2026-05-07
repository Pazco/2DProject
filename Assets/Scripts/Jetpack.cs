using System;
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
        Energy += _energyRegenerationRatio;
    }
    public void Regenerate(float energy)
    {
        Energy += energy;
    }
    public void FlyHorizontal(Direction flyDirection)
    {
        if(!_flying)
        return;

        if (flyDirection == Direction.Left)
        {
            _targetRB.AddForce(Vector2.left * _horizontalForce);
        }
        else
        {
            _targetRB.AddForce(Vector2. right * _horizontalForce);
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

            DoFly();

        //Le quitamos el signo a la velocidad si es negativa
        //Luego si es menor de 0,1 consideramos que estamos parados y cargamos

        if (Mathf.Abs(_targetRB.velocity.y) > 0.1f)
        {
            Regenerate();
        }
    }

    private void DoFly()
    {
        if(Energy > 0)
        {
        _targetRB.AddForce(Vector2.up * _flyForce);
            Energy -= _energyFlyingRatio;
        }
        else
            _flying = false;
    }

    internal void AddEnergy(float NOSE_DAMAGE)
    {
        throw new NotImplementedException();

    }

}
