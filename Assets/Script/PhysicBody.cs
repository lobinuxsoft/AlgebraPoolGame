using UnityEditor;
using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    public enum ShapeType
    {
        Circle, Box
    }

    [SerializeField] ShapeType shapeType = ShapeType.Circle;
    [SerializeField] private bool isStatic = false;
    [SerializeField] private float radius = 0;
    [SerializeField] private float width = 0;
    [SerializeField] private float height = 0;

    [SerializeField] private Vector3 linearVelocity = Vector3.zero;
    [SerializeField] private float rotation = 0;
    [SerializeField] private float rotationalVelocity = 0;

    [SerializeField] private float density = 0;
    [SerializeField] private float mass = 0;
    [SerializeField, Range(0,1)] private float restitution = 0; // Esto es con la potencia que rebota.
    
    private float area = 0;

   

    private void Start()
    {
        linearVelocity = Vector3.zero;
        rotation = 0;
        rotationalVelocity = 0;
        restitution = Mathf.Clamp01(restitution);

        switch (shapeType)
        {
            case ShapeType.Circle:
                area = radius * radius * Mathf.PI;
                break;
            case ShapeType.Box:
                area = width * height;
                break;
        }

        mass = area * density;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        switch (shapeType)
        {
            case ShapeType.Circle:
                Handles.color = Color.green;
                Handles.DrawWireDisc(transform.position, Vector3.forward, radius/2, 1f);
                Handles.DrawLine(transform.position, transform.right * radius / 2);
                break;
            case ShapeType.Box:
                Handles.DrawSolidRectangleWithOutline(new Rect(transform.position.x - width / 2, transform.position.y - height / 2, width, height), Color.clear, Color.green);
                break;
        }
    }
#endif
}
