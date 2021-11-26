using UnityEngine;

public class BallContainer : MonoBehaviour
{
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
        else
        {
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
        }
    }
}
