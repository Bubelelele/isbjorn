using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FadeToBlack : MonoBehaviour
{
    public GameObject blackSquareImage;
    public GameObject deathText;

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
        Color objectColor = blackSquareImage.GetComponent<Image>().color;
        float fadeAmount; 

        if (fadeToBlack)
        {
            while (blackSquareImage.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime); 

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquareImage.GetComponent<Image>().color = objectColor;
                Invoke("ActivateText", 1);
                yield return null;
            }
        }
        else
        {
            while (blackSquareImage.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquareImage.GetComponent<Image>().color = objectColor;
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
