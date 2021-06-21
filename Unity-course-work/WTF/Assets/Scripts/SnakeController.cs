using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0, 3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    //[Range(0, 4)]
    public float Speed = 0.03f;
    private Transform _transform;
    private int k = 0;

    public GameObject[] FoodPrefabs;

    public UnityEvent OnEat;
    public UnityEvent OnCrash;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angel = Input.GetAxis("Horizontal") * 2.5f;
        _transform.Rotate(0, angel, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 prevPos = _transform.position;
        foreach (var bone in Tails)
        {
            if ((bone.position - prevPos).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = prevPos;
                prevPos = temp;
            }
            else
                break;
        }
        _transform.position = newPosition;
    }

    private int s = 0;
    private float x, z;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            var bone = Instantiate(BonePrefab);
            if (k < 3)
            {
                bone.tag = "Untagged";
                k += 1;
            }
            s += 1;
            GameManager.Singleton.SetScore(s);
            Tails.Add(bone.transform);
            Speed += 0.002f;
            x = Random.Range(-17, 17);
            z = Random.Range(8, 43);
            Instantiate(FoodPrefabs[Random.Range(0,FoodPrefabs.Length)], new Vector3(x, 0.456f, z), transform.rotation);
            if (OnEat != null)
                OnEat.Invoke();
        }
        if (collision.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag == "Bone")
        {
            SceneManager.LoadScene(2);
            if (OnCrash != null)
                OnCrash.Invoke();
        }
    }
}
