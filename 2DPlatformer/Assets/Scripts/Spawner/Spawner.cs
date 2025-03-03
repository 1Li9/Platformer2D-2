using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private SpawnableObject[] _spawnableObjects;
    [SerializeField] private float _timePeriod;

    private void Start()
    {
        if (_spawnableObjects.Length == 0)
            throw new System.ArgumentOutOfRangeException(nameof(_spawnableObjects) + " length is zero");

        foreach (SpawnableObject spawnableObject in _spawnableObjects)
            spawnableObject.View.gameObject.SetActive(false);

        _timer.DoActionRepeating(() => Spawn(), _timePeriod);
    }

    private void OnEnable()
    {
        foreach (SpawnableObject spawnableObject in _spawnableObjects)
            spawnableObject.WasInteracted += Interact;
    }

    private void OnDisable()
    {
        foreach (SpawnableObject spawnableObject in _spawnableObjects)
            spawnableObject.WasInteracted -= Interact;
    }

    private void Interact(SpawnableObject spawnableObject)
    {
        spawnableObject.View.gameObject.SetActive(false);
        spawnableObject.CanTake = false;
    }

    private void Spawn()
    {
        SpawnableObject currentObject = GetRandomSpawnableObject();

        currentObject.View.gameObject.SetActive(true);
        currentObject.CanTake = true;
    }

    private SpawnableObject GetRandomSpawnableObject()
    {
        int objectIndex = Random.Range(0, _spawnableObjects.Length);

        return _spawnableObjects[objectIndex];
    }
}
