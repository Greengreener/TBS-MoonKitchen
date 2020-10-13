using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCanvasHolder : MonoBehaviour
{
    UnitInformation unitInfo;
    Fighting unitFighting;
    public GameObject _aimButton;
    public GameObject _fireButton;
    public GameObject _cancelButton;

    void Start()
    {
        transform.position = new Vector3(10, 10, 10);
    }
    public void SetUnitInfo(UnitInformation launchUnitInfo, Fighting lauchUnitFighting)
    {
        unitInfo = launchUnitInfo;
        unitFighting = lauchUnitFighting;
    }
    public void AimButton()
    {
        unitFighting.GetEnemiesInRange();
    }
    public void FireButton()
    {
        unitFighting.FireMaLazer();
    }
    public void CancelButton()
    {
        unitFighting.ResetTargeting();
    }
    #region ButtonActivation
    public void AimButtonActive(bool setActive)
    {
        _aimButton.gameObject.SetActive(setActive);
    }
    public void FireButtonActive(bool setActive)
    {
        _fireButton.gameObject.SetActive(setActive);
    }
    public void UnitCanvasButtonActive(bool setActive)
    {
        _aimButton.SetActive(setActive);
        _fireButton.SetActive(setActive);
        _cancelButton.SetActive(setActive);
    }
    #endregion
}
