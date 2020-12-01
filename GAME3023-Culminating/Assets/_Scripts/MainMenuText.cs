using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuText : MonoBehaviour
{
    [SerializeField]
    Text student1Name;
    [SerializeField]
    Text student1ID;
    [SerializeField]
    Text student2Name;
    [SerializeField]
    Text student2ID;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            student1Name.text = "Cameron Akey";
            student1ID.text = "101166181";

            student2Name.text = "Raven Powless";
            student2ID.text = "101173103";
        }
    }
}
