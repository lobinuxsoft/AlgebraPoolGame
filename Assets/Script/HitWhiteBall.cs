using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
public class HitWhiteBall : MonoBehaviour
{
    [SerializeField] float maxDistanceForce = 3.5f;
    [SerializeField] float forceMultiplier = 30.5f;  

    private PhysicBody whiteBall;
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
    }    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            pixelCoordinatesMousePos = Input.mousePosition;
            worldCoordinatesMousePos = cam.ScreenToWorldPoint(new Vector3(pixelCoordinatesMousePos.x, pixelCoordinatesMousePos.y, cam.nearClipPlane)); //Sacas la coordenada en wordl coordinates              
            
            distanceMouseBall = Vector2.Distance(worldCoordinatesMousePos,  whiteBall.transform.position); //Sacamos distancia entre mouse y la pelota

            direction = (whiteBall.transform.position - worldCoordinatesMousePos).normalized; //Sacas la direccion normalizada del vector           

            //distanceMouseBall = Mathf.Clamp(distanceMouseBall, 0, maxDistanceForce); //Le das un minimo / maximo al valor de la distancia

            force = (distanceMouseBall / maxDistanceForce) * forceMultiplier; //Calculamos la fuerza que va a resivir el rigid body

            whiteBall.HitByCue(direction, force);
        }       
    }   
}
