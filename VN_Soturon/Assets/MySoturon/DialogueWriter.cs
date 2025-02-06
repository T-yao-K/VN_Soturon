using Cysharp.Threading.Tasks;
using ScenarioFlow;
using System.Threading;
using TMPro;
using UnityEngine;

public class DialogueWriter : IReflectable
{
    [SerializeField] public TextMeshProUGUI textUI;

    /// <summary>
    /// SFText側で "write text async" と書かれたコマンドを呼び出すと、こちらが実行される。
    /// </summary>
    /// <param name="message">SFTextから受け取った文字列。{…}内のテキストが入る</param>
    /// <param name="ct">キャンセル用トークン。キャンセルしたいときはここでThrowIfCancellationRequested()</param>
    [CommandMethod("write text async")]
    public async UniTask WriteTextAsync(string message, CancellationToken ct)
    {
        // 1) SFText でよく使われる "<bk>" を本物の改行 "\n" に置き換える
        //    例:  "幕末期の日本<bk>長州藩は…" → "幕末期の日本\n長州藩は…"
        message = message.Replace("<bk>", "\n");

        // 2) テキストをクリア
        textUI.text = "";

        // 3) 文字送り演出：メッセージを1文字ずつ足していく
        //    （演出不要ならループをやめて一気に代入すればOK）
        foreach (char c in message)
        {
            textUI.text += c;

            // 適度に待機して「タイプライター風」にする
            await UniTask.Delay(30, cancellationToken: ct);

            // ユーザーがキャンセル操作したら即中断
            ct.ThrowIfCancellationRequested();
        }

        // 4) もし行末でちょっと待ちたい、等があればここで処理
        // await UniTask.Delay(500, cancellationToken: ct);
    }
}

