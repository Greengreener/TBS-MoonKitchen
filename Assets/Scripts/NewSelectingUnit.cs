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
    [Header("Unit Canvas")]
    GameObject unitCanvas;
    UnitCanvasHolder unitCanvasScript;
    #endregion

    private void Start()
    {
        turnController = FindObjectOfType<TurnController>();
        cameraPos = Camera.main.transform;
        unitCanvas = GameObject.FindGameObjectWithTag("UnitCanvas");
        unitCanvasScript = unitCanvas.GetComponent<UnitCanvasHolder>();
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
                unitCanvasScript.SetUnitInfo(unitInfo, launchUnit);

                if (unitInfo._team == "Blue" && turnController.Turn == 0 && launchUnit.hasGone == false)
                {
                    if (!alreadyOpen)
                    {
                        turnController.StartShowHealth(unitInfo);
                        if (launchUnit != null)
                        {
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
        unitCanvas.transform.position = launchUnit.transform.position;
    }
}