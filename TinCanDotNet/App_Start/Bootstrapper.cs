using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Unity.WebApi;
using System.Web.Http;
using TinCanDotNet.Models.Repositories;
using TinCanDotNet.Models;

namespace TinCanDotNet
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new Unity.Mvc4.UnityDependencyResolver(container));
      GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
     


      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<IDataRepository<Statement>, MemoryRepository<Statement>>(new ContainerControlledLifetimeManager());
    
    }
  }
}