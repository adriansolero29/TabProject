using BaseDataAccess.EventRepository.Interface;
using BaseDataAccess.EventRepository.Repository;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tabulation.UI.AdminManagement.Views;

namespace Tabulation.UI.AdminManagement
{
    public class AdminManagementBoostrapper : PrismBootstrapper
    {
        protected override Window? CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IContestRepository, ContestRepository>();
            containerRegistry.RegisterSingleton<IContestService, ContestService>();

            containerRegistry.RegisterSingleton<ICriteriaRepository, CriteriaRepository>();
            containerRegistry.RegisterSingleton<ICriteriaService, CriteriaService>();

            containerRegistry.RegisterSingleton<ICriterionRepository, CriterionRepository>();
            containerRegistry.RegisterSingleton<ICriterionService, CriterionService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule(typeof(AdminManagementModule));
        }
    }
}
