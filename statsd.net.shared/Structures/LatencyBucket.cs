﻿using statsd.net.shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace statsd.net.shared.Structures
{
  public class LatencyBucket : Bucket
  {
    public KeyValuePair<string, LatencyDatapointBox>[] Latencies { get; private set; }

    public LatencyBucket(KeyValuePair<string, LatencyDatapointBox>[] latencies, 
      long epoch,
      string rootNamespace)
      : base(BucketType.Timing, epoch, rootNamespace)
    {
      Latencies = latencies;
    }

    public override void FeedTarget(ITargetBlock<GraphiteLine> target)
    {
      foreach (var latency in Latencies)
      {
        var lines = MakeGraphiteLines(latency);
        foreach (var line in lines)
        {
          target.Post(line);
        }
      }
    }

    private GraphiteLine[] MakeGraphiteLines(KeyValuePair<string, LatencyDatapointBox> latency)
    {
      return new GraphiteLine[] 
      {
        new GraphiteLine(RootNamespace + latency.Key + ".count", latency.Value.Count, Epoch),
        new GraphiteLine(RootNamespace + latency.Key + ".min", latency.Value.Min, Epoch),
        new GraphiteLine(RootNamespace + latency.Key + ".max", latency.Value.Max, Epoch),
        new GraphiteLine(RootNamespace + latency.Key + ".mean", latency.Value.Mean, Epoch),
        new GraphiteLine(RootNamespace + latency.Key + ".sum", latency.Value.Sum, Epoch),
        new GraphiteLine(RootNamespace + latency.Key + ".sumSquares", latency.Value.SumSquares, Epoch)
      };
    }

    public override string ToString()
    {
      var graphiteLines = new List<string>();
      foreach (var latency in Latencies)
      {
        var lines = MakeGraphiteLines(latency);
        foreach (var line in lines)
        {
          graphiteLines.Add(line.ToString());
        }
      }
      return String.Join(Environment.NewLine, graphiteLines.ToArray());
    }
  }
}
