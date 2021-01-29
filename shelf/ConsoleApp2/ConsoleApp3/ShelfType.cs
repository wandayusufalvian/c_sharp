using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
  public class ShelfType
  {
    public class Enum
    {
      public const int Trough = 100;
      public const int Plate = 200;
    }

    public static readonly ShelfType Trough = new ShelfType() { Id = Enum.Trough, Name = "Trough" };
    public static readonly ShelfType Plate = new ShelfType() { Id = Enum.Plate, Name = "Plate" };

    private static readonly Dictionary<int, ShelfType> Dict = new[] { Trough, Plate }.ToDictionary(e => e.Id, e => e);


        private int _id;

    public int Id
    {
      get
      {
        if (Name == null && Dict.TryGetValue(_id, out ShelfType type))
        {
          Name = type.Name;
        }

        return _id;
      }
      set
      {
        _id = value;
      }
    }

    public string Name { get; set; }
        
   private ShelfType()
   {
     // required by EF
    }



    public static ShelfType FromId(int id)
    {
      return Dict[id];
    }

    public static ShelfType FromName(string name)
    {
      return Dict.Values.SingleOrDefault(e => e.Name.ToLowerInvariant() == name.ToLowerInvariant());
    }
  }
}
