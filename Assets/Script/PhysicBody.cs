using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    public enum ShapeType
    {
        Circle, Box
    }

    //[SerializeField] ShapeType shapeType = ShapeType.Circle;
    //[SerializeField] private Vector3 linearVelocity = Vector3.zero;
    //[SerializeField] float rotation = 0;
    //[SerializeField] float rotationalVelocity = 0;

    //[SerializeField] float density = 0;
    //[SerializeField] float restitution = 0;
    //[SerializeField] float area;
    //  [SerializeField] bool isStatic = false;

    //float radius = 0;
    //float width = 0;    
    //float height = 0;

    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float aceleration = 0.0f;
    [SerializeField] float mass = 5.0f;
    [SerializeField] float force = 15.0f;


    [SerializeField] float coefficientFriction = 0.0f; // 0.18 - 0.24
    [SerializeField] float tableFriction = 0.2f; // 0.18 - 0.24

    [SerializeField] float gravity = 9.8f;

    private void Start()
    {
        aceleration = force / mass;
        coefficientFriction = tableFriction * (mass * gravity);
    }
    private void Update()
    {
        aceleration -= coefficientFriction * Time.deltaTime;

        if (aceleration < 0)
        {
            aceleration = 0;
        }

        velocity = direction * aceleration * Time.deltaTime;

        transform.position += velocity;

    }
    void addForce()
    {

    }

    //ACELERACION : a = Δv / Δt;
    //ACELERACION : a = F/m;

    //MRUV : d = 1/2.a.t2;

    //FUERZA : m * a;

    //Coeficiente de rozamiente: Fr = μ * N (Rozamiento entre 2 superficies = coeficiente de rozamiento (mesa) * Fuerza Normal)

}

