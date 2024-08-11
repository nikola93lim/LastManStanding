using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodSplatter;

    private IKillable _killableEntity;

    private void Start()
    {
        _killableEntity = GetComponent<IKillable>();
        _killableEntity.OnKilled += KillableEntity_OnKilled;
    }

    private void KillableEntity_OnKilled(Vector3 direction)
    {
        _bloodSplatter.transform.forward = direction;
        _bloodSplatter.transform.parent = null;
        _bloodSplatter.Play();
        Destroy(gameObject);
    }
}
