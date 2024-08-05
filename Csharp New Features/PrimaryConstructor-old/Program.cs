public class Person
{
    public string FirstName { get; }
    public string LastName { get; }
    public int BirthYear { get; }

    public Person(string firstName, string lastName, int birthYear)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthYear = birthYear;
    }

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
