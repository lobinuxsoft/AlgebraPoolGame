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

    public void SetRadius(float value)
    {
        radius = value;
    }


#if UNITY_EDITOR

    private void Reset()
    {
        radius =  Mathf.Abs(transform.localScale.x) > Mathf.Abs(transform.localScale.y) ? Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.y);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(Center, Vector3.forward, Radius, 1f);
        Handles.DrawLine(Center, transform.position + (transform.right * Radius));
    }
#endif
}


//Rozamiento
//COlisionamiento(entre bolas, con las paredes y los agujeros de las bochas)
//traspazo de energia