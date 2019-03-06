using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCustomization : MonoBehaviour {

    enum BuyState
    {
        Locked,
        Unlocked,
        Equipped
    }

    public Text totalStarText;

    public int cost = 1; // amount it cost to buy this customization

    private BuyState state = BuyState.Equipped;

    private GameObject unlock;
    private GameObject costTxt;
    private GameObject star;

    private GameObject equip;

    private Image ball;

    private int totalStars;
    private string currentCustomization;

    private void Awake()
    {
        // Set up all references to child UI parts
        unlock = gameObject.transform.GetChild(0).gameObject;
        costTxt = gameObject.transform.GetChild(2).gameObject;
        costTxt.GetComponent<Text>().text = "" + cost;
        star = gameObject.transform.GetChild(3).gameObject;
        equip = gameObject.transform.GetChild(4).gameObject;
        ball = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();

        // Set White to be default if first time
        if (name == "White" && PlayerPrefs.GetString("CurrentBall") == "")
        {
            PlayerPrefs.SetInt("WhiteBuyState", 2);
            unlock.SetActive(false);
            costTxt.SetActive(false);
            star.SetActive(false);

            equip.SetActive(true);
            equip.GetComponent<Text>().text = "Equipped";

            Outline line = gameObject.AddComponent<Outline>();
            line.effectDistance = new Vector2(3, -3);
            line.effectColor = Color.blue;
            line.useGraphicAlpha = false;
        }
    }

    // Use this for initialization
    void Start () {
        // Get the current states through playerprefs
        totalStars = PlayerPrefs.GetInt("TotalStars", 0);
        currentCustomization = PlayerPrefs.GetString("CurrentBall", "White");
        string key = gameObject.name + "BuyState";
        state = (BuyState)PlayerPrefs.GetInt(key, 0);

        totalStarText.text = "x " + totalStars;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        currentCustomization = PlayerPrefs.GetString("CurrentBall", "White");

        switch (state)
        {
            case BuyState.Equipped:
                break;
            case BuyState.Unlocked:
                {
                    // Set this one to be selected
                    state = BuyState.Equipped;
                    equip.GetComponent<Text>().text = "Equipped";

                    // Unselect the currently selected one
                    GameObject.Find(currentCustomization).GetComponent<BallCustomization>().UnEquip();

                    // Set the customization to be the new selection
                    currentCustomization = gameObject.name;
                    PlayerPrefs.SetString("CurrentBall", currentCustomization);

                    Outline line = gameObject.AddComponent<Outline>();
                    line.effectDistance = new Vector2(3, -3);
                    line.effectColor = Color.blue;
                    line.useGraphicAlpha = false;
                }
                
                break;
            case BuyState.Locked:
                Debug.Log("buying dis");
                break;
        }
    }

    public void UnEquip()
    {
        state = BuyState.Unlocked;
        equip.GetComponent<Text>().text = "Equip";

        Destroy(gameObject.GetComponent<Outline>());
    }


}
