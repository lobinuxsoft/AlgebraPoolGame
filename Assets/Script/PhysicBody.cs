using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    [SerializeField] float mass = 1f;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 force = Vector3.zero;

    private void Update()
    {
        velocity += force / mass;
        transform.position += velocity * Time.deltaTime;

        force = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        this.force += force;
    }
}
