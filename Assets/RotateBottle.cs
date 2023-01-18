using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBottle : MonoBehaviour
{
    private const float WaitInterval = 0.5F;
    private const float RotationInterval = 3.0F;
    private const float TotalInterval = RotationInterval + WaitInterval;
    //private float _elapsedX;
    //private float _elapsedY;
    //private float _elapsedZ;
    private float _elapsed = 0;
    enum JikuFlg { X = 0,Y = 1,Z = 2 }

    private void Start()
    {
        StartCoroutine(Rotate());
    }
    private void Update()
    {
        //if (_elapsedX < TotalInterval)
        //{
        //    _elapsedX += Time.deltaTime;
        //    if (_elapsedX < RotationInterval)
        //    {
        //        transform.Rotate(1, 0, 0, Space.World);
        //    }
        //}
        //else if (_elapsedY < TotalInterval)
        //{
        //    _elapsedY += Time.deltaTime;
        //    if (_elapsedY < RotationInterval)
        //    {
        //        transform.Rotate(0, 1, 0, Space.World);
        //    }
        //}
        //else if (_elapsedZ < TotalInterval)
        //{
        //    _elapsedZ += Time.deltaTime;
        //    if (_elapsedZ < RotationInterval)
        //    {
        //        transform.Rotate(0, 0, 1, Space.World);
        //    }
        //}
        //else
        //{
        //    _elapsedX = _elapsedY = _elapsedZ = 0;
        //}
        
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            yield return RotateX();
            yield return RotateY();
            yield return RotateZ();
        }
    }
    IEnumerator RotateX()
    {
        while (_elapsed < RotationInterval)
        {
            transform.Rotate(1, 0, 0, Space.World);
            _elapsed += Time.deltaTime;
            yield return null;
        }
        _elapsed = 0;
        yield return new WaitForSeconds(WaitInterval);
    }
    IEnumerator RotateY()
    {
        while (_elapsed < RotationInterval)
        {
            transform.Rotate(0, 1, 0, Space.World);
            _elapsed += Time.deltaTime;
            yield return null;
        }
        _elapsed = 0;
        yield return new WaitForSeconds(WaitInterval);
    }
    IEnumerator RotateZ()
    {
        while (_elapsed < RotationInterval)
        {
            transform.Rotate(0, 0, 1, Space.World);
            _elapsed += Time.deltaTime;
            yield return null;
        }
        _elapsed = 0;
        yield return new WaitForSeconds(WaitInterval);
    }
}

