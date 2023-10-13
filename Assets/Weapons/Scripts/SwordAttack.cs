using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private const string KILL_METHOD = "Kill";
    [SerializeField] private float _lifeTime = 5.0f;
    [SerializeField] private int _damage = 5;

    private void Awake()
    {
        Invoke(KILL_METHOD, _lifeTime);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    const string FRIENDLY_TAG = "Friendly";
    const string ENEMY_TAG = "Enemy";
    const string ARTIFACT_TAG = "Artifact";
    void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly or enemies or the artifact
        if (other.tag != FRIENDLY_TAG && other.tag != ENEMY_TAG && other.tag != ARTIFACT_TAG)
            return;

        //only hit the opposing team
        if (other.tag == tag)
            return;

        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null)
        {
            otherHealth.Damage(_damage);
            Kill();
        }
    }
}
