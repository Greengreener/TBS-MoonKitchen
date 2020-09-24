using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingUnits : MonoBehaviour
{
    public Fighting launchUnit;
    public Transform _cameraDirection;
    Transform cameraPos;
    bool canSelect;
    
    private void Start()
    {
        canSelect = true;
        cameraPos = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&canSelect)
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
                launchUnit = hit.transform.GetComponent<Fighting>();
                if (launchUnit != null)
                {
                    launchUnit.StartTargeting();
                    canSelect = false;
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