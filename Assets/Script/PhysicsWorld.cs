using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] private List<PhysicCollider> physicColliders;
    [SerializeField] private List<PhysicBody> physicBodies;

    public UnityEvent<GameObject> onEnterTheHole;

    private void Start()
    {

        physicColliders = new List<PhysicCollider>(FindObjectsOfType<PhysicCollider>());

        physicBodies = new List<PhysicBody>(FindObjectsOfType<PhysicBody>());
    }

    private void Update()
    {
        if(physicColliders.Count > 0)
        {
            for (int i = 0; i < physicColliders.Count - 1; i++)
            {
                PhysicCollider colliderA = physicColliders[i];
                PhysicBody bodyA = physicBodies[i];

                for (int j = i + 1; j < physicColliders.Count; j++)
                {
                    PhysicCollider colliderB = physicColliders[j];
                    PhysicBody bodyB = physicBodies[j];

                    if(colliderA is CircleCollider && colliderB is RectCollider)
                    {
                        RectCollider rect = (RectCollider)colliderB;
                        if (Collisions.IntersectCirclePolygon((CircleCollider)colliderA, rect.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia A
                            Vector3 directionForceA = bodyA.GetDirection() * bodyA.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote A en el angulo correcto si fuera necesario
                            Vector3 resultA = Vector3.Reflect(directionForceA, -normal);

                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia B
                            Vector3 directionForceB = bodyB.GetDirection() * bodyB.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote B en el angulo correcto si fuera necesario
                            Vector3 resultB = Vector3.Reflect(directionForceB, normal);

                            // Aplico la correccion de posicion en A
                            bodyA.Move(-normal * depth);

                            // Aplico la fuerza para A
                            bodyA.HitByCue(resultA);

                            // Aplico la correccion de posicion en B
                            bodyB.Move(normal * depth);

                            // Aplico la fuerza para B
                            bodyB.HitByCue(resultB);
                        }
                    }
                    else if (colliderA is RectCollider && colliderB is CircleCollider)
                    {
                        RectCollider rect = (RectCollider)colliderA;
                        if (Collisions.IntersectCirclePolygon((CircleCollider)colliderB, rect.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia A
                            Vector3 directionForceA = bodyA.GetDirection() * bodyA.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote A en el angulo correcto si fuera necesario
                            Vector3 resultA = Vector3.Reflect(directionForceA, normal);

                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia B
                            Vector3 directionForceB = bodyB.GetDirection() * bodyB.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote B en el angulo correcto si fuera necesario
                            Vector3 resultB = Vector3.Reflect(directionForceB, -normal);

                            // Aplico la correccion de posicion en A
                            bodyA.Move(normal * depth);

                            // Aplico la fuerza para A
                            bodyA.HitByCue(resultA);

                            // Aplico la correccion de posicion en B
                            bodyB.Move(-normal * depth);

                            // Aplico la fuerza para B
                            bodyB.HitByCue(resultB);
                        }
                    }
                    else if (colliderA is CircleCollider && colliderB is HoleCollider)
                    {
                        if(Collisions.CircleContainPoint((HoleCollider)colliderB, ((CircleCollider)colliderA).Center))
                        {
                            onEnterTheHole?.Invoke(colliderA.gameObject);
                        }
                    }
                    else if (colliderA is HoleCollider && colliderB is CircleCollider)
                    {
                        if (Collisions.CircleContainPoint((HoleCollider)colliderA, ((CircleCollider)colliderB).Center))
                        {
                            onEnterTheHole?.Invoke(colliderB.gameObject);
                        }
                    }
                    else if (colliderA is CircleCollider && colliderB is CircleCollider)
                    {
                        if (Collisions.IntersectCircles((CircleCollider)colliderA, (CircleCollider)colliderB, out Vector3 normal, out float depth))
                        {
                            // Se calcula la fuerza que se devuelve en el impacto para A
                            Vector3 normalForceA = -normal * Mathf.Abs(bodyA.GetForce() - bodyB.GetForce()) / 2;
                            
                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia A
                            Vector3 directionForceA = bodyA.GetDirection() * bodyA.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote A en el angulo correcto si fuera necesario
                            Vector3 resultA = (normalForceA + directionForceA);

                            // // Se calcula la fuerza que se devuelve en el impacto para B
                            Vector3 normalForceB = normal * Mathf.Abs(bodyB.GetForce() - bodyA.GetForce()) / 2;

                            // Se calcula la fuerza con la direccion en la que la esfera se dirigia B
                            Vector3 directionForceB = bodyB.GetDirection() * bodyB.GetForce() / 2;

                            // Se saca el promedio de la fuerzas para que rebote B en el angulo correcto si fuera necesario
                            Vector3 resultB = (normalForceB + directionForceB);

                            // Aplico la correccion de posicion en A
                            bodyA.Move(-normal * depth / 2);

                            // Aplico la fuerza para A
                            bodyA.HitByCue(resultA);

                            // Aplico la correccion de posicion en B
                            bodyB.Move(normal * depth / 2);

                            // Aplico la fuerza para B
                            bodyB.HitByCue(resultB);
                        }
                    }
                    else if (colliderA is RectCollider && colliderB is RectCollider)
                    {
                        RectCollider rectA = (RectCollider)colliderA;
                        RectCollider rectB = (RectCollider)colliderB;

                        if (Collisions.IntersectPolygons(rectA.TransformedVertices, rectB.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            
                            // Aplico la correccion de posicion en A
                            bodyA.Move(-normal * depth / 2);

                            // Aplico la correccion de posicion en B
                            bodyB.Move(normal * depth / 2);

                        }
                    }
                }
            }
        }
    }

    public void RemoveFromWorld(PhysicCollider physicCollider, PhysicBody physicBody)
    {
        physicBodies.Remove(physicBody);
        physicColliders.Remove(physicCollider);
    }
}
