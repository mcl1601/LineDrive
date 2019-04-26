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

    public GameObject confirmWin;

    public int cost = 1; // amount it cost to buy this customization

    private int id;

    private BuyState state;

    // UI pieces to turn off once unlocked/purchased
    private GameObject unlock;
    private GameObject costTxt;
    private GameObject star;

    // UI piece to turn on once unlocked/purchased
    private GameObject equip;

    private Image ball;
    private CustomizationManager customizationManager;

    private int totalStars;
    private string currentCustomization;

    private void Awake()
    {
        // Set up ref to the confirmation window
        //confirmWin = transform.parent.parent.GetChild(4).gameObject;

        // Set up all references to child UI parts
        unlock = gameObject.transform.GetChild(0).gameObject;
        costTxt = gameObject.transform.GetChild(2).gameObject;
        costTxt.GetComponent<Text>().text = "" + cost;
        star = gameObject.transform.GetChild(3).gameObject;
        equip = gameObject.transform.GetChild(4).gameObject;
        ball = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        // Get the current states through playerprefs
        totalStars = PlayerPrefs.GetInt("TotalStars", 0);
        currentCustomization = PlayerPrefs.GetString("CurrentBall", "White");
        string key = gameObject.name + "BuyState";
        state = (BuyState)PlayerPrefs.GetInt(key, 0);
        customizationManager = GameObject.Find("CustomizationManager").GetComponent<CustomizationManager>();
        id = transform.GetSiblingIndex();

        if (gameObject.name == "White" && currentCustomization != "White")
        {
            state = BuyState.Unlocked;
            Unlock();
        }
        else if(gameObject.name == "White")
        {
            state = BuyState.Equipped;
            Unlock();
            Equip();
        }

        if (state == BuyState.Equipped)
        {
            Unlock();
            Equip();
        }

        if(state == BuyState.Unlocked)
        {
            Unlock();
        }

        totalStarText.text = "x " + totalStars;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        totalStars = PlayerPrefs.GetInt("TotalStars");

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
                    if(gameObject.name.Contains("Trail"))
                    {
                        GameObject.Find(PlayerPrefs.GetString("CurrentTrail")).GetComponent<BallCustomization>().UnEquip();
                    }
                    else
                    {
                        GameObject.Find(PlayerPrefs.GetString("CurrentBall")).GetComponent<BallCustomization>().UnEquip();
                    }

                    // Set the customization to be the new selection
                    Equip();
                }
                
                break;
            case BuyState.Locked:
                {
                    confirmWin.SetActive(true);
                    confirmWin.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = " = " + cost + " of ";
                    confirmWin.transform.GetChild(3).GetComponent<Text>().text = "Unlock for " + cost + " Stars?";
                    confirmWin.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                    confirmWin.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(Purchase);
                    confirmWin.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = ball.color;
                    if (cost > totalStars)
                    {
                        confirmWin.transform.GetChild(2).GetChild(1).GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        confirmWin.transform.GetChild(2).GetChild(1).GetComponent<Button>().interactable = true;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Equips this ball customization
    /// </summary>
    private void Equip()
    {
        equip.GetComponent<Text>().text = "Equipped";

        Outline line = gameObject.AddComponent<Outline>();
        line.effectDistance = new Vector2(3, -3);
        line.effectColor = Color.blue;
        line.useGraphicAlpha = false;

        if (gameObject.name.Contains("Trail"))
        {
            PlayerPrefs.SetInt("Trail", id);
            customizationManager.UpdateCurrentTrail(id);
        }
        else
        {
            PlayerPrefs.SetInt("Ball", id);
            customizationManager.UpdateCurrentBall(id);
        }

        // Set the customization to be the new selection
        if(gameObject.name.Contains("Trail"))
        {
            PlayerPrefs.SetString("CurrentTrail", gameObject.name);
        }
        else
        {
            PlayerPrefs.SetString("CurrentBall", gameObject.name);
        }
        

        state = BuyState.Equipped;
        string key = gameObject.name + "BuyState";
        PlayerPrefs.SetInt(key, (int)state);
    }

    /// <summary>
    /// Unequips this ball customization
    /// Used to make sure only one is equipped at a time
    /// </summary>
    public void UnEquip()
    {
        state = BuyState.Unlocked;
        equip.GetComponent<Text>().text = "Equip";
        string key = gameObject.name + "BuyState";
        PlayerPrefs.SetInt(key, (int)state);
        Destroy(gameObject.GetComponent<Outline>());
    }

    /// <summary>
    /// Unlock the ball customization by removing star cost count and unlock ui text
    /// SHow equip instead
    /// </summary>
    private void Unlock()
    {
        unlock.SetActive(false);
        costTxt.SetActive(false);
        star.SetActive(false);

        equip.SetActive(true);

        // Update UI
        UnEquip();
    }

    public void Purchase()
    {
        Unlock();

        // Unselect the currently selected one
        if (gameObject.name.Contains("Trail"))
        {
            GameObject.Find(PlayerPrefs.GetString("CurrentTrail")).GetComponent<BallCustomization>().UnEquip();
        }
        else
        {
            GameObject.Find(PlayerPrefs.GetString("CurrentBall")).GetComponent<BallCustomization>().UnEquip();
        }

        Equip();
        
        PlayerPrefs.SetInt("TotalStars", totalStars - cost);
        totalStars -= cost;
        totalStarText.text = "x " + totalStars;
        confirmWin.SetActive(false);

        state = BuyState.Equipped;
        string key = gameObject.name + "BuyState";
        PlayerPrefs.SetInt(key, (int)state);
    }
}
