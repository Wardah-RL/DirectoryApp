using DirectoryApp.Service;

public class Program
{
  static void Main()
  {
    MenuDirectory();
  }

  public static void MenuDirectory()
  {
    Console.Write("cd ");
    var directory = Console.ReadLine();
    Menu(directory);
  }
  public static void Menu(string directory)
  {
    Console.Write("[0] Input Directory");
    Console.Write("[1] Scan");
    Console.Write("[2] Suggest");
    Console.Write("[3] Is");
    Console.WriteLine("[4] Exit");
    var index = Console.ReadLine();
    DirectoryService Directory = new DirectoryService();
    Directory.GetDirectory(directory, index);
  }

}