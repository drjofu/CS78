using System;
using System.Collections.Generic;
using System.Text;

namespace CS78
{
class Apfel : IDisposable
{
    public string? Farbe { get; set; } 

  public void Dispose()
  {
    Console.WriteLine($"Apfel [{Farbe}] kompostiert");
  }
}
}
