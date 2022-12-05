using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class FadeToBlack : MonoBehaviour
{

    public GameObject blackOutSquare;
    public GameObject deathText;

    

    

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            StartCoroutine(FadeBlackOutSquare());
            Debug.LogError("FadeTrue");
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            StartCoroutine(FadeBlackOutSquare(false));
            Debug.LogError("FadeFalse");

        }
    }


    public void StartFade()
    {
        StartCoroutine(FadeBlackOutSquare());

    }

    public void StopFade()
    {
        StartCoroutine(FadeBlackOutSquare(false));

    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount; 

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime); 

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                Invoke("ActivateText", 1);
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                deathText.SetActive(false);
                yield return null;

            }
        }
    }


    private void ActivateText()
    {
        deathText.SetActive(true);
    }

}
