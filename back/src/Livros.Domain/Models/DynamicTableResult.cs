using System.Collections.Generic;
using System.Linq;

namespace TudoFarmaRep.Model.Models
{
   
    public class FiltersDynamicTable
    {
        public string? Titulo { get; set; }
        public int Coluna { get; set; }
        public Campos[]? Campos { get; set; }
    }

    public class Campos
    {
        public string? Nome { get; set; }
        public int Value { get; set; }
    }

    public class DynamicTableResult
    {
        public IList<Dictionary<string, string>> Data { get; set; }
        public bool HasData { get => Data?.Any() ?? false; }
        public int RowCount { get; set; }
        public DynamicTableResult()
        {
            Data = new List<Dictionary<string, string>>();
        }
    }
}
