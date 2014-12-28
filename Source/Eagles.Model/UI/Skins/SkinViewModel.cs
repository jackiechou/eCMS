using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.UI.Skins
{
    public enum SkinType
    {
        Desktop = 0,
        Admin = 1,
        Both = 3
    }

    [Serializable()]
    public class SkinViewModel
    {
        private int _SkinId;   
        private SkinType _SkinTypeId;
        private int _SkinPackageId;
        private bool _IsSkinSelected;
        private bool _SkinStatus;

        private string _SkinPackageName;
        private string _SkinPackageSrc;

        public int SkinId
        {
            get { return _SkinId; }
            set { _SkinId = value; }
        }
        public SkinType SkinTypeId
        {
            get { return _SkinTypeId; }
            set { _SkinTypeId = value; }
        }
        public int SkinPackageId
        {
            get { return _SkinPackageId; }
            set { _SkinPackageId = value; }
        }       
        public bool IsSkinSelected
        {
            get { return _IsSkinSelected; }
            set { _IsSkinSelected = value; }
        }

        public bool SkinStatus
        {
            get { return _SkinStatus; }
            set { _SkinStatus = value; }
        }


        public string SkinPackageName
        {
            get { return _SkinPackageName; }
            set { _SkinPackageName = value; }
        }
        public string SkinPackageSrc
        {
            get { return _SkinPackageSrc; }
            set { _SkinPackageSrc = value; }
        }

    }
}
