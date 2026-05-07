using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 5;
    [SerializeField] private List<Item> _spawnList;
    private float _nextSpawnTime;
    private float _cronoTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        _cronoTime += Time.deltaTime;
        if (_cronoTime > _nextSpawnTime)
        {
            SpawnTime();
            ResetTime();
        }

    }

    private void ResetTime()
    {
        _cronoTime = 0;
        _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void SpawnTime()
    {
        //Random object from a list
        int index = Random.Range(0, _spawnList.Count);

        //Random horizontal Position
        float xPos = Random.Range(-7f, 7f);
        Vector3 ItemPosition = new Vector2(xPos, transform.position.y);

        //Instantiation
        Item newItem = Instantiate(_spawnList[index], ItemPosition, Quaternion.identity);

        //Add Rotation Force 
        float torqueforce = Random.Range(-70f, 70f);
        newItem.GetComponent<Rigidbody2D>().AddTorque(torqueforce);

        //Dififulty Progession
        if(_maxSpawnTime  > _minSpawnTime) 
        _maxSpawnTime -= 0.1f;

    }

}
