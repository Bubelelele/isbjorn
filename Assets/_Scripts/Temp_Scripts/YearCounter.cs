using TMPro;
using UnityEngine;

public class YearCounter : MonoBehaviour
{
    public TextMeshProUGUI yearText;
    public float waitTime;

    public bool IsCounting;
    private int year = 2000;
    private float timer;
    private void Update()
    {
        yearText.text = year.ToString();
        timer += Time.deltaTime;

        if (IsCounting && timer >= waitTime && year < 2070)
        {
            year++;
            timer = 0;
            if(waitTime > 0.05)
            {
                waitTime -= 0.03f;
            }

        }
    }
    private void StartCounting()
    {
        IsCounting = true;
    }
}
