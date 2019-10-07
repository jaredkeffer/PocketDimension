using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    private Vector3 lastSelectorPos;
    private Vector3 currentSelectorPos;
    public GameObject selector;
    private GameObject _selector;
    public GameObject basicCube;
    private List<BlockData> blocks = new List<BlockData>();
    private bool alreadyExists;
    private PlayerOneController pc;
    private bool delaying = false;
    private float delayTime = 1f;
    private bool action = false;
    private float destroyTime = 0.05f;
    public bool makeCubes;
    public int player;

    void Start() {
        if (makeCubes) {
            currentSelectorPos = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            lastSelectorPos = currentSelectorPos;
            _selector = Instantiate(selector, currentSelectorPos, Quaternion.identity);
            _selector.name = "Selector";
        }
        pc = GameObject.Find("Player").GetComponent<PlayerOneController>();
        player = pc.player;
    }
    void Update() {
        if (makeCubes) {
            currentSelectorPos = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            if (currentSelectorPos != lastSelectorPos) {
                lastSelectorPos = currentSelectorPos;
                _selector.transform.position = lastSelectorPos;
                // Debug.Log(lastSelectorPos);
                // Debug.Log(cubes.Count);
                // if (Input.GetKey(KeyCode.LeftShift)) {
                // MakeObject(basicCube);
                // }
            }     
            if (player == 1) { 
                if (Input.GetKey(KeyCode.C) && !action) {
                    alreadyExists = false;
                    // action = true;
                    foreach(BlockData block in blocks) {
                        if (block.pos == lastSelectorPos) {
                            alreadyExists = true;
                            delaying = true;
                        }
                    }
                    if (!alreadyExists) {
                        MakeObject(basicCube, lastSelectorPos);
                    } else if (alreadyExists && !delaying) {
                        // pc.GameOver();
                    }
                }
                if (Input.GetKeyUp(KeyCode.C)) {
                    action = false;
                }
                if (Input.GetKey(KeyCode.LeftShift)) {
                    // foreach(BlockData block in blocks) {
                    //     Destroy(block.reference);
                    // }
                    if (!action) {
                        action = true;
                        StartCoroutine(DestroyBlocks(destroyTime));
                    }
                }
                if (Input.GetKeyUp(KeyCode.LeftShift) && action) {
                    action = false;
                    StopCoroutine(DestroyBlocks(destroyTime));
                }  
            }
            if (player == 2) { 
                if (Input.GetKey(KeyCode.M) && !action) {
                    alreadyExists = false;
                    // action = true;
                    foreach(BlockData block in blocks) {
                        if (block.pos == lastSelectorPos) {
                            alreadyExists = true;
                            delaying = true;
                        }
                    }
                    if (!alreadyExists) {
                        MakeObject(basicCube, lastSelectorPos);
                    } else if (alreadyExists && !delaying) {
                        // pc.GameOver();
                    }
                }
                if (Input.GetKeyUp(KeyCode.M)) {
                    action = false;
                }
                if (Input.GetKey(KeyCode.RightShift)) {
                    // foreach(BlockData block in blocks) {
                    //     Destroy(block.reference);
                    // }
                    if (!action) {
                        action = true;
                        StartCoroutine(DestroyBlocks(destroyTime));
                    }
                }
                if (Input.GetKeyUp(KeyCode.LeftShift) && action) {
                    action = false;
                    StopCoroutine(DestroyBlocks(destroyTime));
                }  
            }
            // if (Input.GetKey(KeyCode.Space) && !action) {
            //     alreadyExists = false;
            //     // action = true;
            //     foreach(BlockData block in blocks) {
            //         if (block.pos == lastSelectorPos) {
            //             alreadyExists = true;
            //             delaying = true;
            //         }
            //     }
            //     if (!alreadyExists) {
            //         MakeObject(basicCube, lastSelectorPos);
            //     } else if (alreadyExists && !delaying) {
            //         // pc.GameOver();
            //     }
            // }
            // if (Input.GetKeyUp(KeyCode.Space)) {
            //     action = false;
            // }
            // if (Input.GetKey(KeyCode.LeftShift)) {
            //     // foreach(BlockData block in blocks) {
            //     //     Destroy(block.reference);
            //     // }
            //     if (!action) {
            //         action = true;
            //         StartCoroutine(DestroyBlocks(destroyTime));
            //     }
            // }
            // if (Input.GetKeyUp(KeyCode.LeftShift) && action) {
            //     action = false;
            //     StopCoroutine(DestroyBlocks(destroyTime));
            // }
        }
    }
    void MakeObject(GameObject _prefab, Vector3 _selectorPos) {
        GameObject cube = Instantiate(_prefab, _selectorPos, Quaternion.identity);
        cube.name = "Block";
        blocks.Add(new BlockData("BasicCube", lastSelectorPos, cube));
        // pc.score ++;
    }
    private IEnumerator Delay(float _delayTime) {
        float t = 0;
        while (t <= 1) {
            t += 0.5f;
            yield return new WaitForSeconds(_delayTime);
        }
        delaying = false;
        yield return null;
    }
    private IEnumerator DestroyBlocks(float _destroyTime) {
        int c = blocks.Count;
        if (!action) {
            Debug.Log(action);
            StopCoroutine(DestroyBlocks(_destroyTime));
            yield break;
        }
        while (c > 0 && action) {
            Destroy(blocks[c-1].reference);
            c = c - 1;
            yield return new WaitForSeconds(_destroyTime);
        }
        yield return null;
    }
    
}
