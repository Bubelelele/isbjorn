using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust = 5f;
    public Vector3 playerPos;
    



    private void Awake()
    {
        


    }

    #region Variables
    [SerializeField]
    private float fadeDelay = 10f;
    [SerializeField]
    private float currentAlpha = 1;
    [SerializeField]
    private float requiredAlpha = 0;
    [SerializeField]
    //private bool useMesh = false;
    //public bool destroyGameObject = false;

    private Renderer meshRenderer;
    private Color newColor = default;

    //SpriteRenderer spriteRenderer;
    #endregion

    #region Unity Base Methods

    private void OnEnable()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;


        rb = GetComponent<Rigidbody>();
        rb.AddForce(((transform.position - playerPos).normalized * thrust) ,ForceMode.Impulse);
        try
        {
            //meshRenderer = GetComponentInChildren<Renderer>();
            meshRenderer = GetComponent<Renderer>();

            newColor = meshRenderer.material.color;

            newColor.a = currentAlpha;

            StartCoroutine(FadeObject(currentAlpha, requiredAlpha, fadeDelay));
        }
        catch (System.Exception)
        {
            Debug.Log("No renderer found.");
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    #endregion

    #region User Methods
    private IEnumerator FadeObject(float currentAlpha, float requiredAlpha, float fadeTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            newColor.a = Mathf.Lerp(currentAlpha, requiredAlpha, t);

            meshRenderer.material.color = newColor;
            yield return null;
        }

        //gameObject.SetActive(false);
        Destroy(gameObject); 
    }
    #endregion


    

}
