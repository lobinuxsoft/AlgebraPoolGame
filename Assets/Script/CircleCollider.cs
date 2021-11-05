#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class CircleCollider : PhysicCollider
{
    [SerializeField] private float radius = 0;

    public float Radius => radius;

    private void Start()
    {
        if(bodyComponent != null) bodyComponent.Area = radius * radius * Mathf.PI;
    }

    public override bool CollisionCheck(PhysicCollider other, out Vector3 normal, out float depth)
    {
        normal = Vector3.zero;
        depth = 0;

        if(other is CircleCollider)
        {
            CircleCollider circleCollider = (CircleCollider)other;
            // Si la distancia entre los centros es menor a la suma de los radios, entonces existe una interseccion.
            float distance = Vector3.Distance(transform.position, other.transform.position); // Distancia entre los centros
            float radii = radius/2 + circleCollider.Radius/2; // Suma de los radios

            isColliding = distance < radii; // Existe interseccion?

            // Si existe interseccion se calcula la normal de la collision y la profundidad de la interseccion
            if (isColliding)
            {
                normal = (other.transform.position - transform.position).normalized;
                depth = radii - distance;
            }
        }

        return isColliding;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = isColliding ? Color.red : Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius / 2, 1f);
        Handles.DrawLine(transform.position, transform.position + (transform.right * radius / 2));
    }
#endif
}
