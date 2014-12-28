using Eagle.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eagle.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsNew()
        {
            return Id == 0;
        }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        private DateTime? _createdDate = DateTime.MinValue;

         [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get { return _createdDate ?? DateTime.Now; }
            set { _createdDate = value; }
        }
        public DateTime ModifiedDate { get; set; }
        public int SortOrder { get; set; }
        private int _priority = 1;
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        private bool _showTitle = true;
        public bool ShowTitle
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }

        private int _CreatedByUserId = Null.NullInteger;
        private DateTime _CreatedOnDate = DateTime.Now;
        private int _LastModifiedByUserId = Null.NullInteger;
        private DateTime _LastModifiedOnDate = DateTime.Now;
        [Browsable(false), XmlIgnore()]
        public int CreatedByUserId
        {
            get { return _CreatedByUserId; }
        }
        [Browsable(false), XmlIgnore()]
        public DateTime CreatedOnDate
        {
            get { return _CreatedOnDate; }
        }
        [Browsable(false), XmlIgnore()]
        public int LastModifiedByUserId
        {
            get { return _LastModifiedByUserId; }
        }
        [Browsable(false), XmlIgnore()]
        public DateTime LastModifiedOnDate
        {
            get { return _LastModifiedOnDate; }
        }
    }
}
