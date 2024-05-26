using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    public Phase currentPhase = Phase.PlayerPhase;
    public List<List<Vector2>> grid = new List<List<Vector2>>();
    public Dictionary<GamePiece, Coordinates> piecePositions = new Dictionary<GamePiece, Coordinates>();
    public float _tileSize = 1.5f;

    public System.Action OnEnemyTurn;

    public enum Phase
    {
        PlayerPhase,
        EnemyPhase
    }

    public struct Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        InitializeGrid();
        InitializeGamepieces();
    }

    void InitializeGrid()
    {
        for (int i = 0; i < 10; i++)
        {
            grid.Add(new List<Vector2>());
            for (int j = 0; j < 10; j++)
            {
                grid[i].Add(new Vector2(i, j));
            }
        }
    }

    void InitializeGamepieces()
    {
        GamePiece[] gamePieces = FindObjectsOfType<GamePiece>();

        if (gamePieces.Length == 0)
        {
            Debug.Log("No gamepieces were found in the scene");
            return;
        }

        foreach(GamePiece gp in gamePieces)
        {
            piecePositions.Add(gp, new Coordinates(
                Mathf.FloorToInt(gp.transform.localPosition.x / _tileSize),
                Mathf.FloorToInt(gp.transform.localPosition.z / _tileSize)));
            Vector3 newPosition = GetPositionFromCoordinate(piecePositions[gp]);
            gp.transform.localPosition = new Vector3(newPosition.x, gp.transform.localPosition.y, newPosition.y);
            gp.Initialize();
        }
    }

    public Vector2 RequestMovement(GamePiece piece, Vector2 direction)
    {
        Vector2 newPosition = Vector2.zero;
        
        foreach(KeyValuePair<GamePiece, Coordinates> kvp in piecePositions)
        {
            if(kvp.Value.x == piecePositions[piece].x + direction.x && kvp.Value.y == piecePositions[piece].y + direction.y)
            {
                newPosition = GetPositionFromCoordinate(piecePositions[piece]);
                Debug.Log($"Another piece already occupies that space!");
                return newPosition;
            }
        }

        Coordinates newCoordinates = new Coordinates(
            Mathf.Clamp(piecePositions[piece].x + (int)direction.x, 0, grid.Count - 1), 
            Mathf.Clamp(piecePositions[piece].y + (int)direction.y, 0, grid[0].Count - 1));
        piecePositions[piece] = newCoordinates;
        return GetPositionFromCoordinate(newCoordinates);
    }

    Vector2 GetPositionFromCoordinate(Coordinates c)
    {
        int x = c.x;
        int y = c.y;
        try
        {
            Vector2 newPosition = grid[x][y] * _tileSize;
            return newPosition;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(x + ", " + y);
            x = Mathf.Clamp(x, 0, 9);
            y = Mathf.Clamp(y, 0, 9);
            return GetPositionFromCoordinate(new Coordinates(x, y));
        }
    }

    public void ExecuteEnemyTurn()
    {
        if (currentPhase == Phase.EnemyPhase)
        {
            if (OnEnemyTurn != null)
            {
                OnEnemyTurn();
            }
        }
    }

    public void EndPlayerPhase()
    {
        currentPhase = Phase.EnemyPhase;
        ExecuteEnemyTurn();
    }

    public void CheckEnemyPhase()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (var e in allEnemies)
        {
            if (e.finishedTurn == false)
            {
                return;
            }
        }

        currentPhase = Phase.PlayerPhase;
    }
}
