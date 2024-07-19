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
    [Range(0,10f)]
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
        PlanetSwitcher(); 
    }
    
    public void PlanetSwitcher()
    {
        switch (PlanetIndex)
        {
            case 0:
                RotatePlanet(0);
                break;
            case 1:
                RotatePlanet(1);
                break;
            case 2:
                RotatePlanet(3);
                break;
            case 3:
                RotatePlanet(4);
                break;
            case 4:
                RotatePlanet(5);
                break;
        }
    }

    public void RotatePlanet(int i)
    {
        PlanetIndex = i;

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Planet[PlanetIndex].transform.Rotate(Vector3.left, RotateRate*Time.deltaTime * ver, Space.World);
        Planet[PlanetIndex].transform.Rotate(new Vector3(0,0,1), RotateRate*Time.deltaTime *  hor, Space.World);
    }

    public void PlayerMove()
    {
        
    }

    public void PlayerLander()
    {
        if (landTimer <= LandTime)
        {
            landTimer -= Time.deltaTime;
            if (landTimer <= 0)
            {
                landTimer = LandTime;
                PlayerFlyState = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LaunchPad")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Launching");
                PlayerFlyState = 0;
            }
        }
    }
}
