using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ShowPath : MonoBehaviour
{
    public GameObject myLineRenderer;
    public bool hunterSense;


    private ClosestFoodFinder closestFoodFinder;
    private Vector3 closestFoodLoc;
    private Transform pathStartTransform;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private LineRenderer linePath;

    [SerializeField]
    private float pathHeightOffset = 1.25f;

    [SerializeField]
    private float pathUpdateSpeed = 0.25f;
    
    [SerializeField]
    private float smellDuration = 5f;


    private NavMeshTriangulation triangulation;
    private Coroutine DrawpathCoroutine;

    private void Start()
    {
        closestFoodFinder = GameObject.Find("Player").GetComponent<ClosestFoodFinder>();
        triangulation = NavMesh.CalculateTriangulation();
        pathStartTransform = player.Find("Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/Jaw2");
    }

    private void Update()
    {
        closestFoodLoc = closestFoodFinder.closestFoodLocation;
    }

    public void FindFood()
    {
            if (DrawpathCoroutine != null)
            {
                StopCoroutine(DrawpathCoroutine);
            }
            DrawpathCoroutine = StartCoroutine(DrawPathToFood());

            Invoke(nameof(TurnOffSense), smellDuration);
    }

    private void TurnOffSense()
    {
        hunterSense = false;
        myLineRenderer.SetActive(false); 
    }

    private IEnumerator DrawPathToFood()
    {
        WaitForSeconds wait = new WaitForSeconds(pathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();
        
            myLineRenderer.SetActive(true); 
                if (NavMesh.CalculatePath(pathStartTransform.position, closestFoodLoc, NavMesh.AllAreas, path))
                {
                    linePath.positionCount = path.corners.Length;

                    for (int i = 0; i < path.corners.Length; i++)
                    {
                        linePath.SetPosition(i, path.corners[i] + Vector3.up * pathHeightOffset);
                    }
                }
                yield return wait;
    }
}
