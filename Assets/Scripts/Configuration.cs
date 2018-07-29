using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public float DEGTORAD = 0.017453f;
    public float BULLETSPEED = .1f;
    public int WIDTH = 1280;
    public int HEIGHT = 720;
    public float PLAYERMAXSPEED = 10;
    public float PLAYERDEFAULTSPEED = .05f;
    public float ASTEROIDSPEED = .1f;
    public float ASTEROIDROTATIONSPEED = 1.0f;
    public int PLAYERROTATIONANGLE = 2;

    // singleton
    private static Configuration instance;

    // Construct
    private Configuration()
    {

    }

    // Instance
    public static Configuration Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(Configuration)) as Configuration;
            return instance;
        }
    }
}
