using UnityEngine;

public class GameplayInput : MonoBehaviour
{
    [SerializeField] private float tolerance;
    private Camera camera;
    private ConveyorReciver conveyor;
    private Vector2 mousePosition;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                conveyor = hit.collider.GetComponent<ConveyorReciver>();
                if (conveyor != null)
                {
                    mousePosition = Input.mousePosition;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (conveyor != null)
            {
                float swipe = mousePosition.x - Input.mousePosition.x;
                if (Mathf.Abs(swipe) > tolerance)
                {
                    conveyor.Swipe(swipe);
                }
                conveyor = null;
            }
        }
    
    }
}
