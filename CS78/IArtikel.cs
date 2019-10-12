namespace CS78
{
  interface IArtikel
  {
    string Bezeichnung { get; set; }
    int Id { get; set; }
    decimal Preis { get; set; }

    decimal USt => Preis * 0.19m;

  }
}