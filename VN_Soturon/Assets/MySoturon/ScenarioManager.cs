using Cysharp.Threading.Tasks;
using UnityEngine;
using ScenarioFlow;
using ScenarioFlow.Scripts;
using ScenarioFlow.Tasks;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private ScenarioScript scenarioScript;  // story.sftxt 等をアサイン
    [SerializeField] private Button nextButton;              // Next ボタンをアサイン
    [SerializeField] private TextMeshProUGUI textUI;         // 表示先の Text (TMP) など

    private ScenarioTaskExecutor executor;
    private ScenarioBookReader reader;

    private async void Start()
    {
        // 1) Executorを作る (INextNotifier, ICancellationNotifier)
        executor = new ScenarioTaskExecutor(
            new ButtonNotifier(nextButton),
            new NoCancellationNotifier()  // キャンセル不要ならこれでOK
        );

        // 2) コマンドやデコーダの登録
        var publisher = new ScenarioBookPublisher(
            new IReflectable[]
            {
                // デコーダ
                new CancellationTokenDecoder(executor),
                new PrimitiveDecoder(),
                // コマンド
                // ※ DialogueWriter を MonoBehaviourにしているなら new じゃなく、GetComponent などで拾う場合も
                new DialogueWriter { textUI = textUI },
            }
        );

        // 3) ScenarioScript → ScenarioBook に変換
        var scenarioBook = publisher.Publish(scenarioScript);

        // 4) Readerを作る
        reader = new ScenarioBookReader(executor);

        // 5) 実行
        Debug.Log("Story started");
        await reader.ReadAsync(scenarioBook, this.GetCancellationTokenOnDestroy());
        Debug.Log("Story finished");
    }

    private void OnDestroy()
    {
        // 後始末
        executor?.Dispose();
    }
}
