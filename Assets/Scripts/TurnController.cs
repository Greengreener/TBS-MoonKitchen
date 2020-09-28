using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public int Turn { get; set; }
    public Text _turnDisplay;
    public Image _displayPanel;
    public UnitInformation[] units;
   
    void Start()
    {
        Turn = Random.Range(0, 2);
        print(Turn);
        switch (Turn)
        {
            case 0:
                _turnDisplay.text = "The turn is Blue";
                _turnDisplay.color = Color.white;
                _displayPanel.color = Color.blue + new Color(0, 0, 0, -10);
                break;
            case 1:
                _turnDisplay.text = "The turn is Red";
                _turnDisplay.color = Color.white;
                _displayPanel.color = Color.red + new Color(0,0,0,-10);
                break;
        }
        units = FindObjectsOfType<UnitInformation>();
    }
    private void Update()
    {
        switch (Turn)
        {
            case 0:
                _turnDisplay.text = "The turn is Blue";
                _turnDisplay.color = Color.white;
                _displayPanel.color = Color.blue + new Color(0, 0, 0, 10);
                break;
            case 1:
                _turnDisplay.text = "The turn is Red";
                _turnDisplay.color = Color.white;
                _displayPanel.color = Color.red + new Color(0, 0, 0, 10);
                break;
        }
    }
    public void ChangeTurn()
    {
        Turn++;
        if (Turn >= 2)
        {
            Turn = 0;
        }
        print("Turn = " + Turn);
        for (int i = 0; i < units.Length; i++)
        {
            units[i].fighting.ResetTargeting();
        }
    }
}
