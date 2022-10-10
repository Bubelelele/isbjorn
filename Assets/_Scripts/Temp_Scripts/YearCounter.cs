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
    private int count;
	
	private void Update()
    {
        yearText.text = year.ToString();
        timer += Time.deltaTime;

        if (IsCounting && timer >= waitTime && year < 2070)
        {
			if (year >= 2020)
			{
                year += 2;
			}
			else if (year >= 2050)
			{
                year += 5;
			}
			else
			{
                year++;
			}
            count++;
           
            //Even numbers
			if (count %2 == 0)
			{
                EvenNumber();
			}
            //Odd numbers
			else
			{
                OddNumber();
			}
            timer = 0;
            if(waitTime > 0.045)
            {
                waitTime -= 0.015f;
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
