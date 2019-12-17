using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private LoadMap map;
    private bool inMove = false;
    [SerializeField]
    private Vector2 direct;
    private Vector2 pos;
    private Vector2 initPos;
    private Vector2 nextPos;
    private Tile tileIn;
    private GameObject endScreen;
    private GameObject pauseScreen;
    private Text endScreen_winner;
    private Text endScreen_winner2;
    private Scene currentScene;
    private bool inTp = false;
    private GameObject tileInTp;
    private bool isPaused = false;
    private GameObject saveObj;
    private GameObject loadObj;
    private int saveContain = 0;
    private int loadContain = 0;
    private GameObject tmpParticle;
    private Color myColor;
    private bool haveMove = false;
    private int nbTrene = 5;
    private GameObject myBumper;

    public bool isFirstPlayer;
    public Player enemy;
    public float timeOfImobilityToDeath = 5f;
    public GameObject particuleMove;
    public float timeOfImobility = 0f;
    public GameObject particuleBumper;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        Time.timeScale = 1;
        if (isFirstPlayer)
        {
            myColor = Color.blue;
        }
        else
        {
            myColor = Color.green;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<LoadMap>();
        initPos = transform.position;

        tileIn = map.tileOn(initPos);
        endScreen = GameObject.Find("EndScreen");
        pauseScreen = GameObject.Find("PauseScreen");
        endScreen.transform.Find("Canvas").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnpauseGame();
        }
        if (!isPaused)
        {
            if (!inMove)
            {
                ChoseMove();
                if (haveMove)
                {
                    timeOfImobility += Time.deltaTime;
                }
                
                if (timeOfImobility >= timeOfImobilityToDeath)
                {
                    Destroy();
                }
            }
            else
            {
                timeOfImobility = 0f;
                Move();
                CreateTrene();
            }
        }
        
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.transform.Find("Canvas").gameObject.SetActive(true);

    }

    private void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.transform.Find("Canvas").gameObject.SetActive(false);
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        
        if (!inMove)
        {
            switch (data.isSwipeFromUpperScreen)
            {
                case true:    
                    if (!isFirstPlayer)
                    {
                        if (data.Direction == SwipeDirection.Up)
                        {
                            inMove = true;
                            direct = new Vector2(0, 1);
                            
                        }
                        else if (data.Direction == SwipeDirection.Down)
                        {
                            inMove = true;
                            direct = new Vector2(0, -1);
                        }
                        else if (data.Direction == SwipeDirection.Left)
                        {
                            inMove = true;
                            direct = new Vector2(-1, 0);
                        }
                        else if (data.Direction == SwipeDirection.Right)
                        {
                            inMove = true;
                            direct = new Vector2(1, 0);
                        }
                    }
                    break;
                case false:
                    if (isFirstPlayer)
                    {
                        if (data.Direction == SwipeDirection.Up)
                        {
                            inMove = true;
                            direct = new Vector2(0, 1);
                        }
                        else if (data.Direction == SwipeDirection.Down)
                        {
                            inMove = true;
                            direct = new Vector2(0, -1);
                        }
                        else if (data.Direction == SwipeDirection.Left)
                        {
                            inMove = true;
                            direct = new Vector2(-1, 0);
                        }
                        else if (data.Direction == SwipeDirection.Right)
                        {
                            inMove = true;
                            direct = new Vector2(1, 0);
                        }
                    }
                    break;
            }
            
        }
        ChoseMove();
    }

    void ChoseMove()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("z"))
        {
            inMove = true;
            direct = new Vector2(0, 1);
        }
        else if (Input.GetKeyDown("s"))
        {
            inMove = true;
            direct = new Vector2(0, -1);
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown("q"))
        {
            inMove = true;
            direct = new Vector2(-1, 0);
        }
        else if (Input.GetKeyDown("d"))
        {
            inMove = true;
            direct = new Vector2(1, 0);
        }
        if (inMove)
        {
            AffectMove();
        }
        ChangeRotation();
    }

    public void SwapMove(int godirect)
    {
        if (!inMove)
        {
            switch (godirect)
            {
                case 0: // up
                    inMove = true;
                    direct = new Vector2(0, 1);
                    break;
                case 1: // dawn
                    inMove = true;
                    direct = new Vector2(0, -1);
                    break;
                case 2: // right
                    inMove = true;
                    direct = new Vector2(1, 0);
                    break;
                case 3: // left
                    inMove = true;
                    direct = new Vector2(-1, 0);
                    break;
                default:
                    break;
            }
            ChoseMove();
        }
    }

    void AffectMove()
    {
        if ((initPos + direct).x < 0 || (initPos + direct).y < 0 || (initPos + direct).x > map.myX-1 || (initPos + direct).y > map.myY-1)
        {
            inMove = false;
            return;
        }
        else
        {
            Tile nextTile = map.GetContain(new Vector2(transform.position.x + direct.x, transform.position.y + direct.y));
            saveObj = null;
            if (inTp)
            {
                saveContain = 8;
            }
            else
            {
                saveContain = 0;
            }
            
            switch (nextTile.contain)
            {
                case -1:
                    Debug.Log("error");
                    break;
                case 0: // empty
                    initPos = transform.position;
                    nextPos = initPos + direct;
                    break;
                case 1: // wall
                    inMove = false;
                    Wall myWall = nextTile.obj.GetComponent<Wall>();
                    myWall.hit();
                    break;
                case 2: // player
                    if (enemy.direct != -direct)
                    {
                        enemy.Destroy();
                        initPos = transform.position;
                        nextPos = initPos + direct;
                        EndGame();
                    }
                    else
                    {
                        direct *= -1;
                        enemy.direct *= -1;
                    }
                    inMove = true;
                    break;
                case 3: // box
                    Box myBox = map.GetObj(new Vector2(transform.position.x + direct.x, transform.position.y + direct.y)).GetComponent<Box>();
                    myBox.direct = direct;
                    myBox.AffectMove();
                    myBox.inMove = true;
                    inMove = false;
                    break;
                case 4: // wall Destruct
                    DestructWall myWallDes = nextTile.obj.GetComponent<DestructWall>();

                    myWallDes.TakeHit();
                    myWallDes.hit();
                    if (myWallDes.life-- <= 0)
                    {
                        for (int i = 0; i < map.destrucWalls.Count; i++)
                        {
                            map.destrucWalls.Remove(myWallDes.gameObject);
                            Destroy(myWallDes.gameObject);
                            initPos = transform.position;
                            nextPos = initPos + direct;
                        }
                    }
                    else
                    {
                        inMove = false;
                    }
                    break;
                case 5: // bumper
                    hitBumper();
                    direct *= -1;
                    break;
                case 6: // rotator
                    Rotator myRotator = nextTile.obj.GetComponent<Rotator>();
                    initPos = transform.position;
                    nextPos = initPos + direct;
                    direct = myRotator.direct;
                    saveObj = myRotator.gameObject;
                    saveContain = 6;
                    break;
                case 7: // spike
                    initPos = transform.position;
                    nextPos = initPos + direct;
                    transform.position = nextPos;
                    Destroy();
                    EndGame();
                    break;
                case 8: // teleporter
                    Teleporter myTeport = nextTile.obj.GetComponent<Teleporter>();
                    initPos = transform.position;
                    nextPos = initPos + direct;
                    inTp = true;
                    tileInTp = myTeport.sortie;
                    saveObj = myTeport.sortie.GetComponent<Teleporter>().gameObject;
                    saveContain = 8;
                    myTeport.hit();
                    break;

                default:
                    Debug.Log(map.GetContain(new Vector2(transform.position.x + direct.y, transform.position.y + direct.y)));
                    inMove = false;
                    break;
            }
        }

        
    }

    void Move()
    {
        transform.position = Vector2.Lerp(initPos, nextPos, 1);
        if (transform.position.x == nextPos.x && transform.position.y == nextPos.y)
        {
            if (!haveMove)
            {
                if (GameObject.Find("TeamUI") != null)
                {
                    GameObject.Find("TeamUI").SetActive(false);
                }
                haveMove = true;
                enemy.haveMove = true;
            }
            
            map.affectTile(initPos, loadContain, loadObj);
            
            if (inTp)
            {
                transform.position = tileInTp.transform.position;
                inTp = false;
                initPos = tileInTp.transform.position;
            }
            else
            {
                map.affectTile(nextPos, 2, null);
                map.affectTile(initPos, loadContain, loadObj);
                initPos = transform.position;
            }
            loadContain = saveContain;
            loadObj = saveObj;

            AffectMove();
        }
        if (FindObjectOfType<LoadMap>().players.Count != 2)
        {
            EndGame();
        }
    }

    void CreateTrene()
    {
        for (int i = 0; i < nbTrene; i++)
        {
            tmpParticle = Instantiate(particuleMove);
            tmpParticle.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            tmpParticle.GetComponent<Particle>().myColor = myColor;
        }
        
    }

    void ChangeRotation()
    {
        if (direct == new Vector2(0, 1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direct == new Vector2(0, -1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direct == new Vector2(-1, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    void Destroy()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateTrene();
        }
        enemy.isPaused = true;
        gameObject.SetActive(false);
        EndGame();
    }

    public void hitBumper()
    {
        myBumper = Instantiate(particuleBumper);
        myBumper.transform.position = transform.position + new Vector3(direct.x, direct.y, 0);
        Destroy(myBumper, 2);
    }

    void EndGame()
    {
        endScreen.transform.Find("Canvas").gameObject.SetActive(true);
        endScreen_winner = GameObject.Find("Text").GetComponent<Text>();
        endScreen_winner2 = GameObject.Find("Text2").GetComponent<Text>();
        if (GameObject.Find("Player 1") != null)
        {
            endScreen_winner.text = "Blue Player Won";
            endScreen_winner.color = Color.blue;
            endScreen_winner2.text = "Blue Player Won";
            endScreen_winner2.color = Color.blue;
        }
        else if (GameObject.Find("Player 2") != null)
        {
            endScreen_winner.text = "Green Player Won";
            endScreen_winner.color = Color.green;
            endScreen_winner2.text = "Green Player Won";
            endScreen_winner2.color = Color.green;
        }
        else
        {
            endScreen_winner.text = "Equality";
        }
        Time.timeScale = 0;
    }
}
