﻿using UnityEngine;
using static System.Math;

public class PhysicBody : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float aceleration = 0.0f;
    [SerializeField] float mass = 5.0f;
    [SerializeField] float force = 15.0f;
    [SerializeField] float radius = 5.0f;

    [SerializeField] float coefficientFriction = 0.0f;
    [SerializeField] float tableFriction = 0.2f;
    [SerializeField] float gravity = 9.8f;


    float airDensity = 1.225f;
    float constantAirFriction = 0.000000667f; //Le saco 4 ceros con respecto a su densidad original

    [SerializeField] float frictionForceAir = 0.0f;

    public float FrictionTableForce=> tableFriction * (mass * gravity);

    private void Start()
    {
        aceleration = force / mass;

        coefficientFriction = FrictionTableForce;

        frictionForceAir = getFrictionAirForce(radius);
    }

    private void Update()
    {
        Movement();
    }
     
    /// <summary>
    /// Calcula la friccion con el aire teniendo en cuenta el radio de la bola.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    float getFrictionAirForce(float radius)
    {
        return constantAirFriction * 0.5f * airDensity * (radius * radius) / 4;
    }

    /// <summary>
    /// Aplica las fuerzas de rozamiento con el aire y el rozamiento con la mesa
    /// </summary>
    void Movement()
    {
        aceleration -= coefficientFriction * Time.deltaTime;
        aceleration -= frictionForceAir;

        if (aceleration < 0)
        {
            aceleration = 0;
        }

        velocity = direction * aceleration * Time.deltaTime;

        transform.position += velocity;
    }


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

