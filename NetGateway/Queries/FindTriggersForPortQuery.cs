﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Queries
{
    public class FindTriggersForPortQuery : IQuery
    {
        private readonly IHelloHomeDbContext _ctx;

        public FindTriggersForPortQuery(IHelloHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<IList<Trigger>> ByRfAddressAndPortNumberAsync(byte rfNetwork, byte rfAddress, byte portNumber, CancellationToken cToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Trigger>> ByPortIdAsync(int portId, CancellationToken cToken)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Trigger> CreateQuery(Func<Trigger, bool> predicate)
        {
            return _ctx.Triggers
                .Where(x => predicate(x));
        }
    }
}