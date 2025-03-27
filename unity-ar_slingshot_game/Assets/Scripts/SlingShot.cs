using System.Collections;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public GameObject ammoPrefab;
    public GameObject currentAmmo = null;
    int forceMultiplier = 700;
    int forceFordwardScalar = 4;
    public int AmmoLeft {get; set;}

    public bool dragging;
    float mouseZCoord;
    Vector3 mouseOffset;
    public AudioSource ThrowSound;

    public delegate void OnReloadEventHandler(int ammoLeft);
    public event OnReloadEventHandler OnReload;

    // called b4 1st frame update
    void Start()
    {
        AmmoLeft = 100;
    }
    public void Reload()
    {
        if (currentAmmo == null && transform.childCount == 0 && AmmoLeft > 0)
        {
            currentAmmo = Instantiate(ammoPrefab, transform.position, transform.rotation, transform);
            currentAmmo.GetComponent<Rigidbody>().isKinematic = true;
            currentAmmo.GetComponent<Ammo>().OnAmmoHit += OnCurrentAmmoHit;
            AmmoLeft--;            
        }
        OnReload?.Invoke(AmmoLeft);
    }
    void Throw()
    {
        if (currentAmmo != null && transform.childCount == 1)
        {
            currentAmmo.transform.parent = null;
            currentAmmo.GetComponent<Rigidbody>().isKinematic = false;
            currentAmmo.GetComponent<Rigidbody>().AddForce(GetThrowForce());
            currentAmmo.GetComponent<Ammo>().shot = true;
            currentAmmo.GetComponent<Ammo>().throwOrigin = currentAmmo.transform.position;       
            currentAmmo = null;
            ThrowSound.Play();
        }
    }
    public void Clear()
    {
        if (currentAmmo)
        {
            Destroy(currentAmmo);
            currentAmmo = null;            
        }
        AmmoLeft = 0;
    }

    IEnumerator DelayReload(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        Reload();
    }

    void OnCurrentAmmoHit()
    {
        StartCoroutine(DelayReload(1));
    }

    public Vector3 GetThrowForce()
    {
        Vector3 force = (transform.position - currentAmmo.transform.position) * forceMultiplier;
        force = (transform.forward * force.magnitude * forceFordwardScalar) + force;
        return force;
    }

    public Vector3 TouchPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void Update()
    {
        if (currentAmmo == null)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !dragging)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == currentAmmo)
                {
                    Grab();
                    dragging = true;
                }
            }
        }
        if (dragging)
        {
            Drag();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && dragging)
        {
            Release();
            dragging = false;
        }
    }

    void Grab()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(currentAmmo.transform.position).z;
        mouseOffset = currentAmmo.transform.position - TouchPos();
    }
    void Drag()
    {
        currentAmmo.transform.position = TouchPos() + mouseOffset;
        currentAmmo.transform.forward = GetThrowForce().normalized;
    }
    void Release()
    {
        Throw();
    }
}
