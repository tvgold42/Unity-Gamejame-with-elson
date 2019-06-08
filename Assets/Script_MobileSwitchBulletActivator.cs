using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MobileSwitchBulletActivator : MonoBehaviour
{

    public bool NotTestingForMobile;
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && NotTestingForMobile)
        {
            this.gameObject.SetActive(false);
        }
    }
}
