using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    private Vector2 moveInput;
    private float moveTime = 0.1f;
    private int moveSize = 2;
    private bool isMoving = false;
    private bool isPlacing = false;
    private float delayTime = 0.01f;
    private List<GameObject> blocks = new List<GameObject>();
    private GameObject blockContainer;
    public GameObject block;
    
    void Start() {
        blockContainer = GameObject.Find("Blocks");  
    }
    void Update()
    {
        moveInput = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.X)) && !isPlacing) {
            isPlacing = true;
            PlaceBlock(block, transform);
        }
        if (!isMoving) {
            if (moveInput.y > 0) {
                StartCoroutine(Move("up", moveTime));
            }
            else if (moveInput.y < 0) {
                StartCoroutine(Move("down", moveTime));            
            }
            else if (moveInput.x > 0) {
                // StartCoroutine(Move("right", moveTime));
                StartCoroutine(Rotate("right", moveTime));
            }
            else if (moveInput.x < 0) {
                // StartCoroutine(Move("left", moveTime)); 
                StartCoroutine(Rotate("left", moveTime));
            }
            else if (Input.GetKey(KeyCode.Space)) {
                StartCoroutine(Move("forward", moveTime));
            }
            else if (Input.GetKey(KeyCode.LeftShift)) {
                StartCoroutine(Move("backward", moveTime));            
            }
            else if (Input.GetKey(KeyCode.Q)) {
                StartCoroutine(Rotate("left", moveTime));
            }
            else if (Input.GetKey(KeyCode.E)) {
                StartCoroutine(Rotate("right", moveTime));
            }
            else if (Input.GetKey(KeyCode.Z)) {
                StartCoroutine(Rotate("backwards", moveTime));
            }
            else if (Input.GetKey(KeyCode.C)) {
                StartCoroutine(Rotate("forwards", moveTime));
            }
        }
    }
    private IEnumerator Delay(float _delayTime) {
        float t = 0;
        while (t <= 1) {
            t += 0.1f;
            yield return new WaitForSeconds(_delayTime);
        }
        isMoving = false;
        yield return null;
    }
    private IEnumerator Move(string direction, float time) {
        isMoving = true;
        isPlacing = true;
        Vector3 oldPos = transform.position;
        Vector3 moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z);
        if (direction == "forward") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.forward * moveSize);
        }
        else if (direction == "backward") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.forward * -1 * moveSize);
        }
        else if (direction == "right") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.right * moveSize);
        }
        else if (direction == "left") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.right * -1 * moveSize);
        }
        else if (direction == "up") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.up * moveSize);
        }
        else if (direction == "down") {
            moveVector = new Vector3(oldPos.x, oldPos.y, oldPos.z) + (transform.up * -1 * moveSize);
        }
        float t = 0;
        while (t <= 1) {
            t += time;
            transform.position = Vector3.Lerp(oldPos, moveVector, t);
            yield return new WaitForSeconds(0.01f);
        }
        isPlacing = false;
        StartCoroutine(Delay(delayTime));
        Debug.Log(transform.position);
        yield return null;
    }
    private IEnumerator Rotate(string direction, float time) {
        isMoving = true;
        isPlacing = true;
        Vector3 oldRotation = transform.eulerAngles;
        Vector3 newRotation = new Vector3(oldRotation.x,oldRotation.y,oldRotation.z);
        if (direction == "left") {
            newRotation = new Vector3(oldRotation.x, oldRotation.y - 90, oldRotation.z);
        }
        else if (direction == "right") {
            newRotation = new Vector3(oldRotation.x, oldRotation.y + 90,oldRotation.z);
        }
        else if (direction == "backwards") {
            newRotation = new Vector3(oldRotation.x - 90, oldRotation.y, oldRotation.z);
        } 
        else if (direction == "forwards") {
            newRotation = new Vector3(oldRotation.x + 90, oldRotation.y ,oldRotation.z);
        }
        float t = 0;
        while (t <= 1) {
            t += time;
            transform.eulerAngles = Vector3.Lerp(oldRotation, newRotation, t);
            yield return new WaitForSeconds(0.01f);
        }
        isPlacing = false;
        StartCoroutine(Delay(delayTime));
        yield return null;
    }
    private Vector3 RoundVector3(Vector3 originalVector) {
        Vector3 newVector = new Vector3(Mathf.RoundToInt(originalVector.x), Mathf.RoundToInt(originalVector.y), Mathf.RoundToInt(originalVector.z));
        return newVector;
    }
    private void PlaceBlock(GameObject _block, Transform playerPosition) {
        GameObject Block = Instantiate(_block, playerPosition.position, Quaternion.identity, blockContainer.transform);
        Block.name = "Block";
        blocks.Add(Block);
    }
}
