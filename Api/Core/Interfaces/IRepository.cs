using Microsoft.Azure.Cosmos;
using SD.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IRepository
    {
        Task<T> Get<T>(string id, string partitionKeyValue, CancellationToken cancellationToken) where T : CosmosBase;

        Task<T> Get<T>(QueryDefinition query, string partitionKeyValue, CancellationToken cancellationToken) where T : class;

        Task<List<T>> Query<T>(Expression<Func<T, bool>> predicate, string partitionKeyValue, CosmosType Type, CancellationToken cancellationToken) where T : CosmosBase;

        Task<List<T>> Query<T>(QueryDefinition query, CancellationToken cancellationToken) where T : CosmosBaseQuery;

        Task<T> Add<T>(T item, CancellationToken cancellationToken) where T : CosmosBase;

        Task<bool> Update<T>(T item, CancellationToken cancellationToken) where T : CosmosBase;
    }
}