using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Planet Index
    /// </summary>
    [Tooltip("_______Planet_______")]
    [SerializeField] public GameObject[] Planet;
    [SerializeField] public int PlanetIndex;

    /// <summary>
    /// Cam Index 
    /// 0 = Player
    /// 1 = P1
    /// 2 = P3
    /// 3 = P4
    /// </summary>
    [SerializeField] public GameObject[] PlanetCam;
    [Range(0,1)]
    [SerializeField] public float RotateRate;

    [SerializeField] public GameObject PlayerObj;

    /// <summary>
    /// 0 = Flying, 1 = Landed
    /// </summary>
    int PlayerFlyState;
    [SerializeField] float LandTime;
    float landTimer;

    float hor, ver;

    private void Update()
    {
        RotatePlanet(0);
    }

    public void RotatePlanet(int i)
    {
        PlanetIndex = i;

        Planet[PlanetIndex].transform.Rotate(Vector3.left, RotateRate);
    }

    public void PlayerMove()
    {
        
    }

    public void PlayerLander()
    {

    }
}
