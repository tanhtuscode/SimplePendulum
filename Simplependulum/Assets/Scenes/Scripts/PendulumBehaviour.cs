using UnityEngine;

public class PendulumBehaviour : MonoBehaviour
{
    float distance = 0;

    public Transform pivot;
    private float curangle;

    public Rigidbody rigid;

    private bool ad = false;



    void Update()
    {
        curangle = pivot.transform.rotation.eulerAngles.z;

        if (curangle > 0.5)
        {
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (curangle > 90 && curangle < 180)
        {
            curangle = 90 - curangle;
        }
        else if (curangle > 180 && curangle < 270)
        {
            curangle = 180 - curangle;
        }
        else if (curangle > 270 && curangle <= 360)
        {
            curangle = 360 - curangle;
        }
    }

    void OnMouseDrag()
    {
        Vector3 screepoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseposition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screepoint.z);
        Vector3 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        transform.position = objposition;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnMouseExit()
    {
        
        ad = false;
    }

    
}