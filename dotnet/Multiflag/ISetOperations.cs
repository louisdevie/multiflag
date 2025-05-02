namespace Multiflag
{
    internal interface ISetOperations<TSet>
    {
        /// <summary>
        ///     Creates an empty set of flags.
        /// </summary>
        TSet Empty();
        
        /// <summary>
        ///     Computes the union of two sets of flags.
        /// </summary>
        public TSet Union(TSet first, TSet second);
        
        /// <summary>
        ///     Computes the intersection of two sets of flags.
        /// </summary>
        public TSet Intersection(TSet first, TSet second);

        /// <summary>
        ///     Computes the difference of two set of flags.
        /// </summary>
        public TSet Difference(TSet first, TSet second);

        /// <summary>
        ///     Checks whether the first set of flags is a superset of the second.
        /// </summary>
        public bool IsSupersetOf(TSet first, TSet second);
    }
}