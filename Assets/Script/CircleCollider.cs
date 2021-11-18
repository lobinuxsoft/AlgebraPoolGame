#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
public class CircleCollider : PhysicCollider
{
    [SerializeField] private float radius = 0;
    public float Radius => radius / 2;
    public Vector3 Center => transform.position;

    private void Reset()
    {
        radius =  Mathf.Abs(transform.localScale.x) > Mathf.Abs(transform.localScale.y) ? Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(Center, Vector3.forward, Radius, 1f);
        Handles.DrawLine(Center, transform.position + (transform.right * Radius));
    }
#endif
}
