using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow_School
{
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

    public class StudentFileHandler
    {
        private const string FilePath = "F:\\Practice Project 3\\Rainbow School\\Rainbow School\\students.txt";

        public static List<Student> ReadStudentsFromFile()
        {
            List<Student> students = new List<Student>();

            try
            {
                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        students.Add(new Student { Name = parts[0].Trim(), Class = parts[1].Trim() });
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Create the file and add student data.");
            }

            return students;
        }

        public static void WriteStudentsToFile(List<Student> students)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (Student student in students)
                    {
                        writer.WriteLine($"{student.Name}, {student.Class}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            List<Student> students = StudentFileHandler.ReadStudentsFromFile();

            if (students.Count > 0)
            {
                DisplayMenu();

                while (true)
                {
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice.ToLower())
                    {
                        case "1":
                            DisplayStudents(students);
                            break;
                        case "2":
                            students = students.OrderBy(s => s.Name).ToList();
                            DisplayStudents(students);
                            break;
                        case "3":
                            Console.Write("Enter student name to search: ");
                            string searchName = Console.ReadLine();
                            SearchStudent(students, searchName);
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No student data found.");
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Display Students");
            Console.WriteLine("2. Sort Students by Name");
            Console.WriteLine("3. Search Student by Name");
            Console.WriteLine("4. Exit");
        }

        static void DisplayStudents(List<Student> students)
        {
            const int columnWidth = 25;
            Console.WriteLine("\nStudent List:");
            Console.WriteLine($"{nameof(Student.Name).PadRight(columnWidth)} {nameof(Student.Class)}");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name.PadRight(columnWidth)} {student.Class}");
            }
            Console.WriteLine();
        }

        static void SearchStudent(List<Student> students, string searchName)
        {
            var searchResults = students
                .Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                .ToList();

            if (searchResults.Count > 0)
            {
                Console.WriteLine("\nSearch Results:");
                DisplayStudents(searchResults);
            }
            else
            {
                Console.WriteLine("No matching student found.");
            }
        }


    }

}

