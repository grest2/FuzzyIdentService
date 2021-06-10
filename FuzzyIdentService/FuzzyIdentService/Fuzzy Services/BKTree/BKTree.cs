using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Fuzzy_Services.BKTree
{
    public class BKTree<T> where T : BKTreeNode
    {
        private T _root;

        private readonly Dictionary<T, int> _matches;

        public BKTree()
        {
            _root = null;
            _matches = new Dictionary<T, int>();
        }

        public void add(T node)
        {
            if (_root != null)
            {
                _root.add(node);
            }
            else
            {
                _root = node;
            }
        }

        public Dictionary<T, int> query(BKTreeNode node)
        {
            BKTreeNode bestNode;
            int disctance = _root.findBestMatch(node, int.MaxValue, out bestNode);
            _matches.Clear();
            _matches.Add((T)bestNode, disctance);
            return _matches;
        }
        private Dictionary<T, int> copyMatches(Dictionary<BKTreeNode, int> source)
        {
            _matches.Clear();
            foreach (KeyValuePair<BKTreeNode, int> pair in source)
            {
                _matches.Add((T)pair.Key, pair.Value);
            }
            return _matches;
        }
    }
    
}
