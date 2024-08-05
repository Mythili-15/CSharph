public class Person(string FirstName, string LastName, int BirthYear)
{
    public string FullName => $"{FirstName} {LastName}";

    public int GetAge(int currentYear) => currentYear - BirthYear;
}

class Program
{
    static void Main()
    {
        Person person = new Person("John", "Doe", 1990);
        Console.WriteLine($"Full Name: {person.FullName}");
        Console.WriteLine($"Age: {person.GetAge(2024)}");
    }
}
