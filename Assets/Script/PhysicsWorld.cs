using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] private PhysicCollider[] physicColliders;
    [SerializeField] private PhysicBody[] physicBodies;

    private void Start()
    {
        physicColliders = FindObjectsOfType<PhysicCollider>();

        physicBodies = new PhysicBody[physicColliders.Length];

        for (int i = 0; i < physicColliders.Length; i++)
        {
            physicBodies[i] = physicColliders[i].GetComponent<PhysicBody>();
        }
    }

    private void Update()
    {
        if(physicColliders.Length > 0)
        {
            for (int i = 0; i < physicColliders.Length - 1; i++)
            {
                PhysicCollider colliderA = physicColliders[i];
                PhysicBody bodyA = physicBodies[i];

                for (int j = i + 1; j < physicColliders.Length; j++)
                {
                    PhysicCollider colliderB = physicColliders[j];
                    PhysicBody bodyB = physicBodies[j];

                    if(colliderA is CircleCollider && colliderB is RectCollider)
                    {
                        RectCollider rect = (RectCollider)colliderB;
                        if (Collisions.IntersectCirclePolygon((CircleCollider)colliderA, rect.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            //bodyA.Move(-normal * depth / 2);
                            bodyA.HitByCue(-normal, depth);
                            //bodyB.Move(normal * depth / 2);
                            bodyB.HitByCue(normal, depth);
                        }
                    }
                    else if (colliderA is RectCollider && colliderB is CircleCollider)
                    {
                        RectCollider rect = (RectCollider)colliderA;
                        if (Collisions.IntersectCirclePolygon((CircleCollider)colliderB, rect.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            //bodyA.Move(normal * depth / 2);
                            bodyA.HitByCue(normal, depth);
                            //bodyB.Move(-normal * depth / 2);
                            bodyB.HitByCue(-normal, depth);
                        }
                    }
                    else if (colliderA is CircleCollider && colliderB is CircleCollider)
                    {
                        if (Collisions.IntersectCircles((CircleCollider)colliderA, (CircleCollider)colliderB, out Vector3 normal, out float depth))
                        {
                            //bodyA.Move(-normal * depth / 2);
                            bodyA.HitByCue(-normal, depth);
                            //bodyB.Move(normal * depth / 2);
                            bodyB.HitByCue(normal, depth);
                        }
                    }
                    else if (colliderA is RectCollider && colliderB is RectCollider)
                    {
                        RectCollider rectA = (RectCollider)colliderA;
                        RectCollider rectB = (RectCollider)colliderB;

                        if (Collisions.IntersectPolygons(rectA.TransformedVertices, rectB.TransformedVertices, out Vector3 normal, out float depth))
                        {
                            //bodyA.Move(-normal * depth / 2);
                            bodyA.HitByCue(-normal, depth);
                            //bodyB.Move(normal * depth / 2);
                            bodyB.HitByCue(normal, depth);
                        }
                    }
                }
            }
        }
    }
}
