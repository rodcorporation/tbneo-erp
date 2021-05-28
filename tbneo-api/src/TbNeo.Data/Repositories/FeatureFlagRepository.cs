using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Data.Repositories
{
    public class FeatureFlagRepository : RepositoryBase<FeatureFlag>, IFeatureFlagRepository
    {
        public FeatureFlagRepository(TbNeoContext context) : base(context)
        {
        }
    }
}
