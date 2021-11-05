using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] private PhysicBody[] physicBodies;
    [SerializeField] private FlatVector flatVector = new FlatVector(3,2);

    private void Start()
    {
        physicBodies = FindObjectsOfType<PhysicBody>();
    }

    private void Update()
    {
        if(physicBodies.Length > 0)
        {

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        FlatVector flatNormalized = FlatMath.Normalize(flatVector);
        Handles.color = Mathf.CorrelatedColorTemperatureToRGB(FlatMath.Magnitude(flatVector));
        Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.LookRotation(new Vector3(flatVector.x, flatVector.y)), FlatMath.Magnitude(flatVector), EventType.Repaint);
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, FlatMath.Magnitude(flatVector));
        Handles.color = Color.blue;
        Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.LookRotation(new Vector3(flatNormalized.x, flatNormalized.y)), FlatMath.Magnitude(flatNormalized), EventType.Repaint);
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, FlatMath.Magnitude(flatNormalized));
    }
#endif
}
