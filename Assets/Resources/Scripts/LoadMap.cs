using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public struct Tile
{
    public Vector2 pos;
    public int contain;
    public GameObject obj;
}

public class LoadMap : MonoBehaviour
{
    public int myX = 6;
    public int myY = 5;
    public GameObject wallPrefab;

    [SerializeField]
    private List<Tile> tilesMap = new List<Tile>();

    private List<GameObject> walls = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private List<GameObject> boxs = new List<GameObject>();
    public List<GameObject> destrucWalls = new List<GameObject>();
    private List<GameObject> bumpers = new List<GameObject>();
    public List<GameObject> rotators = new List<GameObject>();
    private List<GameObject> spikes = new List<GameObject>();
    public List<GameObject> teleporters = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Wall item in FindObjectsOfType<Wall>())
        {
            walls.Add(item.gameObject);
        }
        foreach (Player item in FindObjectsOfType<Player>())
        {
            players.Add(item.gameObject);
        }
        foreach (Box item in FindObjectsOfType<Box>())
        {
            boxs.Add(item.gameObject);
        }
        foreach (DestructWall item in FindObjectsOfType<DestructWall>())
        {
            destrucWalls.Add(item.gameObject);
        }
        foreach (Bumper item in FindObjectsOfType<Bumper>())
        {
            bumpers.Add(item.gameObject);
        }
        foreach (Rotator item in FindObjectsOfType<Rotator>())
        {
            rotators.Add(item.gameObject);
        }
        foreach (Spike item in FindObjectsOfType<Spike>())
        {
            spikes.Add(item.gameObject);
        }
        foreach (Teleporter item in FindObjectsOfType<Teleporter>())
        {
            teleporters.Add(item.gameObject);
        }

        Load();

        CreateWall();
    }

    private void CreateWall()
    {
        GameObject tmpWall;
        for (int i = -1; i <= myX; i++)
        {
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(i, -1);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(i, -2);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(i, myY);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(i, myY+1);
        }
        for (int i = 0; i < myY + 2; i++)
        {
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(-1, i);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(myX, i);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(-2, i);
            tmpWall = Instantiate(wallPrefab);
            tmpWall.transform.position = new Vector2(myX+1, i);
        }
    }
    
    public Tile GetContain(Vector2 vector)
    {
        foreach (Tile tile in tilesMap)
        {
            if (tile.pos == vector)
            {
                return tile;
            }
        }

        return tilesMap[0];
    }

    public GameObject GetObj(Vector2 vector)
    {
        foreach (Tile tile in tilesMap)
        {
            if (tile.pos == vector)
            {
                return tile.obj;
            }
        }

        return null;
    }

    public Tile tileOn(Vector2 pos)
    {
        Tile tile = new Tile();

        for (int i = 0; i < tilesMap.Count; i++)
        {
            if (tilesMap[i].pos == pos)
            {
                tile = tilesMap[i];
            }
        }

        return tile;
    }

    public void affectTile(Vector2 pos, int nbObj, GameObject obj)
    {
        for (int i = 0; i < tilesMap.Count; i++)
        {
            if (tilesMap[i].pos == pos)
            {
                Tile t = new Tile();
                t.pos = tilesMap[i].pos;
                t.contain = nbObj; // devien vide
                t.obj = obj;

                tilesMap[i] = t;
                break;
            }
        }
    }

    public Tile ChangeBy(Vector2 origine, Vector2 next, Tile tile)
    {
        Tile tmp = new Tile();
        Tile toReturn = new Tile();

        for (int i = 0; i < tilesMap.Count; i++)
        {
            if (tilesMap[i].pos == origine)
            {
                tmp = tilesMap[i];
                tilesMap[i] = tile;
                break;
            }
        }

        for (int i = 0; i < tilesMap.Count; i++)
        {
            if (tilesMap[i].pos == next)
            {
                toReturn = tilesMap[i];
                tilesMap[i] = tmp;

                break;
            }
        }
        return toReturn;
    }

    private void Load()
    {
        for (int i = 0; i < myX; i++)
        {
            for (int j = 0; j < myY; j++)
            {
                Tile myTile = new Tile();
                myTile.pos = new Vector2(i, j);
                myTile.contain = 0;
                myTile.obj = null;
                tilesMap.Add(myTile);
            }
        }

        foreach (GameObject wall in walls)
        {
            int wallX = (int)wall.transform.position.x;
            int wallY = (int)wall.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(wallX, wallY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 1;
                    t.obj = wall;

                    tilesMap[i] = t;
                    break;
                }
            }
        }

        foreach (GameObject wall in destrucWalls)
        {
            int wallX = (int)wall.transform.position.x;
            int wallY = (int)wall.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(wallX, wallY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 4;
                    t.obj = wall;

                    tilesMap[i] = t;
                    break;
                }
            }
        }

        foreach (GameObject player in players)
        {
            int playerX = (int)player.transform.position.x;
            int playerY = (int)player.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(playerX, playerY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 2;

                    tilesMap[i] = t;
                }
            }
        }

        foreach (GameObject wall in bumpers)
        {
            int wallX = (int)wall.transform.position.x;
            int wallY = (int)wall.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(wallX, wallY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 5;

                    tilesMap[i] = t;
                    break;
                }
            }
        }

        foreach (GameObject box in boxs)
        {
            int boxX = (int)box.transform.position.x;
            int boxY = (int)box.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(boxX, boxY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 3;
                    t.obj = box;

                    tilesMap[i] = t;
                }
            }
        }

        foreach (GameObject rotator in rotators)
        {
            int boxX = (int)rotator.transform.position.x;
            int boxY = (int)rotator.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(boxX, boxY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 6;
                    t.obj = rotator;

                    tilesMap[i] = t;
                }
            }
        }

        foreach (GameObject spike in spikes)
        {
            int boxX = (int)spike.transform.position.x;
            int boxY = (int)spike.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(boxX, boxY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 7;
                    t.obj = spike;

                    tilesMap[i] = t;
                }
            }
        }

        foreach (GameObject teleporter in teleporters)
        {
            int boxX = (int)teleporter.transform.position.x;
            int boxY = (int)teleporter.transform.position.y;
            for (int i = 0; i < tilesMap.Count; i++)
            {
                if (tilesMap[i].pos == new Vector2(boxX, boxY))
                {
                    Tile t = new Tile();
                    t.pos = tilesMap[i].pos;
                    t.contain = 8;
                    t.obj = teleporter;

                    tilesMap[i] = t;
                }
            }
        }
    }

    #region Debug Map
    private void OnDrawGizmosSelected()
    {   
        foreach (Tile tile in tilesMap)
        {
            switch(tile.contain)
            {
                case 0:
                    Gizmos.color = Color.white;
                    break;
                case 1: // wall
                    Gizmos.color = Color.blue;
                    break;
                case 2: // player
                    Gizmos.color = Color.green;
                    break;
                case 3: // box
                    Gizmos.color = Color.grey;
                    break;
                case 4: // destructWall
                    Gizmos.color = Color.cyan;
                    break;
                case 5: // destructWall
                    Gizmos.color = Color.yellow;
                    break;
                case 6: // rotator
                    Gizmos.color = Color.cyan;
                    break;
                case 7: // spike
                    Gizmos.color = Color.black;
                    break;
                case 8: // teleporter
                    Gizmos.color = Color.magenta;
                    break;
            }
            Gizmos.DrawWireSphere(tile.pos, 0.5f);
        }
    }

    
#if UNITY_EDITOR
    [CustomEditor(typeof(LoadMap))]
    public class LoadMapEditor : Editor
    {
        public LoadMap _target;

        private void OnEnable()
        {
            _target = target as LoadMap;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load Map"))
                _target.Load();
        }
    }
#endif
    #endregion
}
