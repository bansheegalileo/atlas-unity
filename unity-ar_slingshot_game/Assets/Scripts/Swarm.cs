using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Swarm : MonoBehaviour
{
    float rayYoffset;
    Vector3 planeCenter;
    float range;
    public float speed = 0.01f;
    bool hasdest;
    Vector3 dest;
    bool moving = false;
    ARPlane movePlane;
    float colliderHeight;
    Quaternion destRotation;
    void Start()
    {
    }

    // called once a frame
    void Update()
    {
        if (!moving)
            return;
        if (hasdest)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, destRotation, speed * 20);            
            transform.position = Vector3.MoveTowards(transform.position, dest, speed);
            if (Vector3.Distance(transform.position, dest) < 0.01f)
            {
                hasdest = false;
            }
        }
        else
        {
            hasdest = randomPoint(planeCenter, rayYoffset, range, out dest);
            destRotation = Quaternion.LookRotation(dest - transform.position, Vector3.up);
        }
    }
    public void Move(ARPlane plane)
    {
        movePlane = plane;
        planeCenter = plane.center;
        range = Mathf.Max(plane.size.x, plane.size.y);
        rayYoffset = 0.5f;
        colliderHeight = transform.localScale.y * GetComponent<CapsuleCollider>().height;
        transform.position = planeCenter + Vector3.up * colliderHeight / 2;
        hasdest = randomPoint(planeCenter, rayYoffset, range, out dest);
        destRotation = Quaternion.LookRotation(dest - transform.position, Vector3.up);
        moving = true;
    }

    public void StopMove()
    {
        moving = false;
    }
    public bool randomPoint(Vector3 center, float rayYoffset, float range, out Vector3 result)
    {
        Vector3 next = center + Random.insideUnitSphere * range;
        RaycastHit hit;
        if (Physics.Raycast(next, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == movePlane.gameObject)
            {
                result = hit.point + Vector3.up * colliderHeight / 2;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
