using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MapNavigation : MonoBehaviour
{
    public static MapNavigation Instance;

    [SerializeField] private Transform player;
    [SerializeField] private MapLibrary library;
    [SerializeField] private Transform mapParent;
    private Dictionary<int, MapData> mapDictionary = new Dictionary<int, MapData>();
    [SerializeField] private GameObject currentMap;
    public UnityEvent OnMapEnter;
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        InitializeMapDictionary();
    }

    private void InitializeMapDictionary()
    {
        mapDictionary.Clear();
        foreach(GameMap m in library.mapLibrary)
        {
            mapDictionary.Add(m.mapID, new MapData(m));
        }
    }

    public void GoToMap(int mapID, int portalID)
    {
        // destroy the current map
        // instantiate the new map under the corresponding parent
        // move the player to the designated cell

        GameStateManager.Instance.SaveGameState();

        currentMap.GetComponentInChildren<EnemySpawner>().ClearEnemies();
        Destroy(currentMap);
        
        currentMap = Instantiate(mapDictionary[mapID].prefab, mapParent);
        Grid g = mapParent.GetComponent<Grid>();
        player.position = g.GetCellCenterWorld(mapDictionary[mapID].entryPoints[portalID].cell);
        StartCoroutine(InitializeNextMap(mapID));
        OnMapEnter?.Invoke();
        // get the cell that we want to the player to spawn in mapDictionary[mapID].entryPoints[portalID].cell
        // convert the cell into world space. returns a Vector3 <-- the new position
        // set the position to the updated value

        // add aesthetics: screen fade, sfx, animations
    }

    private IEnumerator InitializeNextMap(int mapID)
    {
        yield return new WaitForEndOfFrame();
        GameStateManager.Instance.InitializeMap(mapID);

    }
}

public class MapData
{
    public GameObject prefab;
    public string mapName;
    public int mapID;

    public Dictionary<int, MapEntryPoint> entryPoints = new Dictionary<int, MapEntryPoint>();

    public MapData(GameMap config)
    {
        this.prefab = config.prefab;
        this.mapID = config.mapID;
        this.mapName = config.mapName;

        foreach (MapEntryPoint m in config.entryPoints)
        {
            entryPoints.Add(m.entryPointID, m);
        }
    }
}
