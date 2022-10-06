using UnityEngine;
using TMPro;

public class FishAreaManager : MonoBehaviour
{
    public GameObject fishCanvas;
    public TextMeshProUGUI couter;
    public Animator fish2Anim;

    private int fishEaten = 0;

    private void Start()
    {
        fishCanvas.SetActive(false);
        InvokeRepeating("Fish2Animator", 0f, 0.5f);
    }

    public void AddOneFish()
    {
        fishEaten++;
        couter.text = fishEaten.ToString() + "/3";
        if(fishEaten >= 3)
        {
            Invoke("Done", 1f); 
        }
    }
    public void StartFishGame()
    {
        fishCanvas.SetActive(true);
    }
    private void Done()
    {
        fishCanvas.SetActive(false);
    }
    private void Fish2Animator()
    {
        if(fish2Anim != null)
        {
            fish2Anim.SetInteger("HoleNumber", Random.Range(1, 5));
        }
        else
        {
            CancelInvoke();
        }
        
    }

}
