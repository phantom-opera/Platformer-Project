using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNewScene : MonoBehaviour
{
	[SerializeField] public TextMeshProUGUI cherryScore;
	int finalScore;

	void Start()
	{
		finalScore = PlayerPrefs.GetInt("CurrentScore", 0);
		cherryScore.text = finalScore.ToString();
	}
}
