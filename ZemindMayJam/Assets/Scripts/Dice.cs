using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    Sequence _movementSequence;
    public int _currentFace = 0;

    Dictionary<int, AbilityInfo> _abilities = new Dictionary<int, AbilityInfo>()
    {
        {1, null },
        {2, null },
        {3, null },
        {4, null },
        {5, null },
        {6, null }
    };

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider);
        }
    }

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
        _abilities[_currentFace]?._callback();
    }

    void CalculateMovement(Vector3 modifier)
    {
        _movementSequence = DOTween.Sequence();
        Vector3 currentPos = transform.position;
        Vector3 rotateVector = new Vector3(modifier.z, 0, -modifier.x);
        Quaternion axisRotation = Quaternion.AngleAxis(90f, rotateVector);
        Quaternion targetRotation = axisRotation * transform.rotation;
        _movementSequence.Append(transform.DOMove(currentPos + modifier * 1.5f, 1));
        _movementSequence.Join(transform.DORotateQuaternion(targetRotation, 1));
        _movementSequence.onComplete = () =>
        {
            _movementSequence = null;
            for(int ii = 1; ii < 7; ii++)
            {
                DetectFace(ii);
            }
            
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

        float horzMod = 0;
        float depthMod = 0;

        if (Input.GetKeyDown(KeyCode.D))
        {
            horzMod = 1;
            CalculateMovement(new Vector3(horzMod, 0, depthMod));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            horzMod = -1;
            CalculateMovement(new Vector3(horzMod, 0, depthMod));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            depthMod = 1;
            CalculateMovement(new Vector3(horzMod, 0, depthMod));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            depthMod = -1;
            CalculateMovement(new Vector3(horzMod, 0, depthMod));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }
    }
}
