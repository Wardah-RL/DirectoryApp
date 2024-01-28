using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Interface
{
  public interface DirectoryInterface
  {
    void Scanning(string directory);
    void ListDirectory(string directory);
    void Suggest(string directory);
    //List<ListFilesReturn> ListDirectorys(string directory);
    //List<ListFilesReturn> ListFiles(string directory);
    //List<ListFilesReturn> GetAllDirectory(string directory);
    public void GetDirectory(string directory, string input);
  }
}
