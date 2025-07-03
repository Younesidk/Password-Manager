using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class StoredPassword
{
    public string Platform { get; set; }
    public string Email { get; set; }   
    public string Password { get; set; }


    public static void SaveToVault(List<StoredPassword> vault)
    {
        string json = JsonSerializer.Serialize(vault, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("C:\\Study\\C# learning\\Password Manager C#\\vault.json", json);
    }


    public static List<StoredPassword> LoadVault()
    {
        if (!File.Exists("C:\\Study\\C# learning\\Password Manager C#\\vault.json"))
            return new List<StoredPassword>();

        string json = File.ReadAllText("C:\\Study\\C# learning\\Password Manager C#\\vault.json");

        // Defensive fallback: if file is empty, return empty list
        if (string.IsNullOrWhiteSpace(json))
            return new List<StoredPassword>();

        return JsonSerializer.Deserialize<List<StoredPassword>>(json);
    }

}

class Program
{
    static void Main()
    {

        while (true)
        {       

            Console.WriteLine("PASSWORD MANAGER\n" +
                "1. Generate password\n" +
                "2. test the strength of a password\n" +
                "3. Save New Password\n" +
                "4. Show All Saved Passwords\n" +
                "5. Exit\n");

            int choice;
            while (true)
            {
                Console.Write("Enter your choice (1–5): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                    break;

                Console.WriteLine("Invalid input. Please try again.");
            }

            Console.Clear();

            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("in order to Generate a password you need to check these first (note that the more you allow the stronger the password is) :");
                        var useUpper = AskUser("use uppercase ? (y/n)");

                        var useLower = AskUser("use lowercase ? (y/n)");

                        var useDigits = AskUser("use Digits ? (y/n)");

                        var useSpecials = AskUser("use Specials ? (y/n)");

                        Console.WriteLine("Enter the desired password length:");
                        int length = int.Parse(Console.ReadLine());

                        string password = GeneratePassword(length, useUpper, useLower, useDigits, useSpecials);

                        Console.WriteLine($"your password is : {password} and length is : {password.Length}");

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter a password");
                        string password = Console.ReadLine();

                        bool Is_Password_Strong = IsStrongPassword(password);
                        if (Is_Password_Strong)
                            Console.WriteLine($"the password {password} is strong");
                        else
                            Console.WriteLine($"the password {password} is weak");

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the Platform");
                        var Platform = Console.ReadLine();

                        Console.WriteLine("Enter the email");
                        var Email = Console.ReadLine();

                        Console.WriteLine("Enter the password");
                        var Password = Console.ReadLine();

                        SaveNewPassword(Platform, Email, Password);

                        break;
                    }
                case 4:
                    {
                        ShowAllSavedPasswords();
                        break;
                    }
                case 5:
                    return;
            }

        }
    }

    static string GeneratePassword(int length, bool useUpper, bool useLower, bool useDigits, bool useSpecials)
    {
        var password = new StringBuilder();
        var ActiveChar = new StringBuilder();

        bool[] uses = { useUpper, useDigits, useSpecials };

        Func<string>[] charGenerators = new Func<string>[]
        {
            Upper,
            Digit,
            Special
        };

        ActiveChar.Append("abcdefghijklmnopqrstuvwxyz");

        for (int i = 0; i < 3; i++)
        {
            if (uses[i])
            {
                ActiveChar.Append(charGenerators[i]());
            }
        }

        var rnd = new Random();

        for (int i = 0; i < length; i++) 
        {
            password.Append(ActiveChar[rnd.Next(ActiveChar.Length)]);
        }

        return password.ToString();
    }

    static bool IsStrongPassword(string password)
    {
        int passwordLength = password.Length;

        bool UpperCase = false, LowerCase = false, Digit = false, SpecialCharacter = false, Long = false;

        for (int i = 0; i < passwordLength; i++) 
        {
            if (char.IsLower(password[i]))
                LowerCase = true;
            if (char.IsUpper(password[i]))
                UpperCase = true;
            if (char.IsDigit(password[i]))
                Digit = true;
            if (!char.IsLetterOrDigit(password[i]))
                SpecialCharacter = true;
            if (passwordLength >= 8)
                Long = true;
        }

        return (LowerCase && UpperCase && Digit && SpecialCharacter && Long);            
    }

    static void SaveNewPassword(string Platform, string Email, string Password)
    {
        var vault = StoredPassword.LoadVault();

        vault.Add(new StoredPassword
        {
            Platform = Platform,
            Email = Email,
            Password = Password
        });

        StoredPassword.SaveToVault(vault);

    }

    static void ShowAllSavedPasswords()
    {
        var vault = StoredPassword.LoadVault();

        if(vault.Count == 0)
        {
            Console.WriteLine("No passwords stored yet");
            return;
        }

        foreach (var entry in vault)
        {
            Console.WriteLine($"Platform = {entry.Platform}");
            Console.WriteLine($"Email = {entry.Email}");
            Console.WriteLine($"Password = {entry.Password}");
            Console.WriteLine("====================================");
        }    
    }

    static bool AskUser(string message)
    {
        Console.WriteLine(message);
        var GenerationOption = YesNoToLower(char.Parse(Console.ReadLine()));

        return (GenerationOption == 'y');
    }

    static char YesNoToLower(char c)
    {
        switch (c)
        {
            case 'Y': return 'y';
            case 'N': return 'n';
            default: return c;
        }
    }

    static string Upper()
    {
        //var rnd = new Random();

        //return (char)rnd.Next('A', 'Z');

        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }

    static string Lower()
    {
        //    var rnd = new Random();

        //    return (char)rnd.Next('a', 'z');

        return "abcdefghijklmnopqrstuvwxyz";
    }

    static string Digit()
    {
        //var rnd = new Random();

        //return (char)rnd.Next('0', '9');

        return "0123456789";
    }

    static string Special()
    {
        //var rnd = new Random();

        const string Specials = "!@#$%^&*()[]<>?~";

        return Specials;

        //return Specials[rnd.Next(Specials.Length)];
    }
}