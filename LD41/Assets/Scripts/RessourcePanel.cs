using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcePanel : MonoBehaviour {

    public GameObject woodLabel = null;
    public GameObject goldLabel = null;
    public GameObject stoneLabel = null;
    public GameObject ironLabel = null;
    public GameObject foodLabel = null;

    public GameObject happinessLabel = null;
    public GameObject hungerLabel = null;
    public GameObject fertilityLabel = null;
    public GameObject militaryLabel = null;
    public GameObject populationLabel = null;
    public GameObject maxPopulationLabel = null;

    private World attachedWord = null;

    // Use this for initialization
    void Start () {
        GameObject attachedWord_GO = GameObject.Find("World");
        attachedWord = attachedWord_GO.GetComponent<World>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!woodLabel || !goldLabel || !foodLabel || !ironLabel || !stoneLabel)
            return;
        if (!happinessLabel || !hungerLabel || !fertilityLabel || !militaryLabel || !populationLabel || !maxPopulationLabel)
            return;

        if (!!attachedWord)
        {
            // Wood
            Text text = woodLabel.GetComponent<Text>();
            text.text =
                attachedWord.ressource_table[Assets.Scripts.Ressource.TYPE.WOOD].ToString();

            // Iron
            text = ironLabel.GetComponent<Text>();
            text.text =
                attachedWord.ressource_table[Assets.Scripts.Ressource.TYPE.IRON].ToString();

            // Food
            text = foodLabel.GetComponent<Text>();
            text.text =
                attachedWord.ressource_table[Assets.Scripts.Ressource.TYPE.FOOD].ToString();

            // Gold
            text = goldLabel.GetComponent<Text>();
            text.text =
                attachedWord.ressource_table[Assets.Scripts.Ressource.TYPE.GOLD].ToString();

            // Stone
            text = stoneLabel.GetComponent<Text>();
            text.text =
                attachedWord.ressource_table[Assets.Scripts.Ressource.TYPE.STONE].ToString();

            /////////////////////////////////////////////////////////////////////////////////
            // happinessLabel
            text = happinessLabel.GetComponent<Text>();
            text.text =
                attachedWord.happiness.ToString();

            // hungerLabel
            text = hungerLabel.GetComponent<Text>();
            text.text =
                attachedWord.hunger.ToString();

            // fertilityLabel
            text = fertilityLabel.GetComponent<Text>();
            text.text =
                attachedWord.fertility.ToString();

            // militaryLabel
            text = militaryLabel.GetComponent<Text>();
            text.text =
                attachedWord.military.ToString();

            // populationLabel
            text = populationLabel.GetComponent<Text>();
            text.text =
                attachedWord.population.ToString();

            // maxPopulationLabel
            text = maxPopulationLabel.GetComponent<Text>();
            text.text =
                attachedWord.max_villagers.ToString();
        }
	}
}
