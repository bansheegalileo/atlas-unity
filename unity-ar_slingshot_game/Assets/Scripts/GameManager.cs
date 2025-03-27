using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public go's
    [Header("Targets")]
    public GameObject targetPrefab;
    public int targetsNum = 5;
    public int ammo = 7;

    [Header("UI Canvas Objects")]
    public GameObject planeSearchingCanvas;
    public GameObject selectPlaneCanvas;
    public GameObject startButton;
    public GameObject gameUI;
    public Text scoreTxt;
    public GameObject ammoImagePrefab;
    public GameObject ammoImageGrid;
    public GameObject playAgainButton;

    [Header("Sounds")]
    public AudioSource EndingSound;
    public AudioSource planeSelectedSound;


    [Header("Materials")]
    public Material PlaneOcclusionMaterial;

    // private vars
    int totalPoints = 0;

    // private go's
    ARPlane selectedPlane = null;    
    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    SlingShot slingShot;
    ARSession session;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Dictionary<int, GameObject> targets = new Dictionary<int, GameObject>();

    //vnts
    public delegate void PlaneSelectedEventHandler(ARPlane thePlane);
    public event PlaneSelectedEventHandler OnPlaneSelected;
    void Awake()
    {
        session = FindObjectOfType<ARSession>();
        session.Reset();
    }    
    
    // called b 1st frame upd8
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        planeManager = FindObjectOfType<ARPlaneManager>();
        slingShot = FindObjectOfType<SlingShot>();
        
        planeManager.planesChanged += PlanesFound;
        OnPlaneSelected += PlaneSelected;
    }

    // called once a drame
    void Update()
    {
        if (Input.touchCount > 0 && selectedPlane == null && planeManager.trackables.count > 0)
        {
            SelectPlane();
        }
    }
    private void SelectPlane()
    {
        Touch touch = Input.GetTouch(0);
        

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit hit = hits[0];
                selectedPlane =  planeManager.GetPlane(hit.trackableId);                
                selectedPlane.GetComponent<LineRenderer>().positionCount = 0;

                selectedPlane.GetComponent<Renderer>().material = PlaneOcclusionMaterial;
                
                foreach(ARPlane plane in planeManager.trackables)
                {
                    if (plane != selectedPlane)
                    {
                        plane.gameObject.SetActive(false);
                    }
                }
                planeManager.enabled = false;
                selectPlaneCanvas.SetActive(false);
                OnPlaneSelected?.Invoke(selectedPlane);
            }
        }
    }

    void PlanesFound(ARPlanesChangedEventArgs args)
    {
        if (selectedPlane == null && planeManager.trackables.count > 0)
        {
            planeSearchingCanvas.SetActive(false);
            selectPlaneCanvas.SetActive(true);
            planeManager.planesChanged -= PlanesFound;
        }
    }

    void PlaneSelected(ARPlane plane)
    {
        planeSelectedSound.Play();
        foreach (KeyValuePair<int, GameObject> target in targets)
        {
            Destroy(target.Value);
        }
        targets.Clear();

        startButton.SetActive(true);
        for (int i = 1; i <= targetsNum; i++)
        {
            GameObject target = Instantiate(targetPrefab, plane.center, plane.transform.rotation, plane.transform);
            target.GetComponent<Swarm>().Move(plane);
            target.GetComponent<Target>().ID = i;
            target.GetComponent<Target>().OnTargetDestroy += UpdateGameWhenHitTarget;
            targets.Add(i, target);
        }
    }

    void UpdateGameWhenHitTarget(int id, int points)
    {
        targets.Remove(id);
        totalPoints += points;
        scoreTxt.text = totalPoints.ToString();
        if (targets.Count == 0)
        {
            ShowPlayAgain();
        }
    }
    public void StartGame()
    {
        slingShot.AmmoLeft = ammo;
        slingShot.OnReload += ShotReload;
        slingShot.Reload();
        totalPoints = 0;
        scoreTxt.text = totalPoints.ToString();
        startButton.SetActive(false);
        gameUI.SetActive(true);

        

        for (int i = 0; i < slingShot.AmmoLeft; i++)
        {
            GameObject ammoGO = Instantiate(ammoImagePrefab);
            ammoGO.transform.SetParent(ammoImageGrid.transform, false);
        }
    }
    void ShotReload(int ammoLeft)
    {
        if (ammoImageGrid.transform.childCount > 0 && ammoLeft >= 0)
        {
            Destroy(ammoImageGrid.transform.GetChild(0).gameObject);
        }
        else if (ammoLeft == 0)
        {
            ShowPlayAgain();            
        }
    }
    public void ShowPlayAgain()
    {
        EndingSound.Play();
        foreach (Transform ammoImge in ammoImageGrid.transform)
        {
            Destroy(ammoImge.gameObject);
        }
        slingShot.Clear();
        slingShot.OnReload -= ShotReload;
        playAgainButton.SetActive(true);
    }

    public void PlayAgain()
    {
        PlaneSelected(selectedPlane);
        EndingSound.Stop();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("ARSlingshotGame");
    }
}
