using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNotifier : INextNotifier
{
    private readonly Button nextButton;

    public ButtonNotifier(Button button)
    {
        this.nextButton = button;
    }

    public UniTask NotifyNextAsync(CancellationToken ct)
    {
        // ボタンが押されるまで待つ
        // ここでは単純にUnityEventを待つ方法をUniTask化している例
        return nextButton.OnClickAsync(ct);
    }
}
