using System;
using System.Collections.Generic;
using System.Text;

namespace CS78
{
  class Fahrzeug
  {
    public string Farbe { get; set; } = "unbekannt";
    public bool AllInPaket { get; set; }

    public void Deconstruct( out bool allInPaket, out string farbe)
    {
      (allInPaket, farbe) = ( AllInPaket,Farbe);
    }
  }
}
