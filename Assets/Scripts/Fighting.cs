using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    #region Variables
    UnitInformation unitInfo;
    [Header("Range")]
    static float smallRange = 2;
    static float mediumRange = 5;
    static float bigRange = 10;
    float unitRange;
    Collider[] enemyInRange;
    public GameObject _rangeIndicator;
    [Header("Interacting")]
    public bool canShoot;
    UnitInformation targetInfo;
    public Transform _cameraDirection;
    Transform cameraPos;
    float damage = 1;
    public Button fireButton;
    #endregion
    void Start()
    {
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
        cameraPos = Camera.main.transform;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), (_cameraDirection.transform.position - cameraPos.transform.position) * 100, Color.black, 10);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), _cameraDirection.transform.position - cameraPos.transform.position, out hit, 1000)&&canShoot)
            {
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), (_cameraDirection.transform.position - cameraPos.transform.position) * 100, Color.white, 10);
                targetInfo = hit.transform.GetComponent<UnitInformation>();
                Debug.Log("targetHealth = " + targetInfo.Health + " || targetType = " + targetInfo._shipType + " || targetTeam = " + targetInfo._team);
                fireButton.gameObject.SetActive(true);
            }
        }
    }
    public void GetEnemiesInRange()
    {
        _rangeIndicator.SetActive(true);
        _rangeIndicator.transform.localScale = new Vector3(unitRange * 2, 0.01f, unitRange * 2);
        enemyInRange = Physics.OverlapSphere(gameObject.transform.position, unitRange);
        #region DrawingLines
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(unitRange, 0, 0), Color.red, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, unitRange, 0), Color.green, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 0, unitRange), Color.blue, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(-unitRange, 0, 0), Color.red, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -unitRange, 0), Color.green, 100);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 0, -unitRange), Color.blue, 100);
        #endregion
        for (int i = 0; i < enemyInRange.Length; i++)
        {
            Debug.Log("Collider " + i + " pos = " + enemyInRange[i].gameObject.transform.position);
        }
        if (enemyInRange != null)
        { canShoot = true; }
        else { canShoot = false; }
    }
    public void FireMaLazer()
    {
        targetInfo.Health = targetInfo.Health - damage;
        ResetTargeting();
    }
    void ResetTargeting()
    {
        enemyInRange = null;
        targetInfo = null;
        _rangeIndicator.SetActive(false);
        fireButton.gameObject.SetActive(false);
    }
}
