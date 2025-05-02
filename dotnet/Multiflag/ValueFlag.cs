using System.Collections.Generic;

namespace Multiflag
{
    internal class ValueFlag<TSet> : Flag<TSet>
    {
        private readonly TSet value;

        internal ValueFlag(
            ISetOperations<TSet> operations,
            IEnumerable<Flag<TSet>> parents,
            TSet value
        ) : base(operations, parents)
        {
            this.value = value;
        }

        public override bool IsAbstract => false;

        public override TSet AddTo(TSet flags)
        {
            return base.AddTo(this.Operations.Union(flags, this.value));
        }

        public override TSet RemoveFrom(TSet flags)
        {
            return base.RemoveFrom(this.Operations.Difference(flags, this.value));
        }

        public override bool IsIn(TSet flags)
        {
            return this.Operations.IsSupersetOf(flags, this.value)
                   && base.IsIn(flags);
        }
    }
}