using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour {

    public Transform pivot;

    public Material lineMat;

    public float radius = 0.05f;

    // Kết nối tất cả các `points` với `mainPoint`
    public GameObject mainPoint;
    public GameObject[] points;

    // Điền vào đây với mesh mặc định của Unity Cylinder
    // Tính đến trục chính/origin của cylinder ở giữa.
    public Mesh cylinderMesh;
    GameObject[] ringGameObjects;

   
    void Start ()
    {
        this.ringGameObjects = new GameObject[points.Length];
        //this.connectingRings = new ProceduralRing[points.Length];
        for (int i = 0; i < points.Length; i++) {
            // Tạo một gameobject để đặt vòng lên
            // Và sau đó đặt nó như là con của gameobject 
            this.ringGameObjects [i] = new GameObject ();
            this.ringGameObjects [i].name = "ConnectedRing#" + i;
            this.ringGameObjects [i].transform.parent = this.gameObject.transform;

            // Chúng tôi tạo một gameobject offset để đối phó với trục chính/origin mặc định của cylindermesh ở giữa
            GameObject ringOffsetCylinderMeshObject = new GameObject ();
            ringOffsetCylinderMeshObject.transform.parent = this.ringGameObjects [i].transform;

            // Offset cylinder để pivot/origin ở dưới liên quan đến outer ring gameobject.
            ringOffsetCylinderMeshObject.transform.localPosition = new Vector3 (0f, 1f, 0f);
            // Đặt bán kính
            ringOffsetCylinderMeshObject.transform.localScale = new Vector3 (radius, 1f, radius);

            // Tạo Mesh và renderer để hiển thị vòng kết nối
            MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter> ();
            ringMesh.mesh = this.cylinderMesh;

            MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer> ();
            ringRenderer.material = lineMat;
        }
    }

    // Update được gọi mỗi frame
    void Update ()
    {
        for(int i = 0; i < points.Length; i++) {
            // Di chuyển vòng đến điểm
            this.ringGameObjects[i].transform.position = this.points[i].transform.position;

            // Phù hợp tỷ lệ với khoảng cách
            float cylinderDistance = 2f * Vector3.Distance(this.points[i].transform.position, this.mainPoint.transform.position);
            this.ringGameObjects[i].transform.localScale = new Vector3(this.ringGameObjects[i].transform.localScale.x, cylinderDistance, this.ringGameObjects[i].transform.localScale.z);

            // Làm cho cylinder nhìn về main point.
            // Vì cylinder đang chỉ lên (y) và forward là z, cần điều chỉ bằng 90 độ.
            this.ringGameObjects[i].transform.LookAt(this.mainPoint.transform, Vector3.up);
            this.ringGameObjects[i].transform.rotation *= Quaternion.Euler(90, 0, 0);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawLine (transform.position, pivot.position);
    }
}
