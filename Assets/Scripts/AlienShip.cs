using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShip : MonoBehaviour
{
    public GameObject _alienShipPrefab;
    public GameObject _alienProjectile;
    private GameObject _alienShip;
    private GameObject _alienProjectileFiring;

    public GameObject _borderTopLeft;
    public GameObject _borderTopRight;
    public GameObject _borderBottomLeft;
    public GameObject _borderBottomRight;

    public float _currentStartY;
    public float _currentDestinationX;
    public float _currentDestinationY;
    public float _speedOfShip;

    public float _moveDistance;

    public void CreateAlienShip()
    {
        _currentStartY = Random.Range(_borderTopRight.transform.position.y, _borderBottomRight.transform.position.y);

        Vector3 startPosition = new Vector3(_borderTopRight.transform.position.x, _currentStartY, 0);

        _currentDestinationX = _borderTopRight.transform.position.x - _moveDistance;

        _alienShip = Instantiate(_alienShipPrefab, startPosition, Quaternion.identity);

        _currentDestinationY = _alienShip.transform.position.y;

        MoveAlienShip();
    }

    public void MoveAlienShip()
    {
        LeanTween.move(_alienShip, new Vector3(_currentDestinationX, _currentDestinationY, 0f), _speedOfShip).setOnComplete(FinishedMovement);
    }

    public void FinishedMovement()
    {
        Debug.Log("FinishedMovement");
        if(_alienShip.transform.position.x > _borderTopLeft.transform.position.x)
        {
            _currentDestinationY = Random.Range(_borderTopRight.transform.position.y, _borderBottomRight.transform.position.y);
            _currentDestinationX = _alienShip.transform.position.x - _moveDistance;
            MoveAlienShip();
        }
        else
        {
            Destroy(_alienShip);
            GameObject.Find("Main").GetComponent<Main>()._isAlienShipCreated = false;
        }
    }

    public void SpawnShip()
    {
        CreateAlienShip();
    }
}
