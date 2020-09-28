using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    #region Variables
    TurnController turnController;
    GameObject selectingMaster;
    SelectingUnits selectingUnits;
    UnitInformation unitInfo;
    bool targetingReady;

    [Header("Range")]
    static float smallRange = 4;
    static float mediumRange = 6;
    static float bigRange = 8;
    float unitRange;
    Collider[] enemyInRange;
    public GameObject _rangeIndicator;

    [Header("Interacting")]
    bool canShoot;
    UnitInformation targetInfo;
    Vector3 cameraDirection = new Vector3(-0.5f, -0.7f, 0.5f);
    public Transform _cameraDirection;
    Transform cameraPos;
    float damage = 1;
    Collider selfCollider;
    public bool hasGone;

    [Header("Damage")]
    int effectiveDamage = 4;
    int averageDamage = 2;
    int weakDamage = 1;

    [Header("UI")]
    public Button startRange;
    public Button fireButton;
    public Button cancelButton;
    #endregion
    void Start()
    {
        hasGone = false;
        turnController = FindObjectOfType<TurnController>();
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

            var camPosition = cameraPos.transform.position;
            var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(mouseWorldPoint, cameraDirection, Color.black, 10);
            if (Physics.Raycast(mouseWorldPoint, cameraDirection, out hit, 1000) && canShoot)
            {
                Debug.DrawRay(mouseWorldPoint, cameraDirection, Color.white, 10);
                if (targetInfo != null)
                {
                    targetInfo.DeselectUnit();
                    targetInfo = null;
                }
                targetInfo = hit.transform.GetComponent<UnitInformation>();
                if (targetInfo.transform.position != this.transform.position && targetInfo._team != unitInfo._team)
                {
                    targetInfo.SelectUnit();
                    
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
        startRange.gameObject.SetActive(false);
    }
    public void FireMaLazer()
    {
        DamageTargetUnit(unitInfo._shipType);
        hasGone = true;
        FinishFiring();
        //turnController.ChangeTurn();
        //ResetTargeting();
    }
    void DamageTargetUnit(int UnitTypeID)
    {
        switch (UnitTypeID)
        {
            case 1:
                switch (targetInfo._shipType)
                {
                    case 1:
                        targetInfo.Health -= averageDamage;
                        break;
                    case 2:
                        targetInfo.Health -= weakDamage;
                        break;
                    case 3:
                        targetInfo.Health -= effectiveDamage;
                        break;
                }
                break;
            case 2:
                switch (targetInfo._shipType)
                {
                    case 1:
                        targetInfo.Health -= effectiveDamage;
                        break;
                    case 2:
                        targetInfo.Health -= averageDamage;
                        break;
                    case 3:
                        targetInfo.Health -= weakDamage;
                        break;
                }
                break;
            case 3:
                switch (targetInfo._shipType)
                {
                    case 1:
                        targetInfo.Health -= weakDamage;
                        break;
                    case 2:
                        targetInfo.Health -= effectiveDamage;
                        break;
                    case 3:
                        targetInfo.Health -= averageDamage;
                        break;
                }
                break;
        }
    }
    void FinishFiring()
    {
        _rangeIndicator.SetActive(false);
        TurnUi(false);
        fireButton.gameObject.SetActive(false);
        if (targetInfo != null)
        {
            targetInfo.DeselectUnit();
            targetInfo = null; 
        }
        canShoot = false;
        selectingUnits.ResetSelecting();
    }
    public void ResetTargeting()
    {
        Fighting fighting = GetComponent<Fighting>();
        selfCollider.enabled = true;
        targetingReady = false;
        canShoot = false;
        enemyInRange = null;
        FinishFiring();
        hasGone = false;
        selectingUnits.ResetSelecting();
    }
}
