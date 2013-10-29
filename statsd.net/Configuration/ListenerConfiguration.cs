﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statsd.net.Configuration
{
  public class ListenerConfiguration
  {
  }

  public class UDPListenerConfiguration : ListenerConfiguration
  {
    public int Port { get; set; }
    public UDPListenerConfiguration(int port)
    {
      Port = port;
    }
  }

  public class TCPListenerConfiguration : ListenerConfiguration
  {
    public int Port { get; set; }
    public TCPListenerConfiguration(int port)
    {
      Port = port;
    }
  }

  public class HTTPListenerConfiguration : ListenerConfiguration
  {
    public int Port { get; set; }
    public string HeaderKey { get; set; }
    public HTTPListenerConfiguration(int port, string headerKey = null)
    {
      Port = port;
      HeaderKey = headerKey;
    }
  }
}