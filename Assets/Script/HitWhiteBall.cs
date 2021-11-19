using UnityEngine;

[RequireComponent(typeof(PhysicBody))]
[RequireComponent(typeof(LineRenderer))]

public class HitWhiteBall : MonoBehaviour
{
    [SerializeField] float maxDistanceForce = 3.5f;
    [SerializeField] float forceMultiplier = 30.5f;  

    private PhysicBody whiteBall;
    private LineRenderer ballMouseLine;    
    private Camera cam;    
    private Vector2 pixelCoordinatesMousePos;
    private Vector3 worldCoordinatesMousePos;
    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 positionWhereMousePressed;
    private float distanceMousePressedMouseReleased = 0.0f;    
    private float force = 0.0f;   

    void Start()
    {
        cam = Camera.main;       
        whiteBall = GetComponent<PhysicBody>();
        ballMouseLine = GetComponent<LineRenderer>();       
        ballMouseLine.positionCount = 2;
        ballMouseLine.startWidth = 0.15f;
        ballMouseLine.endWidth = 0.15f;
        ballMouseLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        pixelCoordinatesMousePos = Input.mousePosition;

        worldCoordinatesMousePos = cam.ScreenToWorldPoint(new Vector3(pixelCoordinatesMousePos.x, pixelCoordinatesMousePos.y, cam.nearClipPlane)); //Sacas la posicion del mouse en coordenadas de mundo         
        
        distanceMousePressedMouseReleased = Vector2.Distance(worldCoordinatesMousePos, positionWhereMousePressed); //Sacamos distancia entre donde se preciono y donde se libero el click del mouse
        distanceMousePressedMouseReleased /= maxDistanceForce; //Te aseguras que la distancia no sea mayor a un maximo

        direction = (positionWhereMousePressed - worldCoordinatesMousePos).normalized; //Sacas la direccion normalizada del vector
                                                                                       //
        ballMouseLine.SetPosition(0, whiteBall.transform.position - (direction * distanceMousePressedMouseReleased));
        ballMouseLine.SetPosition(1, whiteBall.transform.position);

        if (Input.GetMouseButtonDown(0)) 
        {            
            ballMouseLine.enabled = true;

            ballMouseLine.SetPosition(0, whiteBall.transform.position);            

            positionWhereMousePressed = worldCoordinatesMousePos;
        }        

        if (Input.GetMouseButtonUp(0)) 
        {            
            ballMouseLine.enabled = false;
            
            force = distanceMousePressedMouseReleased * forceMultiplier; //Calculamos la fuerza que va a resivir el rigid body

            whiteBall.HitByCue(direction, force); //Le mandamos la direccion y la fuerza al rigid body
        }        
    }    
}
