using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;      // CancellationToken がある
using ScenarioFlow;
using ScenarioFlow.Tasks;   // ICancellationTokenDecoder がある

public class CancellationTokenDecoder : IReflectable
{
    private readonly ICancellationTokenDecoder decoder;

    public CancellationTokenDecoder(ICancellationTokenDecoder decoder)
    {
        this.decoder = decoder;
    }

    [DecoderMethod]
    public CancellationToken ConvertToCancellationToken(string input)
    {
        return decoder.Decode(input);
    }
}

