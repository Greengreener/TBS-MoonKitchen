using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingUnits : MonoBehaviour
{
    #region Variables
    TurnController turnController;
    UnitInformation unitInfo;
    Fighting launchUnit;
    public Transform _cameraDirection;
    Transform cameraPos;
    bool alreadyOpen;
    #endregion

    private void Start()
    {
        turnController = FindObjectOfType<TurnController>();
        cameraPos = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            var camDirectionPosition = _cameraDirection.transform.position;
            var camPosition = cameraPos.transform.position;
            var rayDirection = (camDirectionPosition - camPosition) * 100f;
            var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.DrawRay(mouseWorldPoint, rayDirection, Color.black, 10);
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
                            launchUnit.StartTargeting();
                            unitInfo.healthText.gameObject.SetActive(true);
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
                        launchUnit.StartTargeting();
                        unitInfo.healthText.gameObject.SetActive(true);
                    }
                    else 
                    { 
                        ResetSelecting(hit);
                    }
                }
                else
                {
                    //Debug.LogError($"There is no 'Fighting' Component attached to '{hit.transform.name}'");
                }

                return;
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
            unitInfo.healthText.gameObject.SetActive(true);
        }
    }
    public void EndTargetingAndStuff()
    {
        alreadyOpen = false;
    }
}