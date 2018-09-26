using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject _player;
    public Player _playerInfo;
    public GameObject _bullet;
    public GameObject _asteroid;
    public List<GameObject> _gameElementsList;
    public List<GameObject> _cleanGameElementsList;
    public bool _newGame = false;
    public Text _score;
    public Text _highScore;
    public int _scoreValue = 0;
    public int _highScoreValue = 0;

    public bool _isAlienShipCreated = false;
    public GameObject _alienShip;

    // Use this for initialization
    void Start()
    {
        _gameElementsList = new List<GameObject>();
        _cleanGameElementsList = new List<GameObject>();
        _playerInfo.GetComponent<Player>();
        _highScoreValue = 0;
        InitGame();
    }

    void InitGame()
    {
        _newGame = false;
        _scoreValue = 0;
        // Clean out the _cleanGameElementsList
        for (int i = 0; i < _gameElementsList.Count; i++)
        {
            if (_gameElementsList[i].name != "Player")
            {
                Destroy(_gameElementsList[i]);
            }
        }

        _gameElementsList.Clear();
        _cleanGameElementsList.Clear();

        // Set Player
        _player.transform.position = new Vector3(0, 0, 0);
        _playerInfo.ResetPlayerPosition();
        _gameElementsList.Add(_player);

        for (int index = 1; index < 6; index++)
        {
            GenerateAsteroid();
        }
    }

    void GenerateAsteroid()
    {
        GameObject myAsteroid = Instantiate(_asteroid) as GameObject;
        myAsteroid.name = "AsteroidLarge";
        do
        {
            myAsteroid.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-2, 2), 0);
        } while (Vector3.Distance(myAsteroid.transform.position, _player.transform.position) < 5);

        _gameElementsList.Add(myAsteroid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _playerInfo._angle -= Configuration.Instance.PLAYERROTATIONANGLE;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _playerInfo._angle += Configuration.Instance.PLAYERROTATIONANGLE;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _playerInfo._thrust = true;
        }
        else
        {
            _playerInfo._thrust = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myBulletGameObject = Instantiate(_bullet) as GameObject;
            myBulletGameObject.name = "Bullet";
            Bullet myBullet = myBulletGameObject.GetComponent<Bullet>();
            myBullet._angle = _playerInfo._angle;
            myBullet._posX = _playerInfo._posX;
            myBullet._posY = _playerInfo._posY;
            myBullet.transform.position = _player.transform.position;
            myBullet.transform.rotation = _player.transform.rotation;
            myBulletGameObject.SetActive(true);
            _gameElementsList.Add(myBulletGameObject);
        }

        // Check Large Asteroid Array
        for (int index = 0; index < _gameElementsList.Count; index++)
        {
            if (_gameElementsList[index].name == "AsteroidLarge")
            {
                if (!_gameElementsList[index].GetComponent<Asteroid>()._health)
                {
                    _scoreValue += 100;
                    Debug.Log("Make smaller Asteroids and Destroy the large one");
                    for (int asteroidIndex = 1; asteroidIndex <= 2; asteroidIndex++)
                    {
                        GameObject myAsteroid = Instantiate(_asteroid) as GameObject;
                        myAsteroid.name = "AsteroidSmall";
                        myAsteroid.transform.localScale = new Vector3(1, 1, 1);
                        myAsteroid.transform.position = _gameElementsList[index].transform.position;
                        _gameElementsList.Add(myAsteroid);
                    }
                    _cleanGameElementsList.Add(_gameElementsList[index]);
                }
            }

            if (_gameElementsList[index].name == "AsteroidSmall")
            {
                if (!_gameElementsList[index].GetComponent<Asteroid>()._health)
                {
                    _scoreValue += 200;
                    Debug.Log("Destroy the small one");
                    _cleanGameElementsList.Add(_gameElementsList[index]);
                }
            }

            if (_gameElementsList[index].name == "Bullet")
            {
                if (!_gameElementsList[index].GetComponent<Bullet>()._health)
                {
                    Debug.Log("Destroy the Bullet");
                    _cleanGameElementsList.Add(_gameElementsList[index]);
                }
            }

            if (_gameElementsList[index].name == "Player")
            {
                if (!_gameElementsList[index].GetComponent<Player>()._health)
                {
                    Debug.Log("Destroy the Player and Reset Game");
                    _gameElementsList[index].GetComponent<Player>()._health = true;
                    _newGame = true;
                }
            }
        }

        foreach (var item in _cleanGameElementsList)
        {
            _gameElementsList.Remove(item);
            Destroy(item);
        }

        _cleanGameElementsList.Clear();

        // Update Score
        _score.text = _scoreValue.ToString();
        if(_scoreValue > _highScoreValue)
        {
            _highScoreValue = _scoreValue;
            _highScore.text = _highScoreValue.ToString();
        }

        if (_newGame)
        {
            InitGame();
        }

        // Should we generate a new asteroid
        if(Random.Range(0,150) > 147)
        {
            GenerateAsteroid();
        }

        if(_scoreValue > 1000 && _isAlienShipCreated == false)
        {
            _isAlienShipCreated = true;
            _alienShip.GetComponent<AlienShip>().SpawnShip();
        }
    }
}
