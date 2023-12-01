using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureManager
{
    public class ProcedureList
    {
        private Dictionary<string, List<string?>> _dictionary = new Dictionary<string, List<string?>>();
        private const string NAME = "Procedure Name";
        private const string QUERY = "Query";

        public int Count { get { return _dictionary[NAME].Count; } }

        public ProcedureList()
        {
            _dictionary.Add(NAME, new List<string?>());
            _dictionary.Add(QUERY, new List<string?>());
        }

        public ProcedureList AddName(string key)
        {
            _dictionary[NAME].Add(key);
            _dictionary[QUERY].Add(null);
            return this;
        }

        public ProcedureList Delete(int index = -1)
        {
            int _index = GetIndex(index);
            _dictionary[NAME].RemoveAt(_index);
            _dictionary[QUERY].RemoveAt(_index);
            return this;
        }

        public Tuple<string?, string?> GetPair(int index = -1)
        {
            int _index = GetIndex(index);
            string? key = _dictionary[NAME][_index];
            string? value = _dictionary[QUERY][_index];
            return new Tuple<string?, string?>(key, value);
        }

        public string? GetQuery(int index = -1)
        {
            return _dictionary[QUERY][GetIndex(index)];
        }

        public ProcedureList SetQuery(string query, int index = -1)
        {
            _dictionary[QUERY][GetIndex(index)] = query;
            return this;
        }

        public ProcedureList AppendQuery(string query, int index = -1)
        {
            _dictionary[QUERY][GetIndex(index)] += query;
            return this;
        }

        private int GetIndex(int index)
        {
            return index < 0 ? _dictionary[NAME].Count - 1 : index;
        }
    }
}
