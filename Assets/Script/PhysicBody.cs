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

    [SerializeField] Vector3 direction;
    [SerializeField] float velocity;
    [SerializeField] float aceleration;
    [SerializeField] float mass = 5.0f;
    [SerializeField] float force = 15.0f;

    [SerializeField] float gravity = -9.8f;

    private void Update()
    {

        aceleration = force / mass;
        aceleration -= gravity * Time.deltaTime;
        velocity += aceleration;

        transform.position += direction * velocity;
    }
    void addForce()
    {

    }

    //ACELERACION : a = Δv / Δt;
    //ACELERACION : a = F/m;

    //MRUV : d = 1/2.a.t2;
     
    //FUERZA : m * a;

    
}

