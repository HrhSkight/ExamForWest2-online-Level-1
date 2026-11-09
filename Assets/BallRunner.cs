using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallRunner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float moveRange = 10f;
    //private float acceleration = 0;
    private Vector3 startPosition;
    private float moveDist { 
        get {
            return (transform.position - startPosition).x;
        }
    }
    private bool isInCold = false;
    private bool isLeft = false;
    Coroutine forCold;

    private float GetSpeed()
    {
        return moveSpeed * ((Mathf.Abs(moveDist) + 1) / moveRange);
    }
    private IEnumerator ColdMove()
    {
        isInCold = true;
        yield return new WaitForSeconds(waitTime);
        isLeft = !isLeft;
        isInCold = false;
    }
    private void Awake()
    {
        startPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isInCold) return;
        if (moveDist >= moveRange && !isLeft || moveDist <= -moveRange && isLeft) StartCoroutine(ColdMove());
        if (!isInCold)
        {
            transform.position += new Vector3((isLeft ? -1 : 1) * GetSpeed() * Time.fixedDeltaTime, 0, 0);
        }
    }

    private void OnDestroy()
    {
        if (forCold != null) StopCoroutine(forCold);
    }
}