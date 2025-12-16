using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Net.Security;
using System.Reflection.Metadata;

SqlConnection connection = new SqlConnection("Server=DESKTOP-SNIONM0\\SQLEXPRESS;Database=PizzaMizzaDB;Trusted_Connection=True;TrustServerCertificate=true");
connection.Open();
evvel:
Console.WriteLine("----------------PIZZAMIZZA RESTORANINA XOS GELMISIZ----------------");
Console.WriteLine("-----------------------------1.Admin-----------------------------");
Console.WriteLine("-----------------------------2.Musteri-----------------------------");
Console.Write("Seciminizi edin: ");
int secim = Convert.ToInt32(Console.ReadLine());
Console.Clear();
switch (secim)
{
    case 1:
    paroladmin:
        Console.Write("Admin Parolunuzu daxil edin: ");
        string? adminParol = Console.ReadLine();
        if(adminParol == "admin123")
        {
        adminevvel:
            Console.WriteLine("-------------------------------MENYU-------------------------------");
            Console.WriteLine("-----------------------------1.Pizzalar-----------------------------");
            Console.WriteLine("-----------------------2.Yeni Pizza elave et------------------------");
            Console.WriteLine("-----------------------3.Pizzani menyudan sil-----------------------");
            Console.Write("Secim et: ");
            string? secim2 = Console.ReadLine();
            Console.Clear();
            switch (secim2)
            {
                case "1":
                    {
                        FullData(connection);
                        break;
                    }

                case "2":
                    {
                        Console.Write("Pizzanin adini daxil et: ");
                        string? name = Console.ReadLine();
                    priceInput:
                        Console.Write("Qiymeti daxil et: ");
                        string? priceInput = Console.ReadLine();
                        var isParsedPrice = decimal.TryParse(priceInput, out decimal price);
                        if (!isParsedPrice)
                        {
                            Console.WriteLine("Duzgun qiymet daxil et.");
                            goto priceInput;
                        }

                    CountInput:
                        Console.Write("Ingredient sayı: ");
                        string? countInput = Console.ReadLine();
                        var isParsedCount = int.TryParse(countInput, out int count);
                        if (!isParsedCount)
                        {
                            Console.WriteLine("Duzgun ingredient sayi daxil et.");
                            goto CountInput;
                        }
                        else 
                        {
                            SqlCommand Insertcommand = new SqlCommand("INSERT INTO Pizzas  VALUES (@name,@price,@count)", connection);
                            Insertcommand.Parameters.AddWithValue("@name", name);
                            Insertcommand.Parameters.AddWithValue("@price", price);
                            Insertcommand.Parameters.AddWithValue("@count", count);
                            int insertResult = Insertcommand.ExecuteNonQuery();

                        if (insertResult == 0)
                        {
                            Console.WriteLine("Pizza elave olunmadi");
                        }
                        else
                        {
                            Console.WriteLine("Pizza ugurla elave olundu ");
                        }
                        }

                        break;
                    }

                case "3":
                    {
                    Sil:
                        Console.Write("Menyudan cixartmaq istediyin pizzanin ID: ");
                        string? idInput = Console.ReadLine();
                        var isParsedId = int.TryParse(idInput, out int id);
                        if (!isParsedId)
                        {
                            Console.WriteLine("Duzgun ID daxil et.");
                            break;
                        }
                        else
                        {
                            SqlCommand Deletecommand = new SqlCommand($"DELETE FROM Pizzas WHERE Id={id}", connection);

                        int result = Deletecommand.ExecuteNonQuery();

                        if (result == 0)
                            {
                            Console.WriteLine("Yanlis Id.");
                                goto Sil;
                            }

                        else
                            Console.WriteLine("Pizza silindi ");
                            FullData(connection);
                        }

                        break;
                    }
                default:
                    Console.WriteLine("Bele bir secim yoxdur duzgun secim daxil et!");
                    Console.WriteLine();
                    goto adminevvel;
            }
        }
        else
        {
            Console.WriteLine("Yanlis parol daxil etdiniz!");
        goto paroladmin;
        }
        break;
    case 2:
        FullData(connection);
        break;
    default:
        Console.WriteLine("Bele Secim yoxdur yeniden daxil et!");
        Console.WriteLine();
        goto evvel;
}
davam:
Console.Write("Emeliyyata davam etmek isteyirsen(y/n):");
string? davamedim = Console.ReadLine();
if (davamedim == "y" || davamedim == "Y" || davamedim == "n" || davamedim == "N")
{
    if (davamedim == "y" || davamedim == "Y")
    {
        Console.Clear();
        goto evvel;
    }
    else
    {
        connection.Close();
    }
}
else
{
    Console.WriteLine("Duzgun secim edin!");
    goto davam;
}

static void FullData(SqlConnection connection)
    {
        SqlCommand Fullcommdand = new SqlCommand("SELECT * FROM Pizzas", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(Fullcommdand);
        DataSet ds = new();
        adapter.Fill(ds);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"{row["Id"]}.{row["Name"]} {row["Price"]} $, Ingredinen sayi:{row["IngredientCount"]}");
        }
        return;
    }