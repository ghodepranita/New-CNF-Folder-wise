using CNF.Business.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
    public interface IConfigurationRepository
    {
        AppConfigurationList GetAppConfiguration();

        string AddEditAppConfiguration(AppConfigurationLst model);

    }
}
