using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWhiteBall : MonoBehaviour
{
    [SerializeField]GameObject whiteBall;
    [SerializeField] float maxDistanceForce = 0;
    [SerializeField] float forceMultiplier = 5.5f;  
    private Camera cam;    
    private Vector2 pixelCoordinatesMousePos;
    private Vector3 worldCoordinatesMousePos;
    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);    
    private float distanceMouseBall;
    private float catetoUno;
    private float catetoDos;
    private float force = 0;

    void Start()
    {
        cam = Camera.main;
    }    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && force == 0) 
        {
            pixelCoordinatesMousePos = Input.mousePosition;
            worldCoordinatesMousePos = cam.ScreenToWorldPoint(new Vector3(pixelCoordinatesMousePos.x, pixelCoordinatesMousePos.y, cam.nearClipPlane));

            catetoUno = worldCoordinatesMousePos.x - whiteBall.transform.position.x;
            catetoDos = worldCoordinatesMousePos.y - whiteBall.transform.position.y;

            distanceMouseBall = Mathf.Sqrt(Mathf.Pow(catetoUno, 2) + Mathf.Pow(catetoDos, 2));
            direction.x = catetoUno;
            direction.y = catetoDos;

            direction.x /= distanceMouseBall;
            direction.y /= distanceMouseBall;

            if (distanceMouseBall > maxDistanceForce)
            {
                force = maxDistanceForce * forceMultiplier;
            }
            else 
            {
                force = distanceMouseBall * forceMultiplier;
            }

            Debug.Log(distanceMouseBall);
        }

        if (force > 0)
        {
            force -= Time.deltaTime * 10;

            whiteBall.transform.position -= direction * force * Time.deltaTime;
        }
        else 
        {
            force = 0;
        }
    }   
}
