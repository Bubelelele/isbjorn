using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ShowPath : MonoBehaviour
{
    private ClosestFoodFinder closestFoodFinder;
    private Vector3 closestFoodLoc;
    //public GameObject closestFoodLoc;
    public bool hunterSense;

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

    [SerializeField]
    private GameObject myLineRenderer;

    private NavMeshTriangulation triangulation;
    private Coroutine DrawpathCoroutine;


    private void Start()
    {
        closestFoodFinder = GameObject.Find("Player").GetComponent<ClosestFoodFinder>();
        triangulation = NavMesh.CalculateTriangulation();

    }

    private void Update()
    {
        closestFoodLoc = closestFoodFinder.closestFoodLocation;

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (!hunterSense)
            {
                hunterSense = true;
                FindFood();

            }



        }

        if (!hunterSense)
        {
            
            //    StopCoroutine(DrawPathToFood());
            myLineRenderer.SetActive(false); 
            
        }

        
    }


    public void FindFood()
    {
        if (hunterSense)
        {
            if (DrawpathCoroutine != null)
            {
                StopCoroutine(DrawpathCoroutine);
            }
            DrawpathCoroutine = StartCoroutine(DrawPathToFood());

            Invoke("TurnOffSense", smellDuration);
        }
        
    }

    void TurnOffSense()
    {
        hunterSense = false;
        myLineRenderer.SetActive(false); 
    }

    private IEnumerator DrawPathToFood()
    {
        WaitForSeconds wait = new WaitForSeconds(pathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();
        
        if (hunterSense)
        {
            myLineRenderer.SetActive(true); 
            while (closestFoodLoc != null)
            {
                if (NavMesh.CalculatePath(player.position, closestFoodLoc, NavMesh.AllAreas, path))
                {
                    linePath.positionCount = path.corners.Length;

                    for (int i = 0; i < path.corners.Length; i++)
                    {
                        linePath.SetPosition(i, path.corners[i] + Vector3.up * pathHeightOffset);
                    }
                }
                //else
                //{
                //    Debug.LogError($"Unable to calculate a path on the NavMesh between {playerOffset.position} and {closestFoodLoc}!");
                //}

                yield return wait;
            }
        }
        
    }
}
