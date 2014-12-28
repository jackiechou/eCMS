using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.Common
{
  
    public class TreeEntity
    {
        public int key { get; set; }
        public int? parentid { get; set; }
        public int? depth { get; set; }       
        public string title { get; set; }
        public string tooltip { get; set; }
        public bool folder { get; set; }
        public bool lazy { get; set; }
        public bool expanded { get; set; }
        public List<TreeEntity> children { get; set; }
    }

    public class TreeNode
    {
        public int id { get; set; }

        public int? parentid { get; set; }

        public string text { get; set; }
        public int depth { get; set; }
        public string state { get; set; }
        public bool? ischecked { get; set; }
        public string icon { get; set; }
        public object attributes { get; set; }

        public List<TreeNode> children { get; set; }
        public string TreeNodeJson()
        {
            return ConvertTreeNodeToJson(this);
        }
        public TreeNode()
        {
            children = new List<TreeNode>();
        }
        private string ConvertTreeNodeToJson(TreeNode node)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            sb.AppendFormat("\"id\":{0},", node.id);

            if (!string.IsNullOrWhiteSpace(node.state))
            {
                sb.AppendFormat("\"state\":\"{0}\",", node.state);
            }
            if (!string.IsNullOrWhiteSpace(node.icon))
            {
                sb.AppendFormat("\"icon\":\"{0}\",", node.icon);
            }
            if (node.ischecked != null)
            {
                sb.AppendFormat("\"checked\":\"{0},\"", node.ischecked);
            }

            // to append attributes...
            if (node.attributes != null)
            {
                var attributesType = node.attributes.GetType();
                foreach (var item in attributesType.GetProperties())
                {
                    var value = item.GetValue(node.attributes, null);
                    if (value != null)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", item.Name, value);
                    }
                }
            }

            //recursive append children
            if (node.children != null && node.children.ToArray().Length > 0)
            {
                StringBuilder sbChildren = new StringBuilder();
                foreach (var item in node.children)
                {
                    sbChildren.AppendFormat("{0},", ConvertTreeNodeToJson(item));
                }

                sb.AppendFormat("\"children\":[{0}],", sbChildren.ToString().TrimEnd(','));
            }


            sb.AppendFormat("\"text\":\"{0}\"", node.text);

            sb.Append("}");

            return sb.ToString();
        }
    }

    public class SelectItem
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
