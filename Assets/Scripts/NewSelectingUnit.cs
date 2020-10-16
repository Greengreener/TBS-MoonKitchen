using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSelectingUnit : WorldWorker
{
    #region Variables
    public Transform _cameraDirection;
    Transform cameraPos;
    bool alreadyOpen;
    UnitInformation unitInfo;
    Fighting launchUnit;
    #endregion

    private void Start()
    {
        #region SetSpecial
        turnController = FindObjectOfType<TurnController>();
        unitCanvasHolder = GameObject.FindGameObjectWithTag("UnitCanvas").GetComponent<UnitCanvasHolder>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<NewSelectingUnit>();
        #endregion
        cameraPos = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var camDirectionPosition = _cameraDirection.transform.position;
            var camPosition = cameraPos.transform.position;
            var rayDirection = (camDirectionPosition - camPosition) * 100f;
            var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Physics.Raycast(mouseWorldPoint, rayDirection, out hit, 1000))
            {
                Debug.DrawRay(mouseWorldPoint, rayDirection, Color.white, 10);
                unitInfo = hit.transform.GetComponent<UnitInformation>();
                launchUnit = hit.transform.GetComponent<Fighting>();


                if (unitInfo._team == "Blue" && turnController.Turn == 0 && launchUnit.hasGone == false)
                {
                    if (!alreadyOpen)
                    {
                        turnController.StartShowHealth(unitInfo);
                        if (launchUnit != null)
                        {
                            unitCanvasHolder.UnitCanvasButtonActive(true, false);
                            unitCanvasHolder.SetUnitInfo(unitInfo, launchUnit);
                            LocateAndSetCanvas(launchUnit);
                            launchUnit.StartTargeting();
                        }
                    }
                    else
                    {
                        ResetSelecting(hit);
                    }
                }
                if (unitInfo._team == "Red" && turnController.Turn == 1 && launchUnit.hasGone == false)
                {
                    turnController.StartShowHealth(unitInfo);
                    if (launchUnit != null)
                    {
                        unitCanvasHolder.UnitCanvasButtonActive(true, false);
                        unitCanvasHolder.SetUnitInfo(unitInfo, launchUnit);
                        LocateAndSetCanvas(launchUnit);
                        launchUnit.StartTargeting();
                    }
                    else
                    {
                        ResetSelecting(hit);
                    }
                }
            }
        }
    }
    public void ResetSelecting(RaycastHit raycastHit)
    {
        unitInfo = raycastHit.transform.GetComponent<UnitInformation>();
        launchUnit = raycastHit.transform.GetComponent<Fighting>();
        turnController.StartShowHealth(unitInfo);
        if (launchUnit != null)
        {
            launchUnit.StartTargeting();
        }
    }
    void LocateAndSetCanvas(Fighting launchUnit)
    {
        unitCanvasHolder.gameObject.transform.position = launchUnit.transform.position;
    }
}