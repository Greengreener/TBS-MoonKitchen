using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWorker : MonoBehaviour
{
    protected TurnController turnController;
    protected UnitCanvasHolder unitCanvasHolder;
    protected GameObject selectingMaster;
    protected NewSelectingUnit selectingUnits;

    void Start()
    {
        #region SetSpecial
        turnController = FindObjectOfType<TurnController>();
        unitCanvasHolder = GameObject.FindGameObjectWithTag("UnitCanvas").GetComponent<UnitCanvasHolder>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<NewSelectingUnit>();
        #endregion
    }
}