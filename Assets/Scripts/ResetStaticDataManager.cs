using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{

    private void Awake()
    {
        ContainerCounter.ResetStaticData();
        StoveCounter.ResetStaticData();
        ClearCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        PlatesCounter.ResetStaticData();
    }
}
