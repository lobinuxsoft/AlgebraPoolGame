using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
[RequireComponent(typeof(LineRenderer))]
public class HitWhiteBall : MonoBehaviour
{
    [SerializeField] float maxDistanceForce = 3.5f;
    [SerializeField] float forceMultiplier = 30.5f;  

    private PhysicBody whiteBall;
    private LineRenderer ballMouseLine;
    private Gradient lineColor = new Gradient();
    private Camera cam;    
    private Vector2 pixelCoordinatesMousePos;
    private Vector3 worldCoordinatesMousePos;
    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);    
    private float distanceMouseBall;     
    private float force = 0;   

    void Start()
    {
        cam = Camera.main;       
        whiteBall = GetComponent<PhysicBody>();
        ballMouseLine = GetComponent<LineRenderer>();       
        ballMouseLine.positionCount = 2;
        ballMouseLine.startWidth = 0.25f;
        ballMouseLine.endWidth = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {

        pixelCoordinatesMousePos = Input.mousePosition;
        worldCoordinatesMousePos = cam.ScreenToWorldPoint(new Vector3(pixelCoordinatesMousePos.x, pixelCoordinatesMousePos.y, cam.nearClipPlane)); //Sacas la coordenada en wordl coordinates              

        ballMouseLine.SetPosition(0, worldCoordinatesMousePos);
        ballMouseLine.SetPosition(1, whiteBall.transform.position);       

        if (Input.GetMouseButtonUp(0)) 
        {
            distanceMouseBall = Vector2.Distance(worldCoordinatesMousePos, whiteBall.transform.position); //Sacamos distancia entre mouse y la pelota

            direction = (whiteBall.transform.position - worldCoordinatesMousePos).normalized; //Sacas la direccion normalizada del vector 

            force = (distanceMouseBall / maxDistanceForce) * forceMultiplier; //Calculamos la fuerza que va a resivir el rigid body

            whiteBall.HitByCue(direction, force);
        }        
    }    
}
