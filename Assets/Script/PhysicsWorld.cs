using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] private PhysicCollider[] physicsColliders;

    private void Start()
    {
        physicsColliders = FindObjectsOfType<PhysicCollider>();
    }

    private void Update()
    {
        if(physicsColliders.Length > 0)
        {
            for (int i = 0; i < physicsColliders.Length; i++)
            {
                physicsColliders[i].CollisionCheck(physicsColliders[i + 1], out Vector3 normal, out float depth);
                physicsColliders[i+1].CollisionCheck(physicsColliders[i], out Vector3 normal2, out float depth2);
            }
        }
    }
}
