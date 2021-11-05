using UnityEditor;
using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    [SerializeField] private bool isStatic = false;

    [SerializeField] private Vector3 linearVelocity = Vector3.zero;
    [SerializeField] private float rotation = 0;
    [SerializeField] private float rotationalVelocity = 0;

    [SerializeField] private float density = 0;
    [SerializeField] private float mass = 0;
    [SerializeField, Range(0,1)] private float restitution = 0; // Esto es con la potencia que rebota.
    
    private float area = 0;

    public float Area { get; set; }

    private void Start()
    {
        linearVelocity = Vector3.zero;
        rotation = 0;
        rotationalVelocity = 0;
        restitution = Mathf.Clamp01(restitution);

        mass = area * density;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(linearVelocity), 1f, EventType.Repaint);
    }
#endif
}
