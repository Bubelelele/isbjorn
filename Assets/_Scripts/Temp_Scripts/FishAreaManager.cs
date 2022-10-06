using UnityEngine;
using TMPro;

public class FishAreaManager : MonoBehaviour
{
    public GameObject fishCanvas;
    public TextMeshProUGUI couter;

    private int fishEaten = 0;

    private void Start()
    {
        fishCanvas.SetActive(false);
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

}
