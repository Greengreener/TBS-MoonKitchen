using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTurn : WorldWorker
{
    public List<UnitInformation> _AiTeam;
    void Start()
    {
        #region SetSpecial
        turnController = FindObjectOfType<TurnController>();
        unitCanvasHolder = GameObject.FindGameObjectWithTag("UnitCanvas").GetComponent<UnitCanvasHolder>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<NewSelectingUnit>();
        mainCamera = Camera.main;
        #endregion

        UnitInformation[] temp = FindObjectsOfType<UnitInformation>();

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i]._team == "Red")
            {
                _AiTeam.Add(temp[i]);
            }
        }
    }
    void Update()
    {
        if (turnController.Turn == 1)
        {
            FireAtClosestEnemy();
        }
    }
    void FireAtClosestEnemy()
    {
        for (int i = 0; i < _AiTeam.Count; i++)
        {
            Fighting Temp = GetFightingForFiring(_AiTeam[i]);
            Temp.GetAiEnemiesInRange();
            Temp.targetInfo = Temp.enemyInRange[0].gameObject.GetComponent<UnitInformation>();
            if (Temp.enemyInRange[0].gameObject.GetComponent<UnitInformation>() != null)
            {
                if (Temp.hasGone == false)
                {
                    Temp.FireAiLazer();
                }
            }
            Temp.ResetTargeting();
        }

        turnController.ChangeTurn();
    }
    Fighting GetFightingForFiring(UnitInformation currentUnitInfo)
    {
        Fighting temp = currentUnitInfo.gameObject.GetComponent<Fighting>();
        return temp;
    }

}
