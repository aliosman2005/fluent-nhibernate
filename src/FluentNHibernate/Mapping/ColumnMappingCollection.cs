using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class ColumnMappingCollection<TParent> : IEnumerable<ColumnMapping>
    {
        private readonly IList<ColumnMapping> columns = new List<ColumnMapping>();
        private readonly TParent parent;

        public ColumnMappingCollection(TParent parent)
        {
            this.parent = parent;
        }

        public TParent Add(string name)
        {
            columns.Add(new ColumnMapping { Name = name });
            return parent;
        }

        public TParent Add(params string[] names)
        {
            foreach (var name in names)
            {
                Add(name);
            }
            return parent;
        }

        public TParent Add(string columnName, Action<ColumnPart> customColumnMapping)
        {
            var mapping = new ColumnMapping { Name = columnName };
            var part = new ColumnPart(mapping);
            customColumnMapping(part);
            columns.Add(mapping);
            return parent;
        }

        public TParent Clear()
        {
            columns.Clear();
            return parent;
        }

        public IEnumerator<ColumnMapping> GetEnumerator()
        {
            return columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}