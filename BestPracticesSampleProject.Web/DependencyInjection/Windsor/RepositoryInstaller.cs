﻿using BestPracticesSampleProject.Web.Models;
using BestPracticesSampleProject.Web.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestPracticesSampleProject.Web
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<BestPracticesSampleProjectDatabaseContext>()
                .ImplementedBy<BestPracticesSampleProjectDatabaseContext>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IProjectRepository>().WithServiceBase()
                                .LifestyleTransient());
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IDepartmentRepository>().WithServiceBase()
                                .LifestyleTransient());
        }
    }
}