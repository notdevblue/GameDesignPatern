using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject cubePrefab;

    public int width;
    public int depth;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(cubePrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new Spawner {
            Prefab = conversionSystem.GetPrimaryEntity(cubePrefab),
            Width = width,
            Depth = depth
        };

        dstManager.AddComponentData(entity, spawnerData);
    }

}
