using UnityEngine;

public class TrajLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    int numPoints = 60;
    SlingShot slingShot;

    float timeBetweenPoints = 0.02f;
    public LayerMask CollidableLayers;

    public Material ammoHitMaterial;
    // called b4 1st frame update
    void Start()
    {
        slingShot = GetComponent<SlingShot>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // called once a frame
    void Update()
    {
        if (slingShot.currentAmmo && slingShot.dragging)
            DrawTraj();
        else if (slingShot.currentAmmo == null)
            lineRenderer.positionCount = 0;
    }
    void DrawTraj()
    {
        int linePoints = numPoints;
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPosition(0, slingShot.currentAmmo.transform.position);

        Vector3 force = slingShot.GetThrowForce();
        Vector3 startingPosition = slingShot.currentAmmo.transform.position;
        Vector3 startingVelocity = force * (Time.fixedDeltaTime / slingShot.currentAmmo.GetComponent<Rigidbody>().mass);

        int i = 1;
        for (float t = Time.fixedDeltaTime; i < linePoints; t += timeBetweenPoints, i++)
        {
            Vector3 newPosition = startingPosition + t * startingVelocity;
            newPosition.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f  * t * t;
            lineRenderer.SetPosition(i, newPosition);


            if(Physics.OverlapSphere(newPosition, slingShot.currentAmmo.GetComponent<SphereCollider>().radius * slingShot.currentAmmo.transform.localScale.y, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = i;
                break;
            }
        }
    }
}
