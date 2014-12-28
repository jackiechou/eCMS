using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SendMailCalendar
{

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            string EventServices = "HRM_UNIT_Send Mail Calendar";
            string EventServicesDescription = "Send Mail Calendar Tự động gửi email thông báo.";

            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            ServiceInstaller Services = new ServiceInstaller();
            Services.StartType = ServiceStartMode.Automatic;
            Services.ServiceName = EventServices;
            Services.DisplayName = EventServices;
            Services.Description = EventServicesDescription;
            this.Installers.AddRange(new Installer[] { serviceProcessInstaller, Services });
        }

        protected override void OnCommitted(System.Collections.IDictionary savedState)
        {
            ServiceController sc = new ServiceController("HRM_UNIT_Send Mail Calendar");
            sc.Start();
        }
    }
}
