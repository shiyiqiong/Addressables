using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ABManager : MonoBehaviour
{
    private List<AsyncOperationHandle> loadHandles = new List<AsyncOperationHandle>();
    private List<GameObject> gObjects = new List<GameObject>();
    
    public void LoadRes()
    {
        //加载AB包资源
        var loadHandle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Square.prefab");
        loadHandle.Completed += h =>
        {
            //实例化AB包资源
            gObjects.Add(Instantiate(loadHandle.Result as GameObject, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity));
        };
        loadHandles.Add(loadHandle);
    }

    public void RemoveRes()
    {
        if(gObjects.Count <= 0)
            return;
        //消耗游戏对象
        Destroy(gObjects[0]);
        gObjects.RemoveAt(0);

        //是否ab包资源
        Addressables.Release(loadHandles[0]);
        loadHandles.RemoveAt(0);

    }
}
