using UnityEngine;
using UnityEngine.Events;

public class BallContainer : MonoBehaviour
{
    [SerializeField] bool listenPhysicWorld = true;
    [SerializeField] int ballsNeeded = 7;
    [SerializeField] string tagIdentifier = "";
    [SerializeField] PhysicsWorld physicsWorld = default;
    [SerializeField] Vector3 startWhiteBallPosition = Vector3.zero;
    [SerializeField] float ballsSpace = 3f;

    public UnityEvent<string> onBallSelected;
    public UnityEvent onCorrectBallEnter;
    public UnityEvent<GameObject> onIncorrectBallEnter;
    public UnityEvent onWin;
    public UnityEvent onLose;

    private void LateUpdate()
    {
        if(Time.frameCount%60 == 0)
        {
            ReOrganizedChilds();
        }
    }

    public void OnBallEnterTheHolePhysicWorld(GameObject go)
    {
        if(listenPhysicWorld) OnBallEnterTheHole(go);
    }

    public void OnBallEnterTheHoleForced(GameObject go)
    {
        OnBallEnterTheHole(go);
    }

    private void OnBallEnterTheHole(GameObject go)
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
        else if(go.CompareTag("Ball8"))
        {
            if (transform.childCount >= ballsNeeded)
            {
                //GANO
                Debug.LogWarning($"gano {gameObject.name}");
                onWin?.Invoke();
            }
            else
            {
                //PERDIO
                Debug.LogWarning($"perdio {gameObject.name}");
                onLose?.Invoke();
            }

            physicsWorld.RemoveFromWorld(go.GetComponent<PhysicCollider>(), physicBody);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
        }
        else if (string.IsNullOrEmpty(tagIdentifier))
        {
            tagIdentifier = go.tag;
            ballsNeeded = GameObject.FindGameObjectsWithTag(tagIdentifier).Length;
            onBallSelected?.Invoke(go.tag);
            onCorrectBallEnter?.Invoke();
        }
        else if(go.CompareTag(tagIdentifier))
        {
            physicsWorld.RemoveFromWorld(go.GetComponent<PhysicCollider>(), physicBody);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            if(listenPhysicWorld) onCorrectBallEnter?.Invoke();
        }
        else
        {
            onIncorrectBallEnter?.Invoke(go);
        }
    }

    void ReOrganizedChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, -(ballsSpace * i), 0);
        }
    }

    public void SetListenPhysicWorld(bool value)
    {
        listenPhysicWorld = value;
    }

    public void SetBallSelected(string tag)
    {
        if(tag == "Striped")
        {
            tagIdentifier = "Smooth";
        }
        else
        {
            tagIdentifier = "Striped";
        }

        ballsNeeded = GameObject.FindGameObjectsWithTag(tagIdentifier).Length;
    }
}
