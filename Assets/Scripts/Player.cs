using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameElement
{
    public bool _thrust;

    public void ResetPlayerPosition()
    {
        _posX = 0;
        _posY = 0;
        _thrust = false;
        _deltaX = 0;
        _deltaY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is applying thrust
        if (_thrust)
        {
            _deltaX += (float)(Mathf.Cos(_angle * Configuration.Instance.DEGTORAD) * Configuration.Instance.PLAYERDEFAULTSPEED);
            _deltaY += (float)(Mathf.Sin(_angle * Configuration.Instance.DEGTORAD) * Configuration.Instance.PLAYERDEFAULTSPEED);
        }
        else
        {
            _deltaX *= 0.99f;
            _deltaY *= 0.99f;
        }

        // Set speed
        float _speed = Mathf.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);

        // Check to see if player is at playerMaxSpeed
        if (_speed > Configuration.Instance.PLAYERMAXSPEED)
        {
            _deltaX *= Configuration.Instance.PLAYERMAXSPEED / _speed;
            _deltaY *= Configuration.Instance.PLAYERMAXSPEED / _speed;
        }

        _posX += _deltaX;
        _posY += _deltaY;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Check to see if off screen. If is, pop up on opposite side.
        if (screenPos.x > Configuration.Instance.WIDTH)
        {
            _posX = Camera.main.ScreenToWorldPoint(new Vector3(0, screenPos.y, 0)).x;
        }
        if (screenPos.x < 0)
        {
            _posX = Camera.main.ScreenToWorldPoint(new Vector3(Configuration.Instance.WIDTH, screenPos.y, 0)).x;
        }
        if (screenPos.y > Configuration.Instance.HEIGHT)
        {
            _posY = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, 0, 0)).y;
        }
        if (screenPos.y < 0)
        {
            _posY = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, Configuration.Instance.HEIGHT, 0)).y;
        }

        transform.eulerAngles = new Vector3(0, 0, _angle);
        transform.position = new Vector3(_posX, _posY, 0);
    }
}
