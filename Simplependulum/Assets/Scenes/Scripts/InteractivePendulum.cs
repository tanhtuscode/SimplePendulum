using UnityEngine;

public class InteractivePendulum : MonoBehaviour
{
    public float length = 5f;  // Length of the pendulum
    public float gravity = 9.8f;  // Acceleration due to gravity
    public float dampingFactor = 0.99f; // Damping factor to simulate air resistance

    private float angularVelocity;
    private float currentAngle;

    private Vector3 mousePositionOffset; // Offset to store the initial mouse position when dragging

    void Start()
    {
        // Set initial position of the pendulum
        ResetPendulum();
    }

    void Update()
    {
        // Check for left control key down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Check for mouse button down
            if (Input.GetMouseButtonDown(0))
            {
                // Store the initial mouse position
                mousePositionOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            // Check for mouse button held down
            if (Input.GetMouseButton(0))
            {
                // Update the position of the pendulum based on the horizontal mouse movement
                float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                transform.position = new Vector3(mouseX + mousePositionOffset.x, transform.position.y, 0);
            }

            // Update the angular velocity and angle based on the physics of a simple pendulum
            float deltaTime = Time.deltaTime;
            float angularAcceleration = -(gravity / length) * Mathf.Sin(currentAngle);
            angularVelocity += angularAcceleration * deltaTime;
            currentAngle += angularVelocity * deltaTime;

            // Damping to simulate air resistance
            angularVelocity *= dampingFactor;

            // Ensure the pendulum stays within the specified length
            float maxX = length * Mathf.Sin(currentAngle);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -maxX, maxX), transform.position.y, 0);
        }
    }

    void ResetPendulum()
    {
        // Set initial angle and velocity
        float initialAngleRad = Random.Range(-Mathf.PI / 4, Mathf.PI / 4); // Random initial angle
        angularVelocity = 0f;  // Assuming initial velocity is zero
        currentAngle = initialAngleRad;

        // Set initial position of the pendulum
        float x = length * Mathf.Sin(initialAngleRad);
        float y = -length * Mathf.Cos(initialAngleRad);
        transform.position = new Vector3(x, y, 0);
    }
}
