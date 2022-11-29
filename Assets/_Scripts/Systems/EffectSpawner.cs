using UnityEngine;
using Random = UnityEngine.Random;

public class EffectSpawner : MonoBehaviour {

    [SerializeField] private EffectCollectionSO effectCollection;

    private static EffectSpawner _instance;
    private void Awake() => _instance = this;

    //Public static method call relayers
    public static void SpawnBloodFX(Vector3 position) => _instance.BloodFX(position);
    
    //Local instantiates
    private void SpawnFX(GameObject prefab, Vector3 position) => Instantiate(prefab, position, Quaternion.identity);

    private void BloodFX(Vector3 position) => SpawnFX(effectCollection.bloodFX, position);
}
