using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarEscape : MonoBehaviour
{
    [SerializeField] GameObject carKeysImage;

    private bool hasCarKeys;

    private void Start()
    {
        carKeysImage.SetActive(false);
    }

    public bool HasKeys()
    {
        return hasCarKeys;
    }

    public void PickUpCarKeys()
    {
        hasCarKeys = true;
        carKeysImage.SetActive(true);
    }
}
