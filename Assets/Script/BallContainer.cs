using UnityEngine;

public class BallContainer : MonoBehaviour
{
    public void OnBallEnterTheHole(GameObject go)
    {
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
    }
}
