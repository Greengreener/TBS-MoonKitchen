using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWorker : MonoBehaviour
{
    protected TurnController turnController;
    protected UnitCanvasHolder unitCanvasHolder;
    protected GameObject selectingMaster;
    protected SelectingUnits selectingUnits;

    void Start()
    {
        turnController = FindObjectOfType<TurnController>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<SelectingUnits>();
    }
}