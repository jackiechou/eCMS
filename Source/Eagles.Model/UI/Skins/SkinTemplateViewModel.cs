using Eagle.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Eagle.Model.UI.Skins
{
    public class SkinTemplateViewModel
    {
        private int _SkinPackageId = Null.NullInteger;
        private int _TemplateId = Null.NullInteger;
        private string _TemplateName;
        private string _TemplateKey;
        private string _TemplateSrc;

        public int SkinPackageId
        {
            get { return _SkinPackageId; }
            set { _SkinPackageId = value; }
        }
        public int TemplateId
        {
            get { return _TemplateId; }
            set { _TemplateId = value; }
        }

        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        public string TemplateKey
        {
            get { return _TemplateKey; }
            set { _TemplateKey = value; }
        }
        public string TemplateSrc
        {
            get { return _TemplateSrc; }
            set { _TemplateSrc = value; }
        }
       
    }
}
