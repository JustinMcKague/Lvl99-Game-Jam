using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Formatting
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public bool actionInProgress;

    public Transform rotatorParent;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLevel()
    {

    }

    public void WinLevel()
    {

    }

    public void QuitToMainMenu()
    {

    }
}
