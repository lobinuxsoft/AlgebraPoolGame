using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    public enum ShapeType
    {
        Circle, Box
    }

    [SerializeField] ShapeType shapeType = ShapeType.Circle;
    [SerializeField] private Vector3 linearVelocity = Vector3.zero;
    [SerializeField] float rotation = 0;
    [SerializeField] float rotationalVelocity = 0;

    [SerializeField] float density = 0;
    [SerializeField] float mass = 0;
    [SerializeField] float restitution = 0;
    [SerializeField] float area;

    [SerializeField] bool isStatic = false;

    float radius = 0;
    float width = 0;
    float height = 0;

}
