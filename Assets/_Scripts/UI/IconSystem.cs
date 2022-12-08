using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class IconSystem : MonoBehaviour
{
    public static IconSystem instance;
    public TextMeshProUGUI responseText; 

    private void Awake()
    {
        instance = this;
    }
    public void SmashIce()
    {
        TextEnabled(true);
        responseText.text = "Press LMB to smash the ice";
    }
    public void RoarIcicle()
    {
        TextEnabled(true);
        responseText.text = "Press E to roar \nto make the icicle dropp";
    }
    public void PickUpFish()
    {
        TextEnabled(true);
        responseText.text = "Press LMB to pick up the fish";
    }
    public void FishInMouth()
    {
        TextEnabled(true);
        responseText.text = "Press R to drop the fish \nPress F to eat";
    }
    public void CustomText(string text)
    {
        TextEnabled(true);
        responseText.text = text;
    }
    public void TextEnabled(bool state)
    {
        responseText.enabled = state;
    }


}
