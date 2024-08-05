using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLConsoleApp
{
    class Program
    {
        static XDocument xmlDoc;
        static string xmlFilePath = "library.xml";

        static void Main(string[] args)
        {
            InitializeXmlDocument();
            DisplayMenu();
        }

        #region XML Document Initialization and Main Method
        static void InitializeXmlDocument()
        {
            XDocument libraryXmlDoc = new XDocument(
            new XElement("library",
                new XElement("books",
                    new XElement("book",
                        new XAttribute("id", 1),
                        new XElement("title", "Book One"),
                        new XElement("author", "Author One"),
                        new XElement("price", 10),
                        new XElement("year", 2021)
                    ),
                    new XElement("book",
                        new XAttribute("id", 2),
                        new XElement("title", "Book Two"),
                        new XElement("author", "Author Two"),
                        new XElement("price", 20),
                        new XElement("year", 2022)
                    ),
                    new XElement("book",
                        new XAttribute("id", 3),
                        new XElement("title", "Book Three"),
                        new XElement("author", "Author Three"),
                        new XElement("price", 15),
                        new XElement("year", 2020)
                    ),
                    new XElement("book",
                        new XAttribute("id", 4),
                        new XElement("title", "Book Four"),
                        new XElement("author", "Author Four"),
                        new XElement("price", 25),
                        new XElement("year", 2019)
                    ),
                    new XElement("book",
                        new XAttribute("id", 5),
                        new XElement("title", "Book Five"),
                        new XElement("author", "Author Five"),
                        new XElement("price", 30),
                        new XElement("year", 2018)
                    )
                )
            )
        );


            // Save the XML document to a file
            libraryXmlDoc.Save("library.xml");

            Console.WriteLine("XML document created successfully.");

            // Load XML file
            try
            {
                xmlDoc = XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading XML file: " + ex.Message);
                return;
            }
        }
        static void DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Search for elements");
                Console.WriteLine("2. Add element");
                Console.WriteLine("3. Delete element");
                Console.WriteLine("4. Update element");
                Console.WriteLine("5. Save changes");
                Console.WriteLine("6. Display XML");
                Console.WriteLine("7. Serialize to XML");
                Console.WriteLine("8. Deserialize from XML");
                Console.WriteLine("9. Filter from XML");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchElements();
                        break;
                    case "2":
                        AddElement();
                        break;
                    case "3":
                        DeleteElement();
                        break;
                    case "4":
                        UpdateElement();
                        break;
                    case "5":
                        SaveChanges();
                        break;
                    case "6":
                        DisplayXml(xmlDoc.Root);
                        break;
                    case "7":
                        SerializeToXml();
                        break;
                    case "8":
                        DeserializeFromXml(@"C:\Users\Z004WV3M\Documents\serialized_books.xml");
                        break;
                    case "9":
                        FilterBooks();
                        break;
                    case "10":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        #endregion

        // Display XML contents
        #region Displaying and Searching XML Elements
        static void DisplayXml(XElement element, int indent = 0)
        {
            string indentString = new string(' ', indent);
            if (element.HasAttributes)
            {
                var attributes = element.Attributes().Where(attr => attr.Name == "id");
                string attributesString = string.Join(" ", attributes.Select(attr => $"{attr.Name}=\"{attr.Value}\""));
                Console.WriteLine($"{indentString}<{element.Name.LocalName} {attributesString}>");
            }
            else
            {
                Console.WriteLine($"{indentString}<{element.Name.LocalName}>");
            }

            if (element.HasElements)
            {
                foreach (var child in element.Elements())
                {
                    DisplayXml(child, indent + 2);
                }

                Console.WriteLine($"{indentString}</{element.Name.LocalName}>"); // Close the element after all its children
            }
            else
            {
                string text = element.Value.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    Console.WriteLine($"{indentString}{new string(' ', 2)}{text}");
                }

                Console.WriteLine($"{indentString}</{element.Name.LocalName}>"); // Close the element if it's a leaf node
            }
        }
        static void SearchElements()
        {
            Console.WriteLine("\nSearch Options:");
            Console.WriteLine("1. Search by tag name");
            Console.WriteLine("2. search by attribute value");
            Console.WriteLine("3. Sort by attribute value");
            Console.WriteLine("4. Return to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the tag name to search for: ");
                    string tagName = Console.ReadLine();
                    var elementsByTagName = xmlDoc.Descendants(tagName);
                    PrintResults(elementsByTagName);
                    break;
                case "2":
                    Console.Write("Enter the attribute name to filter by: ");
                    string attributeName = Console.ReadLine();
                    Console.Write("Enter the attribute value to filter by: ");
                    string attributeValue = Console.ReadLine();
                    var elementsByAttribute = xmlDoc.Descendants()
                        .Where(e => e.Attribute(attributeName)?.Value == attributeValue);
                    PrintResults(elementsByAttribute);
                    break;
                case "3":
                    Console.Write("Enter the attribute name to sort by: ");
                    string sortAttributeName = Console.ReadLine();
                    var elementsToSort = xmlDoc.Descendants()
                        .OrderBy(e => e.Attribute(sortAttributeName)?.Value);
                    PrintResults(elementsToSort);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void PrintResults(IEnumerable<XElement> elements)
        {
            if (!elements.Any())
            {
                Console.WriteLine("No elements found.");
            }
            else
            {
                Console.WriteLine($"Found {elements.Count()} elements:");
                foreach (var element in elements)
                {
                    Console.WriteLine(element);
                }
            }
        }

        #endregion

        #region Modifying XML Elements
        static void AddElement()
        {
            Console.WriteLine("\nAdd Element Menu:");
            Console.WriteLine("1. Add element to parent by ID");
            Console.WriteLine("2. Add parent element with child elements and values");
            Console.WriteLine("3. Add same child element to different parent elements with different values");
            Console.WriteLine("4. Return to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddElementToParentById();
                    break;
                case "2":
                    AddParentWithChildElements();
                    break;
                case "3":
                    AddSameChildElementToDifferentParents();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void AddElementToParentById()
        {
            Console.Write("\nEnter the ID of the parent element: ");
            string parentId = Console.ReadLine();

            Console.Write("Enter the new element tag name: ");
            string newElementTagName = Console.ReadLine();

            Console.Write("Enter the text value for the new element: ");
            string newTextValue = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                return;
            }

            var newElement = new XElement(newElementTagName, newTextValue);
            parentElement.Add(newElement);

            // Check if the new element was successfully added to the parent
            if (parentElement.Elements(newElementTagName).Any())
            {
                Console.WriteLine("Element added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add element. Please try again.");
            }
        }


        static void AddParentWithChildElements()
        {
            Console.Write("\nEnter the new parent element tag name: ");
            string newParentTagName = Console.ReadLine();

            Console.Write("Enter the ID for the new parent element: ");
            string parentId = Console.ReadLine();

            Console.Write("Enter the number of child elements to add: ");
            int numChildElements;
            while (!int.TryParse(Console.ReadLine(), out numChildElements) || numChildElements < 1)
            {
                Console.WriteLine("Please enter a valid number greater than 0.");
                Console.Write("Enter the number of child elements to add: ");
            }

            XElement newParentElement = new XElement(newParentTagName, new XAttribute("id", parentId));

            for (int i = 0; i < numChildElements; i++)
            {
                Console.Write($"Enter the tag name for child element {i + 1}: ");
                string childTagName = Console.ReadLine();

                Console.Write($"Enter the value for child element {i + 1}: ");
                string childValue = Console.ReadLine();

                newParentElement.Add(new XElement(childTagName, childValue));
            }

            xmlDoc.Root.Add(newParentElement);
            Console.WriteLine("Parent element with child elements added successfully.");
        }


        static void AddSameChildElementToDifferentParents()
        {
            Console.Write("\nEnter the tag name for the child element: ");
            string childTagName = Console.ReadLine();

            Console.Write("Enter the number of parents to add the child element to: ");
            int numParents;
            while (!int.TryParse(Console.ReadLine(), out numParents) || numParents < 1)
            {
                Console.WriteLine("Please enter a valid number greater than 0.");
                Console.Write("Enter the number of parents to add the child element to: ");
            }

            for (int i = 0; i < numParents; i++)
            {
                Console.Write($"Enter the ID for parent {i + 1}: ");
                string parentId = Console.ReadLine();

                var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
                if (parentElement == null)
                {
                    Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                    continue;
                }

                Console.Write($"Enter the value for child element {i + 1}: ");
                string childValue = Console.ReadLine();

                var newElement = new XElement(childTagName, childValue);
                parentElement.Add(newElement);
                Console.WriteLine($"Child element added to parent '{parentId}' successfully.");
            }
        }


        static void DeleteElement()
        {
            Console.WriteLine("\nDelete Element Menu:");
            Console.WriteLine("1. Delete parent element by ID");
            Console.WriteLine("2. Delete particular child element from parent by ID");
            Console.WriteLine("3. Delete particular child element from all parents");
            Console.WriteLine("4. Return to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DeleteParentElementById();
                    break;
                case "2":
                    DeleteChildElementFromParentById();
                    break;
                case "3":
                    DeleteChildElementFromAllParents();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void DeleteParentElementById()
        {
            Console.Write("\nEnter the ID of the parent element to delete: ");
            string parentId = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                return;
            }

            parentElement.Remove();

            // Check if the parent element was successfully deleted
            if (!xmlDoc.Descendants("book").Any(e => e.Attribute("id")?.Value == parentId))
            {
                Console.WriteLine("Parent element and its children deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete parent element. Please try again.");
            }
        }

        static void DeleteChildElementFromParentById()
        {
            Console.Write("\nEnter the ID of the parent element: ");
            string parentId = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                return;
            }

            Console.Write("Enter the tag name of the child element to delete: ");
            string childTagName = Console.ReadLine();

            var childElement = parentElement.Element(childTagName);
            if (childElement == null)
            {
                Console.WriteLine($"Child element '{childTagName}' not found in parent with ID '{parentId}'.");
                return;
            }

            childElement.Remove();

            // Check if the child element was successfully deleted
            if (parentElement.Element(childTagName) == null)
            {
                Console.WriteLine($"Child element '{childTagName}' deleted successfully from parent with ID '{parentId}'.");
            }
            else
            {
                Console.WriteLine($"Failed to delete child element '{childTagName}' from parent with ID '{parentId}'. Please try again.");
            }
        }

        static void DeleteChildElementFromAllParents()
        {
            Console.Write("Enter the tag name of the child element to delete from all parents: ");
            string childTagName = Console.ReadLine();

            foreach (var parentElement in xmlDoc.Descendants("book"))
            {
                var childElement = parentElement.Element(childTagName);
                if (childElement != null)
                {
                    childElement.Remove();
                    Console.WriteLine($"Child element '{childTagName}' deleted successfully from parent with ID '{parentElement.Attribute("id").Value}'.");
                }
            }
        }
        static void UpdateElement()
        {
            Console.WriteLine("\nUpdate Element Menu:");
            Console.WriteLine("1. Update element values by tag name for particular parent ID");
            Console.WriteLine("2. Update element tag names by tag name for particular parent ID");
            Console.WriteLine("3. Update parent element ID");
            Console.WriteLine("4. Change similar tag names for every parent with similar child elements");
            Console.WriteLine("5. Return to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UpdateElementValuesByTagNameForParentId();
                    break;
                case "2":
                    UpdateElementTagNamesByTagNameForParentId();
                    break;
                case "3":
                    UpdateParentElementId();
                    break;
                case "4":
                    ChangeSimilarTagNamesForEveryParent();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void UpdateElementValuesByTagNameForParentId()
        {
            Console.Write("\nEnter the ID of the parent element: ");
            string parentId = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                return;
            }

            Console.Write("Enter the tag name of the element to update: ");
            string tagName = Console.ReadLine();

            var element = parentElement.Element(tagName);
            if (element == null)
            {
                Console.WriteLine($"Element with tag name '{tagName}' not found in parent with ID '{parentId}'.");
                return;
            }

            Console.Write("Enter the new value for the element: ");
            string newValue = Console.ReadLine();

            element.Value = newValue;
            Console.WriteLine($"Element with tag name '{tagName}' updated successfully in parent with ID '{parentId}'.");
        }


        static void UpdateParentElementId()
        {
            Console.Write("\nEnter the current ID of the parent element: ");
            string currentId = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == currentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{currentId}' not found.");
                return;
            }

            Console.Write("Enter the new ID for the parent element: ");
            string newId = Console.ReadLine();

            parentElement.Attribute("id").Value = newId;
            Console.WriteLine($"Parent element ID updated from '{currentId}' to '{newId}'.");
        }
        static void UpdateElementTagNamesByTagNameForParentId()
        {
            Console.Write("\nEnter the ID of the parent element: ");
            string parentId = Console.ReadLine();

            var parentElement = xmlDoc.Descendants("book").FirstOrDefault(e => e.Attribute("id")?.Value == parentId);
            if (parentElement == null)
            {
                Console.WriteLine($"Parent element with ID '{parentId}' not found.");
                return;
            }

            Console.Write("Enter the current tag name of the element to update: ");
            string currentTagName = Console.ReadLine();

            var element = parentElement.Element(currentTagName);
            if (element == null)
            {
                Console.WriteLine($"Element with tag name '{currentTagName}' not found in parent with ID '{parentId}'.");
                return;
            }

            Console.Write("Enter the new tag name for the element: ");
            string newTagName = Console.ReadLine();

            element.Name = newTagName;
            Console.WriteLine($"Element tag name updated from '{currentTagName}' to '{newTagName}' in parent with ID '{parentId}'.");
        }

        static void ChangeSimilarTagNamesForEveryParent()
        {
            Console.Write("Enter the current tag name of the child elements to update: ");
            string currentTagName = Console.ReadLine();

            Console.Write("Enter the new tag name for the child elements: ");
            string newTagName = Console.ReadLine();

            foreach (var parentElement in xmlDoc.Descendants("book"))
            {
                foreach (var element in parentElement.Elements(currentTagName))
                {
                    element.Name = newTagName;
                }
            }
            Console.WriteLine($"All child elements with tag name '{currentTagName}' updated to '{newTagName}' in every parent.");
        }

        #endregion

        #region Filtering XML Elements
        static void FilterBooks()
        {
            Console.WriteLine("\nFilter Options:");
            Console.WriteLine("1. Filter by author");
            Console.WriteLine("2. Filter by year");
            Console.WriteLine("3. Filter by title");
            Console.WriteLine("4. Return to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FilterByAuthor();
                    break;
                case "2":
                    FilterByYear();
                    break;
                case "3":
                    FilterByTitle();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void FilterByAuthor()
        {
            Console.Write("Enter the author's name to filter books: ");
            string authorName = Console.ReadLine();

            // Use XName.Get to create XName with proper casing
            var filteredBooks = xmlDoc.Descendants(XName.Get("book")).Where(book => book.Element(XName.Get("author"))?.Value == authorName);
            PrintResults(filteredBooks);
        }

        static void FilterByYear()
        {
            Console.Write("Enter the year to filter books: ");
            string year = Console.ReadLine();

            // Use XName.Get to create XName with proper casing
            var filteredBooks = xmlDoc.Descendants(XName.Get("book")).Where(book => book.Element(XName.Get("year"))?.Value == year);
            PrintResults(filteredBooks);
        }

        static void FilterByTitle()
        {
            Console.Write("Enter the title to filter books: ");
            string title = Console.ReadLine();

            // Use XName.Get to create XName with proper casing
            var filteredBooks = xmlDoc.Descendants(XName.Get("book")).Where(book => book.Element(XName.Get("title"))?.Value == title);
            PrintResults(filteredBooks);
        }

        #endregion

        #region Saving and Serializing Changes
        static void SaveChanges()
        {
            try
            {
                xmlDoc.Save(xmlFilePath);
                Console.WriteLine("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving changes: " + ex.Message);
            }
        }

        static void SerializeToXml()
        {
            Console.Write("Enter the path to save the serialized XML file: ");
            string path = Console.ReadLine();

            // Validate the file path
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            try
            {
                var books = new List<Book>
        {
            new Book { Title = "Book1", Author = "Author1", Year = "2021" },
            new Book { Title = "Book2", Author = "Author2", Year = "2022" }
        };

                var serializer = new XmlSerializer(typeof(List<Book>));
                using (var writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, books);
                }
                Console.WriteLine("Books serialized to XML successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing books to XML: " + ex.Message);
            }
        }

        static void DeserializeFromXml(string path)
        {
            try
            {
                // Validate if the file exists
                if (!File.Exists(path))
                {
                    Console.WriteLine("Error: The specified XML file does not exist.");
                    return;
                }

                var serializer = new XmlSerializer(typeof(Library));
                using (var reader = new StreamReader(path))
                {
                    var library = (Library)serializer.Deserialize(reader);
                    Console.WriteLine("Books deserialized from XML:");
                    foreach (var book in library.Books)
                    {
                        Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializing books from XML: " + ex.Message);
            }
        }

        #endregion
    }

    [XmlRoot("library")]
    public class Library
    {
        [XmlElement("book")]
        public List<Book> Books { get; set; }
    }

    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
    }

}

