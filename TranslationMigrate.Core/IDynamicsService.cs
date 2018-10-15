using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationMigrate.Core
{
    public interface IDynamicsService
    {
        EntityCollection Execute(QueryExpression query);
        void Update(Entity entity);
        void Create(Entity entity);
    }
}
