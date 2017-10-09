using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    public virtual void Die()
    {
        canvas.SetActive(false);
    }
}
