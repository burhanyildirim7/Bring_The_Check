using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public  bool isContinue;

    public int lastX;  // puanların çarpılacağı en son x değerii..

    [HideInInspector] public int score;

    [HideInInspector] public List<GameObject> disabledObjects = new List<GameObject>();

	private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(this);
	}

    public void IncreaseScore()
	{
        score += 10;
        UIController.instance.SetScoreText();
    }

    public void DecreaseScore()
	{
        if(score >= 10)
		{
            score -= 10;
            UIController.instance.SetScoreText();
        }
        
    }

    public void ActivateAllDisabledObjects()
    {
        for (int i = 0; i < disabledObjects.Count; i++)
        {
            disabledObjects[i].SetActive(true);
            disabledObjects[i].GetComponent<Collider>().enabled = true;
            if (!disabledObjects[i].transform.CompareTag("final"))
            {
                disabledObjects[i].GetComponent<Renderer>().enabled = true;
            }
        }
    }

    // bir level bitip di�erine giderken bunlar �a�r�lmadan �nce temizlenecek...
    public void ClearLists()
    {
        disabledObjects.Clear();
    }

    public void FinalScoreMultiply()
    {
        score = score * lastX;
        PlayerPrefs.SetInt("total", GameManager.instance.score + PlayerPrefs.GetInt("total"));
    }

    //public void FinalScoreMultiply(string str)
    //{
    //    if (str == "x10") score *= 10;
    //    else if (str == "x9") score *= 9;
    //    else if (str == "x8") score *= 8;
    //    else if (str == "x7") score *= 7;
    //    else if (str == "x6") score *= 6;
    //    else if (str == "x5") score *= 5;
    //    else if (str == "x4") score *= 4;
    //    else if (str == "x3") score *= 3;
    //    else if (str == "x2") score *= 2;
    //    UIController.instance.SetScoreText();
    //}





}
