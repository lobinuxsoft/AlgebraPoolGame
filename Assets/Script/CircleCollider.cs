#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEditor;
#endif

using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
public class CircleCollider : PhysicCollider
{
    [SerializeField] private float radius = 0;
    public float Radius => radius / 2;
    public Vector3 Center => transform.position;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(Center, Vector3.forward, Radius, 1f);
        Handles.DrawLine(Center, transform.position + (transform.right * Radius));
    }
#endif
}
