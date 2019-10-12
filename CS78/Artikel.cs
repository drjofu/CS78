using System;
using System.Collections.Generic;
using System.Text;

namespace CS78
{
  class Artikel : IArtikel
  {
    public int Id { get; set; }
    public string Bezeichnung { get; set; }
    public decimal Preis { get; set; }

    private string dokumentation;
    public string Dokumentation
    {
      get => dokumentation;
      set => dokumentation = value ?? "unbekannt";
    }

    public Artikel(string dokumentation) => this.dokumentation = dokumentation;

    public Artikel()
    {

    }
    public void Deconstruct(out int id, out string bezeichnung)
    {
      (id, bezeichnung) = (Id, Bezeichnung);
    }
  }
}
