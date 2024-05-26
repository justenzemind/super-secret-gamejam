using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : GamePiece
{
    public bool finishedTurn = false;

    public override void Initialize()
    {
        BoardManager.instance.OnEnemyTurn += OnEnemyTurn;
    }

    void OnEnemyTurn()
    {
        Vector2 newPosition = BoardManager.instance.RequestMovement(this, Vector2.right);
        Tween t = transform.DOLocalMove(new Vector3(newPosition.x, transform.localPosition.y, newPosition.y), 1);
        t.onComplete = () =>
        {
            finishedTurn = true;
            BoardManager.instance.CheckEnemyPhase();
        };
    }
}
