using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow.Tasks;
using System.Threading;

public class NoCancellationNotifier : ICancellationNotifier
{
    public UniTask NotifyCancellationAsync(CancellationToken ct)
    {
        // キャンセルを一切発生させないなら、無限待ちor空の待機
        return UniTask.Never(ct);
    }
}
