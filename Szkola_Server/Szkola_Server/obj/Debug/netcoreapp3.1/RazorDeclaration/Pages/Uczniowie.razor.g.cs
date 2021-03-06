// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Szkola_Server.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Szkola_Server;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Szkola_Server.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\_Imports.razor"
using Npgsql;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/uczniowie")]
    public partial class Uczniowie : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "C:\Users\PC\source\repos\Szkola_Server\Szkola_Server\Pages\Uczniowie.razor"
       
    private List<uczniowie> uczniowieTabela;
    private NpgsqlConnection connection;
    private NpgsqlDataReader reader;
    private string labelID;
    private string labelImie;
    private string labelNazwisko;
    private string labelDOB;
    private string labelSrednia;
    private string labelKlasa;

    private string message = "";

    // ????czenie si?? z baz?? sql poprzez u??ycie NPGSql
    protected override async Task OnInitializedAsync()
    {
        try
        {
            labelID = "";
            labelImie = "";
            labelNazwisko = "";
            labelDOB = "";
            labelSrednia = "";
            labelKlasa = "";
            uczniowieTabela = new List<uczniowie>();
            connection = new NpgsqlConnection("Host=localhost;Database=szkola;Username=postgres;Password=czarnykot;Persist Security Info=True");
            connection.Open();
            querry("SELECT * FROM Uczniowie");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    /// <summary>
    /// Metoda wysy??aj??ca kod SQL z instrukcjami co do servera SQL. Mo??e prosi?? o dane, tworzy?? nowe tabele
    /// usuwa?? albo edytowa??
    /// </summary>
    /// <param name="querry">Linia zawieraj??ca kod SQLa</param>
    protected void querry(string querry) {
        try
        {
            uczniowieTabela.Clear();
            NpgsqlCommand cmd = new NpgsqlCommand(querry, connection);
            reader = cmd.ExecuteReader();

            // Je??eli reader dosta?? informacje spowrotem kod przekszta??ca dane w tabele
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    uczniowie st = new uczniowie();
                    st.id = Int32.Parse(reader[0].ToString());
                    st.imie = reader[1].ToString();
                    st.nazwisko = reader[2].ToString();
                    st.dob = DateTime.Parse(reader[3].ToString());
                    st.srednia = Decimal.Parse(reader[4].ToString());
                    st.klasa = Int32.Parse(reader[5].ToString());
                    uczniowieTabela.Add(st);
                }
                StateHasChanged();
            }
            reader.Close();
        }
        catch (Exception e)
        {
            message = "Querry: " + querry + " " + e.Message;
            Console.WriteLine(e.Message);
        }
    }

    // Wyszukaj najwi??kszej liczby ID i j?? oddaje. Do wykorzystania podczas tworzenia nowego wpisu do tabeli
    public int getMaxID() {
        try
        {
            int maxID = 0;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT Max(id) FROM uczniowie", connection);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    maxID = Int32.Parse(reader[0].ToString());
                }
            }
            reader.Close();
            return maxID;
        }
        catch (Exception e)
        {
            message = e.Message;
            return 1;
        }
    }

    // Wyszukiwanie informacji na podstawie wpisanych danych przez u??ytkownika.
    // Metoda tworzy kod SQL jako 'stringQuerry' i wysy??a kod do querry()
    protected void search()
    {
        bool flaga = false;
        string stringQuerry = "SELECT * FROM uczniowie";
        if (!labelID.Equals("")) {
            stringQuerry += $" WHERE id='{Int32.Parse(labelID)}' ";
            flaga = true;
        }
        if (!labelImie.Equals("")) {
            if (flaga)
            {
                stringQuerry += $"AND imie='{labelImie}' ";
            }
            else {
                stringQuerry += $" WHERE imie='{labelImie}' ";
                flaga = true;
            }
        }
        if (!labelNazwisko.Equals(""))
        {
            if (flaga)
            {
                stringQuerry += $"AND nazwisko='{labelNazwisko}' ";
            }
            else
            {
                stringQuerry += $" WHERE nazwisko='{labelNazwisko}' ";
                flaga = true;
            }
        }
        if (!labelDOB.Equals(""))
        {
            if (flaga)
            {
                stringQuerry += $"AND dob='{Convert.ToDateTime(labelDOB).Date}' ";
            }
            else
            {
                stringQuerry += $" WHERE dob='{Convert.ToDateTime(labelDOB).Date}' ";
                flaga = true;
            }
        }
        if (!labelSrednia.Equals(""))
        {
            if (flaga)
            {
                stringQuerry += $"AND srednia='{float.Parse(labelSrednia)}' ";
            }
            else
            {
                stringQuerry += $" WHERE srednia='{float.Parse(labelSrednia)}' ";
                flaga = true;
            }
        }
        if (!labelKlasa.Equals(""))
        {
            if (flaga)
            {
                stringQuerry += $"AND klasa='{Int32.Parse(labelKlasa)}' ";
            }
            else
            {
                stringQuerry += $" WHERE klasa='{Int32.Parse(labelKlasa)}' ";
                flaga = true;
            }
        }
        querry(stringQuerry);
    }

    // Metoda kt??ra edytuje wpisane ju?? informacje na podstanie ID 'WHERE id={labelID}'
    public void update() {
        string stringQuerry;
        if (!labelID.Equals("") && !labelImie.Equals("") && !labelNazwisko.Equals("") && !labelDOB.Equals("") && !labelSrednia.Equals("") && !labelKlasa.Equals(""))
        {
            stringQuerry = $"UPDATE uczniowie SET imie = '{labelImie}', nazwisko = '{labelNazwisko}', " +
                    $"dob = '{labelDOB}', srednia = '{float.Parse(labelSrednia)}', klasa = '{Int32.Parse(labelKlasa)}' " +
                    $"WHERE id = '{Int32.Parse(labelID)}'";
            querry(stringQuerry);
            querry("SELECT * FROM Uczniowie");
        }
        else {
            message = "Uzupelnij wszystkie warunki!";
        }
    }

    // Stw??rz nowy wpis do tabeli
    public void create() {
        if (!labelImie.Equals("") && !labelNazwisko.Equals("") && !labelDOB.Equals("") && !labelSrednia.Equals("") && !labelKlasa.Equals("")) {
            int maxID = getMaxID();
            querry($"INSERT INTO uczniowie VALUES ('{maxID + 1}', '{labelImie}', '{labelNazwisko}', '{labelDOB}', " +
                $"'{float.Parse(labelSrednia)}', '{Int32.Parse(labelKlasa)}')");
            querry("SELECT * FROM Uczniowie");
        }
    }

    // Usu?? wpis z tabeli
    public void delete() {
        if (!labelID.Equals("")) {
            querry($"DELETE FROM uczniowie WHERE id='{Int32.Parse(labelID)}'");
            querry("SELECT * FROM Uczniowie");
        }
    }

    // Klasa przechowuje dane z tabeli
    public class uczniowie
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public DateTime dob { get; set; }
        public decimal srednia { get; set; }
        public int klasa { get; set; }
        public bool[] checkIfUsed;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
