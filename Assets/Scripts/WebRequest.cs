using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// APIへのリクエスト実行
/// </summary>
/// <typeparam name="T">リクエストにより取得するパラメータ群の型</typeparam>
public class WebRequest<T> : UnityWebRequest
{
    private const string webApiUrl = "API設置予定のURL";
    public T requestedObject = default;

    /// <summary>リクエストをコンストラクタで作成</summary>
    /// <param name="requestMethod">呼び出すAPI関数名</param>
    public WebRequest(string requestMethod) {
        APIRequest(requestMethod);
    }

    /// <summary>
    /// リクエスト結果を取得 .. 成功:取得したオブジェクトを返す / 失敗:警告とデフォルト値
    /// </summary>
    public T GetResult()
    {
        if (EqualityComparer<T>.Default.Equals(requestedObject,default(T)))
            Debug.LogWarning("データが未取得またはリクエストエラーです");
        return requestedObject;
    }

    // APIリクエスト部
    private IEnumerator APIRequest(string method)
    {
        UnityWebRequest request = UnityWebRequest.Post(webApiUrl, method);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        requestedObject = JsonUtility.FromJson<T>(request.downloadHandler.text);
    }
}
