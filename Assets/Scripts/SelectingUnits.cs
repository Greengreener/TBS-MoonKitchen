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
    bool canSelect;
    #endregion

    private void Start()
    {
        turnController = FindObjectOfType<TurnController>();
        canSelect = true;
        cameraPos = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSelect)
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
                    if (launchUnit != null)
                    {
                        launchUnit.StartTargeting();
                        unitInfo.healthText.gameObject.SetActive(true);
                        canSelect = false;
                    }
                    else { return; }
                }
                if (unitInfo._team == "Red" && turnController.Turn == 1 && launchUnit.hasGone == false)
                {
                    if (launchUnit != null)
                    {
                        launchUnit.StartTargeting();
                        unitInfo.healthText.gameObject.SetActive(true);
                        canSelect = false;
                    }
                    else { return; }
                }
                else
                {
                    //Debug.LogError($"There is no 'Fighting' Component attached to '{hit.transform.name}'");
                }

                return;
            }
        }
    }
    public void ResetSelecting()
    {
        canSelect = true;
    }
}