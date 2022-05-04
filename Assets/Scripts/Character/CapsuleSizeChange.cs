using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSizeChange : MonoBehaviour
{
    [SerializeField]
    private Vector3 capsuleCenter;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float height;

    private Vector3 initialCenter;
    private float initialRadius;
    private float initialHeight;


    private CapsuleCollider _capsuleColl;
    private CharacterEngine _charEng;

    private void Awake()
    {
        _capsuleColl = GetComponent<CapsuleCollider>();
        _charEng = GetComponent<CharacterEngine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialCenter = _capsuleColl.center;
        initialRadius = _capsuleColl.radius;
        initialHeight = _capsuleColl.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (_charEng.ReturnCrouch())
        {
            _capsuleColl.center = capsuleCenter;
            _capsuleColl.radius = radius;
            _capsuleColl.height = height;
        }
        else
        {
            _capsuleColl.center = initialCenter;
            _capsuleColl.radius = initialRadius;
            _capsuleColl.height = initialHeight;
        }
    }
}
