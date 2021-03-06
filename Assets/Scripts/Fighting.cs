﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : WorldWorker
{
    #region Variables
    UnitInformation unitInfo;
    bool targetingReady;

    [Header("Range")]
    static float smallRange = 4;
    static float mediumRange = 8;
    static float bigRange = 12;
    float unitRange;
    public Collider[] enemyInRange;
    public GameObject _rangeIndicator;

    [Header("Interacting")]
    bool canShoot;
    public UnitInformation targetInfo;
    Vector3 cameraDirection = new Vector3(-0.5f, -0.7f, 0.5f);
    public Transform _cameraDirection;
    Transform cameraPos;
    float damage = 1;
    Collider selfCollider;
    public bool hasGone;

    //!
    //?
    //todo
    //*
    //

    [Header("Damage")]
    int effectiveDamage = 4;
    int averageDamage = 2;
    int weakDamage = 1;
    #endregion
    void Start()
    {
        #region SetSpecial
        turnController = FindObjectOfType<TurnController>();
        unitCanvasHolder = GameObject.FindGameObjectWithTag("UnitCanvas").GetComponent<UnitCanvasHolder>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<NewSelectingUnit>();
        #endregion
        hasGone = false;
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
        selfCollider = GetComponent<Collider>();
        mainCamera = Camera.main;
    }
    public void StartTargeting()
    {
        targetingReady = true;
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
                    turnController.selectedShowHealth(targetInfo);
                    unitCanvasHolder.FireButtonActive(true);
                }
            }
        }
    }
    public void GetEnemiesInRange()
    {
        selfCollider.enabled = false;
        _rangeIndicator.SetActive(true);
        _rangeIndicator.transform.localScale = new Vector3(unitRange * 2, 0.01f, unitRange * 2);
        _rangeIndicator.transform.position = this.transform.position + ((cameraDirection * 10) - cameraDirection);
        var thisRotation = this.transform.rotation;
        var cameraRotation = mainCamera.transform.rotation;
        //print(_rangeIndicator.transform.rotation);
        _rangeIndicator.transform.rotation = new Quaternion(-0.4f, -0.4f, -0.1f, 0.9f);
        enemyInRange = Physics.OverlapSphere(gameObject.transform.position, unitRange);
        for (int i = 0; i < enemyInRange.Length; i++)
            if (enemyInRange != null)
            {
                canShoot = true;
            }
            else { canShoot = false; }
    }
    public void FireMaLazer()
    {
        DamageTargetUnit(unitInfo._shipType);
        hasGone = true;
        turnController.UpdateSelectedHealth(targetInfo);
        FinishFiring();
    }
    public void GetAiEnemiesInRange()
    {
        selfCollider.enabled = false;
        enemyInRange = Physics.OverlapSphere(gameObject.transform.position, unitRange);
    }
    public void FireAiLazer()
    {
        if (targetInfo._team != unitInfo._team)
        {
            DamageTargetUnit(unitInfo._shipType);
            hasGone = true;
        }
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
        if (targetInfo != null)
        {
            targetInfo.DeselectUnit();
            targetInfo = null;
        }
        canShoot = false;
    }
    public void ResetTargeting()
    {
        turnController.ResetShowHealth();
        Fighting fighting = GetComponent<Fighting>();
        selfCollider.enabled = true;
        targetingReady = false;
        canShoot = false;
        enemyInRange = null;
        FinishFiring();
        hasGone = false;
    }
}