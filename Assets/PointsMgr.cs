using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsMgr : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private int points;

    [SerializeField] private GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        text.text = "Points: " + points;

        foreach(GameObject enemy in enemies)
        {
            if (enemy != null) break;

            //end game
        }
    }

    public void AddPoints(int _points)
    {
        points += _points;
    }
}
