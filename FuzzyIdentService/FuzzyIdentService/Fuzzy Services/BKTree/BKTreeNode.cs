using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Fuzzy_Services.BKTree
{
    public abstract class BKTreeNode
    {
        private readonly Dictionary<int, BKTreeNode> _children;

        public BKTreeNode()
        {
            _children = new Dictionary<int, BKTreeNode>();
        }

        public virtual void add(BKTreeNode node)
        {

            int distance = calculateDisctance(node);
            if (_children.ContainsKey(distance))
            {
                _children[distance].add(node);
            }
            else
            {
                _children.Add(distance, node);

            }
        }
        public virtual int findBestMatch(BKTreeNode node, int bestDisctance, out BKTreeNode bestNode)
        {
            int disctanceAtNode = calculateDisctance(node);
            bestNode = node;
            if (disctanceAtNode < bestDisctance)
            {
                bestDisctance = disctanceAtNode;
                bestNode = this;
            }
            int possibleBest = bestDisctance;
            foreach (int disctance in _children.Keys)
            {
                if (disctance < disctanceAtNode + bestDisctance)
                {
                    possibleBest = _children[disctance].findBestMatch(node, bestDisctance, out bestNode);
                    if (possibleBest < bestDisctance)
                    {
                        bestDisctance = possibleBest;
                    }
                }
            }
            return bestDisctance;
        }
        public virtual void query(BKTreeNode node, int threshold, Dictionary<BKTreeNode, int> collected)
        {
            int disctanceAtNode = calculateDisctance(node);
            if (disctanceAtNode == threshold)
            {
                collected.Add(this, disctanceAtNode);
                return;
            }
            if (disctanceAtNode < threshold)
            {
                collected.Add(this, disctanceAtNode);
            }
            for (int distance = (disctanceAtNode - threshold); distance <= (threshold + disctanceAtNode); distance++)
            {
                if (_children.ContainsKey(distance))
                {
                    _children[distance].query(node, threshold, collected);
                }
            }
        }
        protected abstract int calculateDisctance(BKTreeNode node);
    }
    
}
