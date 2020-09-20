using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    #region Variables
    UnitInformation unitInfo;
    [Header("Range")]
    static float smallRange = 2;
    static float mediumRange = 5;
    static float bigRange = 10;
    float unitRange;
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
    }

}
