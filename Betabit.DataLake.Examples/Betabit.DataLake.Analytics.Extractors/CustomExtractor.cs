using System;
using System.Collections.Generic;

using Microsoft.Analytics.Interfaces;

namespace Betabit.DataLake.Analytics.Extractors
{
    [SqlUserDefinedExtractor]
    public class CustomExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            // Implement extracting data here
            throw new NotImplementedException();
        }
    }
}