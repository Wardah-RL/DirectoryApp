using DirectoryApp.Enum;
using DirectoryApp.Interface;
using DirectoryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Service
{
  public class DirectoryService : DirectoryInterface
  {
    public void Scanning(string directory)
    {
      var listAllDirectory = GetAllDirectory(directory);
      var countFolder = listAllDirectory.Where(e => e.type == TypeFile.Folder).Count();
      var files = listAllDirectory.Where(e => e.type == TypeFile.file).ToList();
      var countFile = files == null ? 0 : files.Count;
      var countSize = files == null ? 0 : files.Sum(e => e.size);

      Console.WriteLine("Working directory : " + directory);
      Console.WriteLine($"Scanned successfully {countFolder} folders {countFile}, all files with total {countSize} bytes");
    }

    public void ListDirectory(string directory)
    {
      var listAllDirectory = GetAllDirectory(directory);

      Console.WriteLine($"Working directory : {directory}");
      foreach (var item in listAllDirectory)
      {
        var path = item.file.Replace(directory, "");
        if (item.type == TypeFile.Folder)
        {
          Console.WriteLine($@"Folder: : {path}\");
        }
        else
        {
          Console.WriteLine($"File:{path} | {item.size} bytes");
        }
      }
    }

    public void Suggest(string directory)
    {
      var listAllDirectory = GetAllDirectory(directory);
      var folders = listAllDirectory.Where(e => e.type == TypeFile.Folder).ToList();

      foreach (var item in folders)
      {
        var getFiles = ListFiles(item.file);
        var countFile = getFiles == null ? 0 : getFiles.Count;
        var countSize = getFiles == null ? 0 : getFiles.Sum(e => e.size);
        Console.WriteLine($"Folder : {item.file} it has {countFile} files with {countSize} bytes should be cleaned");
      }
    }

    public List<ListFilesReturn> ListDirectorys(string directory)
    {
      var listDirectory = Directory.GetDirectories(directory).ToList();

      List<ListFilesReturn> directorys = new List<ListFilesReturn>();
      foreach (string file in listDirectory)
      {
        directorys.Add(new ListFilesReturn
        {
          file = file,
          size = 0,
          type = TypeFile.Folder
        });
      }
      return directorys;
    }

    public List<ListFilesReturn> ListFiles(string directory)
    {
      try
      {
        var listFiles = Directory.GetFiles(directory).ToList();
        List<ListFilesReturn> files = new List<ListFilesReturn>();
        foreach (string file in listFiles)
        {
          files.Add(new ListFilesReturn
          {
            file = file,
            size = new System.IO.FileInfo(file).Length,
            type = TypeFile.file
          });
        }
        return files;
      }
      catch (UnauthorizedAccessException)
      {
        return default;
      }
    }

    public List<ListFilesReturn> GetAllDirectory(string directory)
    {
      var listDirectory = ListDirectorys(directory);
      var listFiles = ListFiles(directory);
      var countFile = listFiles == null ? 0 : listFiles.Count;
      var countSize = listFiles == null ? 0 : listFiles.Sum(e => e.size);

      List<ListFilesReturn> listAllDirectory = new List<ListFilesReturn>();
      listAllDirectory.AddRange(listDirectory);
      listAllDirectory.AddRange(listFiles);
      return listAllDirectory.OrderBy(e => e.type).ThenBy(e => e.file).ToList();
    }

    public void GetDirectory(string directory, string input)
    {
      DirectoryService scanningService = new DirectoryService();
      switch (input.ToLower())
      {
        case "0":
          Program.MenuDirectory();
          Console.WriteLine("======================================");
          break;
        case "1":
          scanningService.Scanning(directory);
          Console.WriteLine("======================================");
          Program.Menu(directory);
          break;
        case "2":
          scanningService.Suggest(directory);
          Console.WriteLine("======================================");
          Program.Menu(directory);
          break;
        case "3":
          scanningService.ListDirectory(directory);
          Console.WriteLine("======================================");
          Program.Menu(directory);
          break;
        case "4":
          Console.WriteLine("Exit");
          break;
        default:
          Console.WriteLine("your key is wrong");
          Console.WriteLine("======================================");
          Program.Menu(directory);
          break;
      }
    }
  }
}
