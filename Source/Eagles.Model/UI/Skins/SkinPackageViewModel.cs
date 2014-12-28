using Eagle.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Eagle.Model.UI.Skins
{
    [Serializable()]
    public class SkinPackageViewModel : BaseModel
    {
        private int _ApplicationId = Null.NullInteger;
        private int _SkinPackageId = Null.NullInteger;
        private int _SkinPackageTypeId = Null.NullInteger;        
        private string _SkinPackageName;
        private string _SkinPackageAlias;
        private string _SkinPackageSrc;
        private Dictionary<int, string> _Skins = new Dictionary<int, string>();
       

        public int ApplicationId
        {
            get { return _ApplicationId; }
            set { _ApplicationId = value; }
        }
        public int SkinPackageId
        {
            get { return _SkinPackageId; }
            set { _SkinPackageId = value; }
        }
        public int SkinPackageTypeId
        {
            get { return _SkinPackageTypeId; }
            set { _SkinPackageTypeId = value; }
        }

        public string SkinPackageName
        {
            get { return _SkinPackageName; }
            set { _SkinPackageName = value; }
        }
        public string SkinPackageAlias
        {
            get { return _SkinPackageAlias; }
            set { _SkinPackageAlias = value; }
        }
        public string SkinPackageSrc
        {
            get { return _SkinPackageSrc; }
            set { _SkinPackageSrc = value; }
        }

        [XmlIgnore()]
        public Dictionary<int, string> Skins
        {
            get { return _Skins; }
            set { _Skins = value; }
        }        
    }
}
