#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEditor;
#endif

using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
public class RectCollider : PhysicCollider
{
    [SerializeField] float width = 1;
    [SerializeField] float height = 1;

    Vector3[] vertices = new Vector3[4];

    public Vector3[] TransformedVertices => vertices;

    private void Start()
    {
        CalculateVertices();
    }

    private void LateUpdate()
    {
        CalculateVertices();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CalculateVertices()
    {
        vertices[0] = transform.position + (-transform.right * width / 2) + (transform.up * height / 2);
        vertices[1] = transform.position + (transform.right * width / 2) + (transform.up * height / 2);
        vertices[2] = transform.position + (transform.right * width / 2) + (-transform.up * height / 2);
        vertices[3] = transform.position + (-transform.right * width / 2) + (-transform.up * height / 2);
    }


#if UNITY_EDITOR
    private void Reset()
    {
        width = Mathf.Abs(transform.localScale.x);
        height = Mathf.Abs(transform.localScale.y);
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) CalculateVertices();
        
        Handles.color = Color.green;

        for (int i = 0; i < vertices.Length; i++)
        {
            Handles.DrawLine(vertices[0], vertices[1]);
            Handles.DrawLine(vertices[1], vertices[2]);
            Handles.DrawLine(vertices[2], vertices[3]);
            Handles.DrawLine(vertices[3], vertices[0]);
        }
    }
#endif
}
