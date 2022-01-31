﻿using Autofac;
using AutoMapper;
using CMS.Application.AutoMapper;
using CMS.Application.Models.DTOs;
using CMS.Application.Services.Concrete;
using CMS.Application.Services.Interface;
using CMS.Application.Validation.FluentValidation;
using CMS.Domain.UnitOfWork.Interfaces;
using CMS.Infrastructure.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #region Services
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PageService>().As<IPageService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            #endregion

            #region Fluent Validation
            builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();
            #endregion

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                // Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                // This resolves a new context that can bu used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion
        }
    }
}
