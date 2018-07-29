using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameElement
{
    void Start()
    {
        _deltaX = (float)(Random.Range(0, Configuration.Instance.ASTEROIDSPEED));
        _deltaY = (float)(Random.Range(0, Configuration.Instance.ASTEROIDSPEED));

        _posX = transform.position.x;
        _posY = transform.position.y;

        _angle = (float)(Random.Range(0, 360));
        _health = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update position based on changes in the delta
        _posX += _deltaX;
        _posY += _deltaY;

        _angle += Configuration.Instance.ASTEROIDROTATIONSPEED;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Bullet")
        {
            _health = false;
            other.GetComponent<Bullet>()._health = false;
        }
        if (other.gameObject.name == "Player")
        {
            _health = false;
            other.GetComponent<Player>()._health = false;
        }
    }
}
