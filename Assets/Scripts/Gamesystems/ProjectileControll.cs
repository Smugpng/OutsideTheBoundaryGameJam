using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControll : MonoBehaviour
{

    public ParticleSystem _particleSystem;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Invoke("destroy", 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        _rb.isKinematic = true;
        _meshRenderer.enabled = false;
        _particleSystem.Play();
        Invoke("destroy", 4f);
    }
    private void destroy()
    {
        Destroy(gameObject);
    }
}
