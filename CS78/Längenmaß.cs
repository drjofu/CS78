using System;
using System.Collections.Generic;
using System.Text;

namespace CS78
{
  class Längenmaß
  {
    public double LängeInMeter { get; set; }
    public double this[string einheit]
    {
      get => einheit switch
      {
        "m" => LängeInMeter,
        "cm" => LängeInMeter * 100,
        "inch" => LängeInMeter * 100 / 2.54,
        _ => throw new ApplicationException("unbekannte Einheit")
      };
    }
  }
}

