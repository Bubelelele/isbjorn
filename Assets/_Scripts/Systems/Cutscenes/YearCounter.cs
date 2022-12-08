using TMPro;
using UnityEngine;

public class YearCounter : MonoBehaviour
{
    public TextMeshProUGUI yearText;
    public float waitTime;
    public float minWaitTime = 0.075f;
    public int year = 2000;

    private bool IsCounting;

    private int targetYear;
    private float timer;
    public AudioSource oddNumber;
    public AudioSource evenNumber;
    private int count;
	
	private void Update()
    {
        yearText.text = year.ToString();
        timer += Time.deltaTime;

        if (IsCounting && timer >= waitTime && year < targetYear)
        {
            year++;
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
            if(waitTime > minWaitTime)
            {
                waitTime -= 0.05f;
            }

        }
    }
    public void StartCounting(int desiredYear)
    {
        targetYear = desiredYear;
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
