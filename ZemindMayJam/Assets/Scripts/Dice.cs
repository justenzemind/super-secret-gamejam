using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice : GamePiece
{
    Sequence _movementSequence;
    public int _currentFace = 1;

    Dictionary<int, AbilityInfo> _abilities = new Dictionary<int, AbilityInfo>()
    {
        {1, null },
        {2, null },
        {3, null },
        {4, null },
        {5, null },
        {6, null }
    };

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.magenta);
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.cyan);
        //Debug.DrawLine(transform.position, transform.position + -transform.up * 10, Color.white);
        //Debug.DrawLine(transform.position, transform.position + -transform.forward * 10, Color.yellow);
        //Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.green);
        //Debug.DrawLine(transform.position, transform.position + -transform.right * 10, Color.white);
        ListenForInput();
    }

    public void AssignAbility(int index, AbilityInfo ability)
    {
        _abilities[index] = ability;
    }

    void UseAbility()
    {
        if (_abilities[_currentFace] != null)
        {
            _abilities[_currentFace]?._callback();
            return;
        }

        Debug.Log($"No ability found on {_currentFace}");
    }

    void CalculateMovement(Vector3 modifier, Vector3 newPosition)
    {
        float distance = Vector3.Distance(newPosition, transform.localPosition);
        newPosition.y = transform.position.y;

        if (Vector3.Distance(newPosition, transform.localPosition) < 1)
        {
            return;
        }

        _movementSequence = DOTween.Sequence();
        Vector3 rotateVector = new Vector3(modifier.x, 0, modifier.z);
        Quaternion axisRotation = Quaternion.AngleAxis(90f, rotateVector);
        Quaternion targetRotation = axisRotation * transform.rotation;
        _movementSequence.Append(transform.DOLocalMove(newPosition, 1));
        _movementSequence.Join(transform.DORotateQuaternion(targetRotation, 1));
        _movementSequence.onComplete = () =>
        {
            _movementSequence = null;
            for(int ii = 1; ii < 7; ii++)
            {
                DetectFace(ii);
            }
            BoardManager.instance.EndPlayerPhase();
        };
        _movementSequence.Play();
    }

    void DetectFace(int face)
    {
        switch (face)
        {
            case 1:
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 1;
                }
                break;

            case 2:
                if (Physics.Raycast(transform.position, -transform.forward, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 2;
                }
                break;

            case 3:
                if (Physics.Raycast(transform.position, -transform.right, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 3;
                }
                break;

            case 4:
                if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 4;
                }
                break;

            case 5:
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 5;
                }
                break;

            case 6:
                if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "FaceChecker")
                        _currentFace = 6;
                }
                break;
        }
    }

    void ListenForInput()
    {
        if (_movementSequence != null)
        {
            return;
        }

        if (BoardManager.instance.currentPhase != BoardManager.Phase.PlayerPhase)
        {
            return;
        }

        float horzMod = 0;
        float depthMod = 0;

        if (Input.GetKeyDown(KeyCode.D))
        {
            horzMod = -1;
            Vector2 newPosition = BoardManager.instance.RequestMovement(this, new Vector2(horzMod, depthMod));
            CalculateMovement(new Vector3(-horzMod, 0, depthMod), new Vector3(newPosition.x, 0, newPosition.y));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            horzMod = 1;
            Vector2 newPosition = BoardManager.instance.RequestMovement(this, new Vector2(horzMod, depthMod));
            CalculateMovement(new Vector3(-horzMod, 0, depthMod), new Vector3(newPosition.x, 0, newPosition.y));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            depthMod = -1;
            Vector2 newPosition = BoardManager.instance.RequestMovement(this, new Vector2(horzMod, depthMod));
            CalculateMovement(new Vector3(horzMod, 0, -depthMod), new Vector3(newPosition.x, 0, newPosition.y));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            depthMod = 1;
            Vector2 newPosition = BoardManager.instance.RequestMovement(this, new Vector2(horzMod, depthMod));
            CalculateMovement(new Vector3(horzMod, 0, -depthMod), new Vector3(newPosition.x, 0, newPosition.y));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }
    }
}
