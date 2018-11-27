using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public float red;
    public float green;
    public float blue;

    public Vector3 localScale;

    private bool eliminated = false;
    public float timeToEliminate = 0f;

    private Collider _collider;
    private Renderer _renderer;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    private void Init()
    {
        _collider=GetComponent<BoxCollider>();
        _renderer=GetComponent<Renderer>();
        _renderer.material.color=new Color(red, blue, green);
        localScale=transform.localScale;
    }

    private void OnMouseDown()
    {
        eliminated=true;
        timeToEliminate=PopulationManager.elapsed;
        Debug.Log("dead at :"+timeToEliminate);
        _renderer.enabled=false;
        _collider.enabled=false;
    }
}