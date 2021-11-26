using UnityEngine;

public class BallContainer : MonoBehaviour
{
    [SerializeField] string tagIdentifier = "";
    [SerializeField] PhysicsWorld physicsWorld = default;
    [SerializeField] Vector3 startWhiteBallPosition = Vector3.zero;
    public void OnBallEnterTheHole(GameObject go)
    {
        PhysicBody physicBody = go.GetComponent<PhysicBody>();
        
        if(physicBody != null)
        {
            physicBody.SetAcceleration(0);
        }

        if (go.CompareTag("WhiteBall"))
        {
            go.transform.position = startWhiteBallPosition;
        }
        else if(go.CompareTag(tagIdentifier))
        {
            physicsWorld.RemoveFromWorld(go.GetComponent<PhysicCollider>(), physicBody);
            //CircleCollider circleCollider = go.GetComponent<CircleCollider>();
            //circleCollider.SetRadius(0);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, -(3f * i), 0);
        }
    }
}
