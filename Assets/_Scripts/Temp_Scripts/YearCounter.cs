using TMPro;
using UnityEngine;

public class YearCounter : MonoBehaviour
{
    public TextMeshProUGUI yearText;
    public float waitTime;

    private bool IsCounting;
    private int year = 2000;
    private float timer;
    public AudioSource oddNumber;
    public AudioSource evenNumber;
	
	private void Update()
    {
        yearText.text = year.ToString();
        timer += Time.deltaTime;

        if (IsCounting && timer >= waitTime && year < 2070)
        {
            year++;
            //Even numbers
			if (year %2 == 0)
			{
                EvenNumber();
			}
            //Odd numbers
			else
			{
                OddNumber();
			}
            timer = 0;
            if(waitTime > 0.05)
            {
                waitTime -= 0.03f;
            }

        }
    }
    public void StartCounting()
    {
        IsCounting = true;
    }
    private void OddNumber()
	{
        oddNumber.Play();
	}

    private void EvenNumber()
	{
        evenNumber.Play();
	}
}
