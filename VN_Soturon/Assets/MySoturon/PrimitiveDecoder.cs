using ScenarioFlow;

public class PrimitiveDecoder : IReflectable
{
    // "string" 用のデコーダ
    [DecoderMethod]
    public string ConvertToString(string input)
    {
        // 単純に入力された文字列をそのまま返す例。
        // (必要に応じて前後の空白除去や改行変換などを行うこともある)
        return input;
    }
}
