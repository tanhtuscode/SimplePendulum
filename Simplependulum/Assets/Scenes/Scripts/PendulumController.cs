using UnityEngine;

public class PendulumController : MonoBehaviour
{
    public float maxDragDistance = 5f;
    private Vector3 initialPosition;
    private Rigidbody sphereRigidbody;
    public FixedJoint fixedJoint;
    public HingeJoint hingeJoint;

    void Start()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        //fixedJoint = GetComponent<FixedJoint>();

        // Assuming the HingeJoint is on the connected cylinder, you may need to adjust this based on your actual hierarchy
        //hingeJoint = GetComponentInChildren<HingeJoint>();

        if (hingeJoint == null)
        {
            Debug.LogError("HingeJoint component not found. Make sure it is on the connected cylinder.");
            return;
        }

        // Store the initial position of the sphere
        initialPosition = transform.position;

        // Set up the hinge joint limits based on the length of the connecting cylinder
        hingeJoint.limits = new JointLimits { min = -maxDragDistance, max = maxDragDistance };
    }

    void Update()
    {
        // Check for user input to simulate dragging the sphere
        if (Input.GetMouseButton(0))
        {
            DragSphere();
        }
    }

    void DragSphere()
    {
        // Get the mouse position in the world coordinates
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the new position for the sphere within the drag limit
            Vector3 newPosition = initialPosition + Vector3.ClampMagnitude(hit.point - initialPosition, maxDragDistance);

            // Move the sphere to the new position
            sphereRigidbody.MovePosition(newPosition);
        }
    }
}