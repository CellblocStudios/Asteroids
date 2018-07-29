using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameElement
{
    private void Start()
    {
        _health = true;
    }
    // Update is called once per frame
    void Update()
    {
        // Update the position of the Bullet
        _deltaX = Mathf.Cos(_angle * Configuration.Instance.DEGTORAD) * Configuration.Instance.BULLETSPEED;
        _deltaY = Mathf.Sin(_angle * Configuration.Instance.DEGTORAD) * Configuration.Instance.BULLETSPEED;
        _posX += _deltaX;
        _posY += _deltaY;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // If it leaves the screen, set to destroy
        if (screenPos.x < 0 || screenPos.x > Configuration.Instance.WIDTH || screenPos.y > Configuration.Instance.HEIGHT || screenPos.y < 0)
        {
            _health = false;
        }

        transform.eulerAngles = new Vector3(0, 0, _angle);
        transform.position = new Vector3(_posX, _posY, 0);
    }
}
