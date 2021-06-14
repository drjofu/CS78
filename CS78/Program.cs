using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS78
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Tuples();
      //PatternMatching();
      //RefPointer();
      //SpanOfT();
      //InterfaceExtension();
      //SwitchExpression();
      //CS8Pattern();
      //UsingDeclarations();
      //await AsyncEnumerables();
      IndicesAndRanges();
      //NullCoalescingAssignment();
      ///StackAllocSpan();
      //Console.ReadLine();
    }

    private static void StackAllocSpan()
    {
      Span<int> zahlen = stackalloc int[] { 1, 34, 6, 2, 743 };
      Span<int> z = stackalloc int[1_000_000];  // -> StackOverflowException
    }

    private static void NullCoalescingAssignment()
    {
      string text = null;

      text ??= "Hallo";
      Console.WriteLine(text);
      text ??= "Welt";
      Console.WriteLine(text);
    }

    private static void IndicesAndRanges()
    {
      string[] farben = { "rot", "grün", "blau", "gelb", "lila" };
      Console.WriteLine(farben[^1]);
      Index a = ^2;
      
      Console.WriteLine(farben[a]);
      Index b = 2;
      Console.WriteLine(farben[b]);
      Console.WriteLine("---");
      foreach (var farbe in farben[2..^0])
      {
        Console.WriteLine(farbe);
      }

      Console.WriteLine("---");
      Range r = 2..5;
      foreach (var farbe in farben[r])
      {
        Console.WriteLine(farbe);
      }

      r = ..3;
      r = 2..;


      Console.WriteLine(a);
      Console.WriteLine(b);
      Console.WriteLine(r);

      Span<string> ff = farben;
      ff[0] = "pink";
      var fff = ff[1..2];
      fff[0] = "rosa";

      Console.WriteLine("Hallo Welt"[6..]);
      ReadOnlySpan<char> text = "Hallo Welt";
      Console.WriteLine(text[2..8].ToString());
      //text[0] = 'x';
      Console.WriteLine("Hallo Welt"[^3..^0]);

      // Vermeiden von Garbage
      string t = "x;y;100";
      // normal z. B. mit string.Substring oder string.Split etc.
      // ohne Heap-Alloc:
      int index = t.LastIndexOf(';');
      ReadOnlySpan<char> chars = t;
      var zahlenwert = chars[(index + 1)..];
      var wert = int.Parse(zahlenwert);

      
    }

    private static async Task AsyncEnumerables()
    {
      await foreach (var text in GetStrings())
      {
        Console.WriteLine(text);
      }

      async IAsyncEnumerable<string> GetStrings()
      {
        for (int i = 1; i <= 10; i++)
        {
          await Task.Delay(1000);
          yield return $"Text {i}";
        }
      }
    }

    //private static async IAsyncEnumerable<string> GetStrings()
    //{
    //  for (int i = 1; i <= 10; i++)
    //  {
    //    await Task.Delay(1000);
    //    yield return $"Text {i}";
    //  }
    //}


    private static void UsingDeclarations()
    {
      using var apfel1 = new Apfel { Farbe = "grün" };
      using var apfel2 = new Apfel { Farbe = "rot" };

      Console.WriteLine("Äpfel werden verarbeitet");
    }

    private static void CS8Pattern()
    {
      var fzg1 = new Fahrzeug { Farbe = "Rot", AllInPaket = false };
      var fzg2 = new Fahrzeug { Farbe = "Metallic", AllInPaket = false };
      var fzg3 = new Fahrzeug { Farbe = "Metallic", AllInPaket = true };

      Console.WriteLine(GetPreis(30000, fzg1));
      Console.WriteLine(GetPreis2(30000, fzg2));
      Console.WriteLine(GetPreis3(30000, fzg3));

      static decimal GetPreis(decimal basispreis, Fahrzeug fzg) =>
        fzg switch
        {
          { AllInPaket: true } => basispreis + 3000,
          { Farbe: "Metallic" } => basispreis + 1000,
          _ => basispreis
        };

      static decimal GetPreis2(decimal basispreis, Fahrzeug fzg) =>
        (fzg.AllInPaket, fzg.Farbe) switch
        {
          (true, _) => basispreis + 3000,
          (false, "Metallic") => basispreis + 1000,
          (_, _) => basispreis
        };

      static decimal GetPreis3(decimal basispreis, Fahrzeug fzg) =>
        (fzg.AllInPaket, fzg.Farbe) switch
        {
          (true, _) => basispreis + 3000,
          (false, "Metallic") => basispreis + 1000,
          var (_, farbe) when farbe.StartsWith("R") => basispreis + 500,
          (_, _) => basispreis
        };
    }

    private static void SwitchExpression()
    {
      Längenmaß lm = new Längenmaß { LängeInMeter = 10 };
      Console.WriteLine(lm["m"]);
      Console.WriteLine(lm["cm"]);
      Console.WriteLine(lm["inch"]);
      //Console.WriteLine(lm["km"]);
    }

    private static void InterfaceExtension()
    {
      IArtikel artikel = new Artikel { Id = 123, Bezeichnung = "Vase", Preis = 5.8m };
      Console.WriteLine(artikel.USt);
    }

    private static void SpanOfT()
    {
      var array = new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
      var slice = new Span<int>(array, 2, 5);
      for (int ctr = 0; ctr < slice.Length; ctr++)
        slice[ctr] *= 2;

      foreach (var value in array)
        Console.Write($"{value}  ");
      //      2  4  12  16  20  24  28  16  18  20
    }

    private static void RefPointer()
    {
      int[] werte = { 2314, 531, 1, 21, 6 };
      ref int x = ref GetElement(werte, 1);   // liefert Zeiger auf 3. Element
      Console.WriteLine(x);                   // -> 1
      x = 777;                                // setzt das 3. Element
      int n = GetElement(werte, 1);           // kopiert das 3. Element
      Console.WriteLine(n);                   // -> 777
    }

    private static ref int GetElement(int[] zahlen, int n)
    {
      return ref zahlen[2];
    }


    private static void PatternMatching()
    {
      object obj = new Artikel { Id = 123, Bezeichnung = "Vase", Preis = 5.8m };
      if (obj is Artikel a)
        Console.WriteLine(a.Bezeichnung);

      Artikel a1 = new Artikel();
      if (a1 is null)
        Console.WriteLine("ist null...");

      //obj = 123;
      //if (obj is 123)
      //  Console.WriteLine("ist 123");

      switch (obj)
      {
        case 100:                                                       // constant pattern
          Console.WriteLine("Wert 100");
          break;

        case Artikel art when art.Preis < 10:                           // type pattern mit Bedingung
          Console.WriteLine($"preisw. Artikel: {art.Bezeichnung}");
          break;

        case Artikel art:                                               // type pattern
          Console.WriteLine($"anderer Artikel: {art.Bezeichnung}");
          break;

        case null:                                                      // null pattern
          Console.WriteLine("ist null");
          break;

        default:
          Console.WriteLine("Default");
          break;
      }

      if(obj is Artikel { Preis: 5000,Bezeichnung: var bez})
        Console.WriteLine(bez);
    }

    private static void Tuples()
    {
      var t1 = (1, 2, "Hallo", "Welt");
      Console.WriteLine(t1.Item1);

      (int a, int b, string c, string _) t2 = (1, 2, "Hallo", "Welt");
      Console.WriteLine(t2.c);

      var t3 = (x: 7, text: "Hallo", y: 1.234);
      Console.WriteLine(t3.text);

      (int minimum, int maximum) = FindRange(10, 5, 63, 243);
      Console.WriteLine($"{minimum} - {maximum}");

      var artikel = new Artikel { Id = 123, Bezeichnung = "Vase", Preis = 5.8m };
      (int id, string name) = artikel;
      Console.WriteLine($"{id} - {name}");

      (_, int max) = FindRange(2, 4, 1, 6);
      artikel.Deconstruct(out _, out string bez);
      _ = 123;  // geht, aber sinnlos...

    }

    private static (int min, int max) FindRange(params int[] values)
    {
      int min = values.Min();
      int max = values.Max();
      return (min, max);
    }
  }
}
