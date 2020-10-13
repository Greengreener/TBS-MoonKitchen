using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public int Turn { get; set; }
    Color red = Color.red;
    Color blue = Color.blue;
    Color white = Color.white;
    Color alphaDecrease = new Color(0, 0, 0, 10);
    Color alphaIncrease = new Color(0, 0, 0, 255);
    public Text _turnDisplay;
    public Image _displayPanel;
    UnitInformation[] units;
    public Image _attackingUnitHealthPanel;
    public Image _selectedUnitHealthPanel;
    public Text _attackingUnitHealthText;
    public Text _selectedUnitHealthText;

    void Start()
    {
        Turn = Random.Range(0, 2);
        print(Turn);
        ChangeUiBasedOnTurn(Turn);
        units = FindObjectsOfType<UnitInformation>();
    }
    private void Update()
    {
        ChangeUiBasedOnTurn(Turn);
    }
    void ChangeUiBasedOnTurn(int turnInput)
    {
        switch (turnInput)
        {
            case 0:
                _turnDisplay.text = "The turn is Blue";
                _turnDisplay.color = white;
                _displayPanel.color = blue + alphaDecrease;
                break;
            case 1:
                _turnDisplay.text = "The turn is Red";
                _turnDisplay.color = white;
                _displayPanel.color = red + alphaDecrease;
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
        _attackingUnitHealthPanel.color = white + alphaIncrease;
        _attackingUnitHealthText.text = "";
    }
    public void StartShowHealth(UnitInformation attackingUnit)
    {
        switch (attackingUnit._team)
        {
            case "Blue":
                _attackingUnitHealthPanel.color = blue;
                break;
            case "Red":
                _attackingUnitHealthPanel.color = red;
                break;
        }
        _attackingUnitHealthText.text = attackingUnit.health.ToString();
    }
    public void selectedShowHealth(UnitInformation selectedUnit)
    {
        switch (selectedUnit._team)
        {
            case "Blue":
                _selectedUnitHealthPanel.color = blue;
                break;
            case "Red":
                _selectedUnitHealthPanel.color = red;
                break;
        }
        _selectedUnitHealthText.text = selectedUnit.health.ToString();
    }
    public void UpdateSelectedHealth(UnitInformation selectedUnit)
    {
        _selectedUnitHealthText.text = selectedUnit.Health.ToString();
        if (selectedUnit.Health <= 0)
        {
            _selectedUnitHealthText.text = "Dead";
        }
    }
    public void ResetShowHealth()
    {
        _attackingUnitHealthText.text = "";
        _attackingUnitHealthPanel.color = white + alphaIncrease;
        _selectedUnitHealthText.text = "";
        _selectedUnitHealthPanel.color = white + alphaIncrease;
    }
}
