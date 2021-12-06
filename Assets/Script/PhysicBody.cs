using UnityEditor;
using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    [SerializeField] bool isStatic = false;
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float aceleration = 0.0f;
    [SerializeField, Range(0.001f, 50)] float mass = 5.0f;
    [SerializeField] float force = 15.0f;
    [SerializeField] float radius = 5.0f;

    [SerializeField] float coefficientFriction = 0.0f;
    [SerializeField] float tableFriction = 0.2f; // Coeficiente de rozamiento que suele tener una mesa de Pool
    [SerializeField] float gravity = 9.8f;


    float airDensity = 1.225f; //Densidad del aire a una temperatura templada
    float constantAirFriction = 0.000000667f; //Le saco 4 ceros con respecto a su coeficiente original

    [SerializeField] float frictionForceAir = 0.0f;

    public float FrictionTableForce => tableFriction * (mass * gravity); // Calculo la friccion de la mesa: Coeficiente de rozamiento de la mesa * (masa * gravedad)  

    private void Start()
    {
        mass = Mathf.Clamp(mass, 0.001f, float.MaxValue); //Clampeamos la masa para que solo pueda ser positiva

        aceleration = force / mass; //Se calcula la aceleracio (f / m)

        coefficientFriction = FrictionTableForce; // Se le aplica el calculo de la friccion con la mesa al coeficiente de friccion

        frictionForceAir = GetFrictionAirForce(radius); //  Aca se calcula el rozamiento con el aire
    }

    private void Update()
    {
        Movement();
    }

    public void Move(Vector3 amount)
    {
        if(!isStatic) transform.position += amount;
    }
     
    /// <summary>
    /// Calcula la friccion con el aire teniendo en cuenta el radio de la bola.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    float GetFrictionAirForce(float radius) //Formula de rozamiento con el aire: Fr= CD * 1/2 * ρf * Rd²/4
    {
        return constantAirFriction * 0.5f * airDensity * (radius * radius) / 4;
    }

    /// <summary>
    /// Aplica las fuerzas de rozamiento con el aire y el rozamiento con la mesa
    /// </summary>
    void Movement()
    {
        aceleration -= coefficientFriction * Time.deltaTime; //Se le aplica el rozamiento de la mesa a la velocidad, esto para simular la "friccion con la mesa"
        aceleration -= frictionForceAir; //Se le aplica el rozamiento del aire a la velocidad, esto para simular la "friccion con el aire"

        if (aceleration < 0) //Se limita la aceleracion para que no pueda ser negativa
        {
            aceleration = 0;
        }

        velocity = direction * aceleration * Time.deltaTime; //Se le aplica a la veolocidad la direccion y la aceleracion (la cual esta siendo reducida todos los frames por el rozamiento)

        transform.position += velocity; //Se le aplica la velocidad al objeto
    }
    
    public void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0,0,angle));
    }

    public void HitByCue(Vector3 force) 
    {
        if (!isStatic)
        {
            direction.z = 0;

            this.direction = force.normalized;

            aceleration = Mathf.Abs(force.magnitude) / mass;
        }
    }

    public float GetForce()
    {
        return mass * aceleration;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetAcceleration(float value)
    {
        aceleration = value;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(velocity), Mathf.Clamp01(aceleration), EventType.Repaint); // Dibuja una flecha indicando la direccion a la que se esta moviendo cada bola
    }
#endif

    // NOTAS:
    //Constante de resistencia aerodnamica =  0,0000000000667

    //ACELERACION : a = Δv / Δt;
    //ACELERACION : a = F/m;

    //MRUV : d = 1/2.a.t2;

    //FUERZA : m * a;

    //Coeficiente de rozamiente: Fr = μ * N (Rozamiento entre 2 superficies = coeficiente de rozamiento (mesa) * Fuerza Normal)

    //Formula de rozamiento con el aire:          Fdrag = ||v||2 * Cd * -1
    //Fuerza de arrastre = velocidad.normalize² * Coeficiente de friccion * -1 (El signo indica el lado al que se aplica la fuerza)


    //Fr= CD * 1/2 * ρf * Av2
    //Fr = 0,0000000000667 * 1/2 * 1.225 * Rd²/4

    //Rd²/4 = Friccion de una esfera

    //  0,0000000000667 Nm² / kg²
    // 1.225 kg / m 3
}
