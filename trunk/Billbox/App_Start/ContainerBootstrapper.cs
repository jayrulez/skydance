using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Billbox.Models.Interfaces;
using Billbox.Models.Respositories;
using Microsoft.Practices.Unity;

namespace Billbox
{
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
        }
    }
}