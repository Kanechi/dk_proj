using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

using UniRx;
using System.Collections;
using UnityEngine.Networking;

using MiniJSON;
using UniRx.WebRequest;
using UnityEngine.Events;


/// <summary>
/// リクエスト用レコードテーブル
/// </summary>
/// <typeparam name="RecordT"></typeparam>
public abstract class RequestRecordTable<RecordT> : IRecordTable<RecordT>
{
    protected List<RecordT> m_recordList = new List<RecordT>();

    public List<RecordT> RecordList => m_recordList;

    public abstract RecordT Get(uint key);
}

/// <summary>
/// リクエスト用レコードテーブル解析用ビルダーインターフェース
/// </summary>
/// <typeparam name="RecordT"></typeparam>
public interface IRecordTableRequestPaserBuilder<RecordT> {
    RequestRecordTable<RecordT> GetResult();
    void Create();
    bool Import(string response);
}

/// <summary>
/// Json 解析リクエストビルダー
/// </summary>
/// <typeparam name="RecordT"></typeparam>
/// <typeparam name="ResponseT"></typeparam>
public abstract class JsonRequestPaserBuilder<RecordT, ResponseT> : IRecordTableRequestPaserBuilder<RecordT> 
    where RecordT : IJsonParser, new()
    where ResponseT : JsonRequestPaserBuilder<RecordT, ResponseT>.IRootResponse, new()
{
    public interface IRootResponse : IJsonParser { 
        IList<RecordT> Root { get;  }
    }

    protected RequestRecordTable<RecordT> m_dataTable = null;
    public RequestRecordTable<RecordT> GetResult() => m_dataTable;
    public abstract void Create();
    public bool Import(string response) {
        var res = Json.Deserialize(response) as Dictionary<string, object>;
        var rootResponse = new ResponseT();
        rootResponse.Parse(res);
        foreach (var data in rootResponse.Root)
        {
            m_dataTable.RecordList.Add(data);
        }
        return true;
    }
}

/// <summary>
/// リクエストレコードテーブル用ディレクター
/// </summary>
public class RecordTableRequestDirector<RecordT>
{
    private IRecordTableRequestPaserBuilder<RecordT> m_builder = null;

    protected IEnumerator m_enumerator = null;

    public RecordTableRequestDirector(IRecordTableRequestPaserBuilder<RecordT> builder) => m_builder = builder;

    public void SendRequest(string url, UnityAction<RequestRecordTable<RecordT>> completed)
    {
        ObservableWebRequest.GetAndGetBytes(url).Subscribe(
            download =>
            {
                var response = System.Text.Encoding.UTF8.GetString(download);
                if (string.IsNullOrEmpty(response) == true)
                {
                    Debug.Log("download error");
                }
                m_builder.Create();
                m_builder.Import(response);
                completed?.Invoke(m_builder.GetResult());
            });
    }
}

