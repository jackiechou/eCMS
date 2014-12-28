using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Menu
{
    //FlatData[] elements = new FlatData[]
    //{
    //    new FlatData(1,0,"Category"),   
    //    new FlatData(2,1,"Sub Category 1"), 
    //    new FlatData(3,1,"Sub Category 2"), 
    //    new FlatData(4,2,"Item 1"), 
    //    new FlatData(5,2,"Item 2"),             
    //    new FlatData(6,2,"Item 3"),
    //    new FlatData(7,3,"Item 4"),
    //    new FlatData(8,3,"Item 5"),
    //    new FlatData(9,3,"Item 6")
    //};

    public class FlatData
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public int Depth { get; set; }
        public int ListOrder { get; set; }
        public string MenuName { get; set; }
        public string Alias { get; set; }
        public int PageId { get; set; }
        public string Target { get; set; }
        public int IconFile { get; set; }
        public string IconClass { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public FlatData(int _MenuId, int _ParentId, int _TypeId, int _Depth,
            int _ListOrder, string _Name, string _Alias, int _PageId, string _Target, int _IconFile, string _IconClass, string _CssClass, string _Description, int _Status)
        {
            MenuId = _MenuId;
            ParentId = _ParentId;
            TypeId = _TypeId;
            Depth = _Depth;
            ListOrder = _ListOrder;
            MenuName = _Name;
            Alias = _Alias;
            PageId = _PageId;
            Target = _Target;
            IconFile = _IconFile;
            IconClass = _IconClass;
            CssClass = _CssClass;
            Description = _Description;
            Status = _Status;
        }
    }
    public class NodeData
    {
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
        public int TypeId { get; set; }
        public int Depth { get; set; }
        public int ListOrder { get; set; }
        public string MenuName { get; set; }
        public string Alias { get; set; }
        public int PageId { get; set; }
        public string Target { get; set; }
        public int IconFile { get; set; }
        public string IconClass { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public List<NodeData> Children { get; set; }
    }

    public class NodeDataIEnumerable
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public int Depth { get; set; }
        public int ListOrder { get; set; }
        public string MenuName { get; set; }
        public string Alias { get; set; }
        public int PageId { get; set; }
        public string Target { get; set; }
        public int IconFile { get; set; }
        public string IconClass { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public IEnumerable<NodeDataIEnumerable> Children { get; set; }
    }

    public class DeepNodeDataIEnumerable
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public int Depth { get; set; }
        public int ListOrder { get; set; }
        public string MenuName { get; set; }
        public string Alias { get; set; }
        public int PageId { get; set; }
        public string Target { get; set; }
        public int IconFile { get; set; }
        public string IconClass { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public IEnumerable<DeepNodeDataIEnumerable> Children { get; set; }
    } 
}
