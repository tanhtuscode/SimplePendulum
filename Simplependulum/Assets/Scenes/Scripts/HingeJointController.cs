using UnityEngine;

public class HingeJointController : MonoBehaviour
{
    public HingeJoint hingeJoint;
    public float swingSpeed = 10.0f; // Adjust the speed as needed

    void Start()
    {
        if (hingeJoint == null)
        {
            hingeJoint = GetComponent<HingeJoint>();
        }
    }

    void Update()
    {
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = swingSpeed;
        hingeJoint.motor = motor;
    }
}