using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    #region Variables
    GameObject selectingMaster;
    SelectingUnits selectingUnits;
    UnitInformation unitInfo;
    bool targetingReady;
    [Header("Range")]
    
    static float smallRange = 2;
    static float mediumRange = 5;
    static float bigRange = 10;
    float unitRange;
    Collider[] enemyInRange;
    public GameObject _rangeIndicator;
    [Header("Interacting")]
    bool canShoot;
    UnitInformation targetInfo;
    public Transform _cameraDirection;
    Transform cameraPos;
    float damage = 1;
    
    Collider selfCollider;
    [Header("UI")]
    public Button startRange;
    public Button fireButton;
    public Button cancelButton;
    #endregion
    void Start()
    {
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<SelectingUnits>();
        unitInfo = GetComponent<UnitInformation>();
        switch (unitInfo._shipType)
        {
            case 1:
                unitRange = smallRange;
                break;
            case 2:
                unitRange = mediumRange;
                break;
            case 3:
                unitRange = bigRange;
                break;
        }
        TurnUi(false);
        cameraPos = Camera.main.transform;
        selfCollider = GetComponent<Collider>();
    }
    public void StartTargeting()
    {
        TurnUi(true);
        targetingReady = true;
    }
    void TurnUi(bool input)
    {
        startRange.gameObject.SetActive(input);
        cancelButton.gameObject.SetActive(input);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && targetingReady == true)
        {
            RaycastHit hit;
            var camDirectionPosition = _cameraDirection.transform.position;
            var camPosition = cameraPos.transform.position;
            var rayDirection = (camDirectionPosition - camPosition) * 100f;
            var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.DrawRay(mouseWorldPoint, rayDirection, Color.black, 10);
            if (Physics.Raycast(mouseWorldPoint, rayDirection, out hit, 1000) && canShoot)
            {
                Debug.DrawRay(mouseWorldPoint, rayDirection, Color.white, 10);
                targetInfo = hit.transform.GetComponent<UnitInformation>();
                
                if (targetInfo.transform.position != this.transform.position && targetInfo._team != unitInfo._team)
                {
                    targetInfo.SelectUnit();
                    startRange.gameObject.SetActive(false);
                    fireButton.gameObject.SetActive(true);
                }
            }
        }
    }
    public void GetEnemiesInRange()
    {
        selfCollider.enabled = false;
        _rangeIndicator.SetActive(true);
        _rangeIndicator.transform.localScale = new Vector3(unitRange * 2, 0.01f, unitRange * 2);
        _rangeIndicator.transform.localPosition = new Vector3(0, -0.5f, 0);
        enemyInRange = Physics.OverlapSphere(gameObject.transform.position, unitRange);
        #region DrawingLines
        /*Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(unitRange, 0, 0), Color.red, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, unitRange, 0), Color.green, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 0, unitRange), Color.blue, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(-unitRange, 0, 0), Color.red, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -unitRange, 0), Color.green, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 0, -unitRange), Color.blue, 100);*/
        #endregion
        for (int i = 0; i < enemyInRange.Length; i++)
            if (enemyInRange != null)
            {
                canShoot = true;
                
            }
        else { canShoot = false; }
    }
    public void FireMaLazer()
    {
        targetInfo.Health = targetInfo.Health - damage;
        ResetTargeting();
    }
    public void ResetTargeting()
    {
        Fighting fighting = GetComponent<Fighting>();
        selfCollider.enabled = true;
        targetingReady = false;
        canShoot = false;
        enemyInRange = null;
        if (targetInfo != null)
        {
            targetInfo.DeselectUnit();
            targetInfo = null;
        }
        _rangeIndicator.SetActive(false);
        TurnUi(false);
        fireButton.gameObject.SetActive(false);        
        selectingUnits.ResetSelecting();
    }
}
