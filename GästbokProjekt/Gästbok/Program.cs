using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    // Lista för att hålla reda på gästbokens inlägg
    static List<Inlägg> gästbokInlägg = new List<Inlägg>();

    static void Main()
    {
        // Ladda tidigare sparade inlägg när programmet startar
        LoadData();

        while (true)
        {
            Console.Clear(); // Rensa konsolen för att skapa en ren vy
            SkrivMeny(); // Visa huvudmenyn
            HanteraMenyVal(Console.ReadLine()); // Läs in och hantera användarens menyval
        }
    }

    // Metod för att skriva ut huvudmenyn
    static void SkrivMeny()
    {
        Console.WriteLine("Välkommen till min Gästbok!");
        Console.WriteLine("1. Lägg till ett inlägg");
        Console.WriteLine("2. Ta bort ett inlägg");
        Console.WriteLine("3. Visa alla inlägg");
        Console.WriteLine("0. Avsluta");
    }

    // Metod för att hantera användarens menyval
    static void HanteraMenyVal(string val)
    {
        switch (val)
        {
            case "1":
                LäggTillInlägg(); // Anropar metoden för att lägga till inlägg
                break;
            case "2":
                TaBortInlägg(); // Anropar metoden för att ta bort inlägg
                break;
            case "3":
                VisaAllaInlägg(); // Anropar metoden för att visa alla inlägg
                break;
            case "0":
                SaveData(); // Sparar inläggen innan programmet avslutas
                Environment.Exit(0); // Avslutar programmet
                break;
            default:
                Console.WriteLine("Ogiltigt val. Försök igen.");
                break;
        }

        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    // Metod för att lägga till ett nytt inlägg
    static void LäggTillInlägg()
    {
        Console.Write("Ange författare till inlägget: ");
        string ägare = Console.ReadLine();

        Console.Write("Ange texten till inlägget: ");
        string text = Console.ReadLine();

        // Felhantering för att kontrollera om något av fälten är tomt
        if (string.IsNullOrEmpty(ägare) || string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Båda fälten måste vara ifyllda. Inlägget har inte lagts till.");
            return;
        }

        // Skapa ett nytt Inlägg-objekt och lägg till det i listan
        Inlägg nyttInlägg = new Inlägg { Ägare = ägare, Text = text };
        gästbokInlägg.Add(nyttInlägg);

        Console.WriteLine("Inlägget har lagts till i gästboken!");
    }

    // Metod för att ta bort ett inlägg
    static void TaBortInlägg()
    {
        VisaAllaInlägg(); // Visa alla inlägg för användaren att välja från

        Console.Write("Ange index på inlägget att ta bort: ");
        if (int.TryParse(Console.ReadLine(), out int valtIndex))
        {
            // Felhantering för ogiltiga index
            if (valtIndex >= 0 && valtIndex < gästbokInlägg.Count)
            {
                gästbokInlägg.RemoveAt(valtIndex);
                Console.WriteLine("Inlägget har tagits bort från gästboken.");
            }
            else
            {
                Console.WriteLine("Ogiltigt index. Inlägget har inte tagits bort.");
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt index. Inlägget har inte tagits bort.");
        }
    }

    // Metod för att visa alla inlägg
    static void VisaAllaInlägg()
    {
        Console.WriteLine("----- Alla Inlägg -----");
        for (int i = 0; i < gästbokInlägg.Count; i++)
        {
            Console.WriteLine($"[{i}] {gästbokInlägg[i].Ägare}: {gästbokInlägg[i].Text}");
        }
        Console.WriteLine("------------------------");
    }

    // Metod för att spara inlägg till fil
    static void SaveData()
    {
        string jsonData = JsonSerializer.Serialize(gästbokInlägg);
        File.WriteAllText("gästbok.json", jsonData);
    }

    // Metod för att ladda tidigare sparade inlägg från fil
    static void LoadData()
    {
        try
        {
            string jsonData = File.ReadAllText("gästbok.json");
            gästbokInlägg = JsonSerializer.Deserialize<List<Inlägg>>(jsonData);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ingen tidigare sparad data hittades. En ny gästbok har skapats.");
        }
    }
}

// Klass för att representera ett inlägg i gästboken
class Inlägg
{
    public string Ägare { get; set; }
    public string Text { get; set; }
}



