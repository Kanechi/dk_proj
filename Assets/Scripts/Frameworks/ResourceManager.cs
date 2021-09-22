using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ResourceRequestExtenion
{
    // Resources.LoadAsyncの戻り値であるResourceRequestにGetAwaiter()を追加する
    public static TaskAwaiter<Object> GetAwaiter(this ResourceRequest resourceRequest)
    {
        var tcs = new TaskCompletionSource<Object>();
        resourceRequest.completed += operation => {
            // ロードが終わった時点でTaskCompletionSource.TrySetResult
            tcs.TrySetResult(resourceRequest.asset);
        };
        // TaskCompletionSource.Task.GetAwaiter()を返す
        return tcs.Task.GetAwaiter();
    }
}

/// <summary>
/// リソース管理
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    // リソースマップ
    private Dictionary<string, AsyncOperationHandle> resourceMap_ = new Dictionary<string, AsyncOperationHandle>();

    /// <summary>
    /// リソースの取得
    /// </summary>
    public Ty Get<Ty>(string path) where Ty : UnityEngine.Object {

        if (resourceMap_.ContainsKey(path) == false)
            return null;

        return resourceMap_[path].Result as Ty;
    }

    /// <summary>
    /// リソースのクリア
    /// </summary>
    public void Clear()
    {
        foreach (var handle in resourceMap_)
        {
            Addressables.Release(handle.Value);
        }

        resourceMap_.Clear();
    }

    public void Unload(string path)
    {
        if (resourceMap_.ContainsKey(path) == false)
            return;

        Addressables.Release(resourceMap_[path]);

        resourceMap_.Remove(path);
    }

    /// <summary>
    /// 非同期読み込み
    /// Addressable Asset System のみ
    /// </summary>
    /// <typeparam name="Ty"></typeparam>
    /// <param name="path"></param>
    /// <param name="completed"></param>
    public async Task LoadAsync<Ty>(string path, UnityAction<Ty> completed) where Ty : UnityEngine.Object
    {
        var res = Get<Ty>(path);

        if (res != null) {
            completed?.Invoke(res);
        }

        var handle = Addressables.LoadAssetAsync<Ty>(path);

        await handle.Task;

        if (handle.IsDone == true && handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null) {
            resourceMap_.Add(path, handle);
            completed?.Invoke(handle.Result);
            return;
        }

#if false
        var request = Resources.LoadAsync<Ty>(path);
        await request;

        if (request.asset != null)
        {
            resourceMap_.Add(path, request.asset);
            completed?.Invoke(request.asset as Ty);
            return;
        }
#endif
    }


}
