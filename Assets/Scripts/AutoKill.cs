using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    [SerializeField] float _lifeTime = 5.0f;

    void Awake()
    {
        Invoke("Kill", _lifeTime);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
