using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;
    [SerializeField] private GameObject background;
    
	public static UI instance; 

	private void Awake()
	{
		instance = this;
		ClearSubtitle();
	}


	public void SetSubtitle(string subtitle, float delay)
	{
		subtitleText.text = subtitle;
		background.SetActive(true);

		StartCoroutine(ClearAfterSeconds(delay));
	}

	public void ClearSubtitle()
	{
		subtitleText.text = "";
		background.SetActive(false);
	}

	private IEnumerator ClearAfterSeconds(float delay)
	{
		yield return new WaitForSeconds(delay);
		ClearSubtitle();
	}
    
}
