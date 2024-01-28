using DirectoryApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Model
{
  public class ListFilesReturn
  {
    public string file { get; set; }
    public long size { get; set; }
    public TypeFile type { get; set; }
  }
}
