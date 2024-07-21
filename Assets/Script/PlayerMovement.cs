using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    #region Variables
    [Header("_______ Planet _______")]
    [Tooltip("All Planet Mesh in scene")]
    [SerializeField] public GameObject[] Planet;

    [Tooltip("All Surface points for each planet Mesh in scene")]
    [SerializeField] public GameObject[] PlanetSurface;

    [Tooltip("All Orbit points for each planet Mesh in scene")]
    [SerializeField] public GameObject[] PlanetOrbit;

    [Tooltip("Index for flow control of planets in scene")]
    [SerializeField] public int PlanetIndex;

    [Tooltip("All Planet names")]
    [SerializeField] public string[] PlanetNames;

    [Tooltip("Sun")]
    [SerializeField] public GameObject Sun;

    [Tooltip("All Virtual Cams assigned to each planets and player")]
    /// <summary> 
    /// Cam Index for(Planet++)
    /// 0 = Player
    /// 1 = P1
    /// 2 = P3
    /// 3 = P4
    /// </summary>
    [SerializeField] public GameObject[] PlanetCam;
    
    [Header("_______ Player _______")]
    [Tooltip("Rate at which player will be rotating while moving.")]
    [Range(0, 10f)]
    [SerializeField] public float PlayerLookRate;

    [Tooltip("Rate at which Ship will be flying in space.")]
    [SerializeField] public float MoveRate;

    [Tooltip("Rate at which the Ship will be landing or launching.")]
    [SerializeField] public float LandRate;

    [Tooltip("Rate at which the planet will be rotating.")]
    [Range(0, 10f)]
    [SerializeField] public float RotateRate;

    [Tooltip("The Jump Height of Player.")]
    [SerializeField] public float JumpHeight;
    
    [Tooltip("The Jump force of Player.")]
    [SerializeField] public float JumpRate;

    /// <summary>
    /// if player is jumping or not, 0 = no, 1 = yes, 2 = Rest
    /// </summary>
    int IsJumping;

    [Tooltip("0 = Flying, 1 = Orbit, 2 = OnGround")]
    public int PlayerFlyState;

    /// <summary>
    /// Only true if(PlayerFlyState == 1), 0 = Landing, 1 = Launching 
    /// </summary>
    int LanderState;

    /// <summary>
    /// Permision to Land and Launch.
    /// </summary>
    bool LandingPerms;

    [Tooltip("Time takes to land or launch the ship.")]
    [SerializeField] float LandTime;
    float landTimer;
    int rotf, rotb, rotr, rotl;
    float hor, ver;
    float heightpoint;
    [SerializeField] Transform[] RadarTransform;
    [SerializeField] float RayDistance; 
    [SerializeField] float RayDistanceF;

    public GameObject player1;
    #endregion

    #region Unity events
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        /// PS State Launch to Land
        /// 2 > 1 > 0 > 1 > 2
        /// At PSo Coordinates for new planet will be recieved.
        /// 
        PlanetIndex = 0;
        PlayerFlyState = 2;
        IsJumping = 2;
        landTimer = LandTime;
        heightpoint = PlanetSurface[PlanetIndex].transform.position.y;
        transform.position = player1.transform.position;
        transform.rotation = player1.transform.rotation;
        player1.SetActive(false);
    }

    private void Update()
    {
        if (LevelManager.instance.IsDead == true)  return;
        if (LevelManager.instance.IsCutscene == true) return;
        if (LevelManager.instance.IsPaused == true) return;
        PlanetSwitcher();
        PlayerMove();
    }
    #endregion

    #region Planet
    /// <summary>
    /// Switch between Planets
    /// </summary>
    public void PlanetSwitcher()
    {
        if (PlayerFlyState == 2)
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
    }

    /// <summary>
    /// Rotate the Current Indexed Planet
    /// </summary>
    /// <param name="i"></param>
    public void RotatePlanet(int i)
    {
        PlanetIndex = i;
        Planet[PlanetIndex].transform.Rotate(Vector3.left, RotateRate * Time.deltaTime * ver, Space.World);
        Planet[PlanetIndex].transform.Rotate(Vector3.forward, RotateRate * Time.deltaTime * hor, Space.World);
    }
    #endregion

    #region Player
    /// <summary>
    /// Moves the player around space 
    /// </summary>
    public void PlayerMove()
    {
        switch(PlayerFlyState)
        {
            // Flying
            case 0:
                transform.LookAt(PlanetOrbit[PlanetIndex].transform);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, PlanetOrbit[PlanetIndex].transform.position.x, MoveRate), Mathf.Lerp(transform.position.y, PlanetOrbit[PlanetIndex].transform.position.y, LandRate), Mathf.Lerp(transform.position.z, PlanetOrbit[PlanetIndex].transform.position.z, MoveRate));

                for (int c = 0; c < PlanetCam.Length; c++)
                {
                    if (c != 0)
                    {
                        PlanetCam[c].SetActive(false);
                    }
                }
                PlanetCam[0].SetActive(true);
                break;

            // Orbiting
            case 1:

                for (int c = 0; c < PlanetCam.Length; c++)
                {
                    if (c != 0)
                    {
                        PlanetCam[c].SetActive(false);
                    }
                }

                PlanetCam[0].SetActive(true);
                // Landing and Launching the ship in Orbit Mode
                if (LandingPerms == true)
                {
                    PlayerLander(1);
                }
                else if (LandingPerms == false)
                {
                    PlayerLander(0);
                }

                switch (LanderState) 
                {
                    // Landing
                    case 0:
                        //transform.Rotate(Vector3.zero, Space.World);
                        transform.position = new Vector3(Mathf.Lerp(transform.position.x, PlanetSurface[PlanetIndex].transform.position.x, MoveRate), Mathf.Lerp(transform.position.y, PlanetSurface[PlanetIndex].transform.position.y, LandRate), Mathf.Lerp(transform.position.z, PlanetSurface[PlanetIndex].transform.position.z, MoveRate));
                        break;

                    // Launching
                    case 1:
                        transform.LookAt(PlanetOrbit[PlanetIndex].transform);
                        transform.position = new Vector3(Mathf.Lerp(transform.position.x, PlanetOrbit[PlanetIndex].transform.position.x, MoveRate), Mathf.Lerp(transform.position.y, 24, LandRate), Mathf.Lerp(transform.position.z, PlanetOrbit[PlanetIndex].transform.position.z, MoveRate));
                        break;
                }
                break;

            // OnLand
            case 2:

                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                hor = h;
                ver = v;


                for (int c = 0; c < PlanetCam.Length; c++)
                {
                    if (c != PlanetIndex + 1)
                    {
                        PlanetCam[c].SetActive(false);
                    }
                }
                PlanetCam[PlanetIndex+1].SetActive(true);

                // Jumping as Player
               /* if (Input.GetKeyDown(KeyCode.Space) && IsJumping == 2)
                {
                    IsJumping = 1;  
                }

                switch (IsJumping)
                {
                    case 0:
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, heightpoint, transform.position.z), JumpRate);
                        if (transform.position.y <= heightpoint)
                        {
                            IsJumping = 2;
                        }

                        break;

                    case 1:
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, heightpoint + JumpHeight, transform.position.z), JumpRate);
                        if (transform.position.y >= heightpoint + JumpHeight)
                        {
                            IsJumping = 0;
                        }
                        break;

                    case 2:
                        transform.position = new Vector3(PlanetSurface[PlanetIndex].transform.position.x,heightpoint, PlanetSurface[PlanetIndex].transform.position.z);
                        break;
                }*/
                   
                // Rotating the Player while on ground
                if (hor != 0 || ver != 0)
                {
                    Vector3 direction = new Vector3(hor, 0, ver);
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, PlayerLookRate * Time.deltaTime);
                    Sun.transform.Rotate(Vector3.left, RotateRate * Time.deltaTime * ver, Space.World);
                }
                
                /*
                Vector3 rayDirection = -transform.up;
                Ray ray = new Ray(RadarTransform[0].position, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, RayDistance))
                {
                    if (hit.collider.gameObject.tag == "Planet")
                    {
                        Vector3 targetNormal = hit.normal;
                        Quaternion targetplayerRotation = Quaternion.FromToRotation(transform.forward, targetNormal);

                        heightpoint = hit.point.y + 0.25f;
                       
                    }
                }
                Debug.DrawRay(RadarTransform[0].position, rayDirection * RayDistance, Color.red);

                float angle = Mathf.Sin(timeCounter * 3) * 10;
                timeCounter += Time.deltaTime;

                Vector3 rayDirection1 =  RadarTransform[1].transform.forward;
                Ray ray1 = new Ray(RadarTransform[1].position, rayDirection1);
                RaycastHit hit1;

                Vector3 rayDirection2 =  RadarTransform[2].transform.forward;
                Ray ray2 = new Ray(RadarTransform[2].position, rayDirection2);
                RaycastHit hit2;

                Vector3 rayDirection3 =  RadarTransform[3].transform.forward;
                Ray ray3 = new Ray(RadarTransform[3].position, rayDirection3);
                RaycastHit hit3;

                Vector3 rayDirection4 =  RadarTransform[4].transform.forward;
                Ray ray4 = new Ray(RadarTransform[4].position, rayDirection4);
                RaycastHit hit4;

                if (Physics.Raycast(ray1, out hit1, RayDistanceF))
                {
                    if (hit1.collider.gameObject.tag == "Planet")
                    {
                        Debug.LogWarning("Ship Stop");
                        ver = Mathf.Clamp(v, 0, 1) ;
                    }
                }
                Debug.DrawRay(RadarTransform[1].position, rayDirection1 * RayDistanceF, Color.yellow);

                if (Physics.Raycast(ray2, out hit2, RayDistanceF))
                {
                    if (hit2.collider.gameObject.tag == "Planet")
                    {
                        Debug.LogWarning("Ship Stop");
                        ver = Mathf.Clamp(v, -1, 0);
                    }
                }
                Debug.DrawRay(RadarTransform[2].position, rayDirection2 * RayDistanceF, Color.yellow);

                if (Physics.Raycast(ray3, out hit3, RayDistanceF))
                {
                    if (hit3.collider.gameObject.tag == "Planet")
                    {
                        Debug.LogWarning("Ship Stop");
                        hor = Mathf.Clamp(h, -1, 0);
                    }
                }
                Debug.DrawRay(RadarTransform[3].position, rayDirection3 * RayDistanceF, Color.yellow);

                if (Physics.Raycast(ray4, out hit4, RayDistanceF))
                {
                    if (hit4.collider.gameObject.tag == "Planet")
                    {
                        Debug.LogWarning("Ship Stop");
                        hor = Mathf.Clamp(h, 0, 1);
                    }
                }
                Debug.DrawRay(RadarTransform[4].position, rayDirection4 * RayDistanceF, Color.yellow);
                */
                // Checking the Collision with Raycast


                break;
        }       
    }

    /// <summary>
    /// Land or Launch the player, 0 = Flying, 1 = Landing, 2
    /// </summary>
    public void PlayerLander(int i)
    {
        LanderState = i;

        if (landTimer <= LandTime)
        {
            landTimer -= Time.deltaTime;
            if (landTimer <= 0)
            {
                landTimer = LandTime;

                if (LanderState == 0)
                {
                    Debug.LogWarning("Ship Landed");
                    PlayerFlyState = 2;
                }
                else if (LanderState == 1)
                {
                    Debug.LogWarning("Ship Launched");
                    LevelManager.instance.MapPanel.SetActive(true);
                    PlayerFlyState = 0;
                }
                landTimer = LandTime;
            }
        }
    }
    #endregion

    #region Colliders

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            LevelManager.instance.IsDead = true;
            LevelManager.instance.DeadPanel.SetActive(true);
        }

        if (other.gameObject.tag == "Trader")
        {
            LevelManager.instance.DialoguePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trader")
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "LaunchPad")
        {
            Debug.Log("Launching Perms");
            if (Input.GetKey(KeyCode.E))
            {
                LandingPerms = true;
                PlayerFlyState = 1;
            }
        } 

        if (other.gameObject.tag == "LandingPad")
        {
            Debug.Log("Landing Perms");
            if (Input.GetKey(KeyCode.E))
            {
                LandingPerms = false;
                PlayerFlyState = 1;
            }
        }
    }
    #endregion
}
