using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace JobsWatcher.Infrastructure.UnitTests.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> input)
        {
            return new NotInDbSet<T>(input);
        }
    }

    public class NotInDbSet<T> : IQueryable<T>, IAsyncEnumerable<T>, IEnumerable<T>, IEnumerable
    {
        private readonly List<T> _innerCollection;

        public NotInDbSet(IEnumerable<T> innerCollection)
        {
            _innerCollection = innerCollection.ToList();
        }


        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new())
        {
            return new AsyncEnumerator(GetEnumerator());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => typeof(T);
        public Expression Expression => Expression.Empty();
        public IQueryProvider Provider => new EnumerableQuery<T>(Expression);

        public class AsyncEnumerator : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public AsyncEnumerator(IEnumerator<T> enumerator)
            {
                _enumerator = enumerator;
            }

            public ValueTask DisposeAsync()
            {
                return new();
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new(_enumerator.MoveNext());
            }

            public T Current => _enumerator.Current;
        }
    }
}