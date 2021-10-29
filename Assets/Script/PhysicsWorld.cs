using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    [SerializeField] private List<PhysicBody> physicBodies = new List<PhysicBody>();


    private void Update()
    {
        if(physicBodies.Count > 0)
        {

        }
    }

    void AddObject(PhysicBody physicBody)
    {
        physicBodies.Add(physicBody);
    }
}
