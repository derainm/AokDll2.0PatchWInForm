
using System.Collections.Generic;

namespace DllPatchAok20
{
   public class DrsTable
  {
    public uint Type;
    public uint Start;
    public IEnumerable<DrsItem> Items;
  }
}
