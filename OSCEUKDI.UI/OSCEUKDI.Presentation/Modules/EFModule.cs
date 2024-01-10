using System;
using System.Collections.Generic;
using System.Data.Entity;
using Autofac;
using OSCEUKDI.Common.Interfaces;
using OSCEUKDI.Repository.BaseRepository;

namespace OSCEUKDI.Presentation.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(OSCEUKDIContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }
    }
}