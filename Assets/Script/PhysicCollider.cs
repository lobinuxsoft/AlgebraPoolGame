using UnityEngine;

public class PhysicCollider : MonoBehaviour
{
    protected PhysicBody bodyComponent = null;

    protected bool isColliding = false;

    private void Awake()
    {
        bodyComponent = GetComponent<PhysicBody>();
    }

    public virtual bool CollisionCheck(PhysicCollider other, out Vector3 normal, out float depth) 
    {
        normal = Vector3.zero;
        depth = 0;
        return false;
    }
}
